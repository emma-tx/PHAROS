#r "Newtonsoft.Json"
#r "..\SiteDataModel.dll"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Site.Data.Models;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function received. Building page.");

    // The request body here contains both the site data and template page HTML
    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    var sitePageData = GetSitePageData(requestBody);
    var updatedPageMarkup = GeneratePage(sitePageData, log);

    return new OkObjectResult(new{postid = sitePageData.post.PostId, page = updatedPageMarkup});
}

public static Root GetSitePageData(string data)
{
    var model = JsonConvert.DeserializeObject<Root>(data);
    return model;
}

// Passing HTML template markup and content to this method, which replaces placeholders with generated HTML.
public static string GeneratePage(Root model, ILogger log)
{
    List<string> authorList = new List<string>();
    List<string> tagsList = new List<string>();
    List<string> categoriesList = new List<string>();

    // Generate HTML elements for each data type we want to render as a list or table row.
    if (model.post.authors?.Any() ?? false)
    {
        foreach (var i in model.post.authors)
        {
            authorList.Add("<tr>" + "<td>" + i.authorName + "</td>" + "<td>" + i.authorRole + "</td>" ?? "</tr>");
        }
    }

    if (model.post.tags?.Any() ?? false)
    {
        foreach (var i in model.post.tags)
        {
            tagsList.Add("<td>" + i.tag + "</td> ?? "" + "</td>");
        }
    }

    if (model.post.categories?.Any() ?? false)
    {
        foreach (var i in model.post.categories)
        {
            categoriesList.Add("<td>" + i.category + "</td>" ?? "" + "</td>");
        }
    }

    // The string join is required to render list items as text.
    var authorsString = string.Join(" ", authorList);
    var tagsString = string.Join(" ", tagsList);
    var categoriesString = string.Join(" ", categoriesList);

    string updatedHTML = model.template
        .Replace("__title__", model.post?.title ?? "")
        .Replace("__postDate__", model.post?.postDate ?? "")
        .Replace("__authors__", authorsString ?? "")
        .Replace("__tags__", tagsString ?? "")
        .Replace("__categories__", categoriesString ?? "");
    return updatedHTML;
}

// Maps the JSON objects we want to use in the request body.
public class Root
{
    public Post post { get; set; }
    public string? template { get; set; }
}

