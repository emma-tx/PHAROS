#r "Newtonsoft.Json"
#r "..\SiteDataModel.dll"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Site.Data.Models;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

    var siteFileData = GetSiteFileData();
    var currentPosts = GetCurrentPosts(siteFileData);
    var currentFiles = GetFileListing(siteFileData);

    var filesToDelete = FilesToDelete(currentPosts, currentFiles);

    return new OkObjectResult(new{veroid = sitePageData.post.PostId, page = updatedPageMarkup});
}

public static Root GetSiteFileData(string requestBody)
{
    var model = JsonConvert.DeserializeObject<Root>(requestBody);
    return model;
}


public static List<string> GetCurrentPosts(Root model)
{
    List<string> postIdList = new List<string>(); 

    var posts = model.posts.ToList();

    foreach(var i in posts)
    {
        postIdList.Add(i.PostId.ToString());
    }
    
    return postIdList;
}

// Read current file listing
public static List<string> GetFileListing(Root model)
{
    List<string> fileNamesList = new List<string>();

    var currentFiles = model.bloblist.ToList();

    foreach (var item in currentFiles)
    {
        string postName = item.Name.Replace(".html", "");
        fileNamesList.Add(postName);
    }

    return fileNamesList;
}

public static List<string> FilesToDelete(List<string> currentPosts, List<string> currentFiles)
{
    List<string> filesToDelete = new List<string>();

    var expiredContent = currentFiles.Except(currentPosts).ToList();

    foreach(var i in expiredContent) 
    {
        var fileName = "/$web/posts/" + i.ToString() + ".html";
        filesToDelete.Add(fileName);
    }

    filesToDelete.RemoveAt(0);

    return filesToDelete;
}

public class Root
{
    public IEnumerable<Post> posts { get; set; }
    public IEnumerable<BlobData> bloblist { get; set; }
}

public class BlobData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string Path { get; set; }
    public DateTime LastModified { get; set; }
    public int Size { get; set; }
    public string MediaType { get; set; }
    public bool IsFolder { get; set; }
    public string ETag { get; set; }
    public string FileLocator { get; set; }
    public object LastModifiedBy { get; set; }
}
