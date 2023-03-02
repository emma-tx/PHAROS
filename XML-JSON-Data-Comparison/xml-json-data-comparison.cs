using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Xml;
using RemoveDeadFiles.Model;

namespace RemoveDeadFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get list of current content objects and list of content pages
            var currentContentObjectCodes = GetContentObjects();
            var contentFileNames = GetCurrentFileNames();

            // Search the list for expired content objects
            // For this to work, both JSON and XML must contain an ID field
            var filesToDelete = FilesToDelete(currentContentObjectCodes, contentFileNames);

            // Call method to send DELETE API request
            RemoveFiles(filesToDelete);

            Console.WriteLine("------------------------\n");
            Console.Write("Press a key to continue...");
            Console.WriteLine("\n");
            return;
        }

        // Get list of content pages in the directory
        public static List<string> GetCurrentFileNames()
        {
            // Load the returned XML as an XmlDocument
            XmlDocument doc = new XmlDocument();
            doc.Load("listing.xml");

            List<string> contentFileNames = new List<string>();

            XmlNodeList elemList = doc.GetElementsByTagName("Name");
            for (int i = 0; i < elemList.Count; i++)
            {
                string pageURI = elemList[i].InnerXml;

                // Remove file extension from each URI, giving us just the content object's ID.
                string pageName = pageURI.Replace(".txt", "");
                contentFileNames.Add(pageName);
            }

            return contentFileNames;
        }

        // Read current content object listing from JSON (possibly returned by an API)
        public static List<string> GetContentObjects()
        {
            String jsonString = new StreamReader("listing.json").ReadToEnd();
            JObject extracted = JObject.Parse(jsonString);

            List<string> contentObjectList = new List<string>();

            var model = JsonConvert.DeserializeObject<Root>(jsonString);

            var getUniqueObjects = model.EnumerationResults.Blobs.Blob.Select(o => o.Name).ToList();

            foreach (var item in getUniqueObjects)
            {
                string objectItem = item.Replace(".txt", "");
                contentObjectList.Add(objectItem);
            }

            return contentObjectList;
        }

        // Compare the list of content page files with current content object listing
        public static List<string> FilesToDelete(List<string> currentObjects, List<string> contentFileNames)
        {
            var expiredContentObjects = contentFileNames.Except(currentObjects).ToList();
            return expiredContentObjects;
        }

        // Some Azure API code here. This should be ignored.
        // Left this as public static string in case we need to return the status
        public static string RemoveFiles(List<string> fileList)
        {
            //Declare fileNamesToDelete
            var listOfFiles = fileList;

            // Create REST API client
            var restClientOptions = new RestClientOptions("https://myaccount.blob.core.windows.net/")
            {
                ThrowOnAnyError = true,
                Timeout = 1000
            };
            var client = new RestClient(restClientOptions);

            foreach (var i in fileList)
            {
                string objectName = i.ToString();
                var deleteObjectRequest = new RestRequest($"MyContainer/{objectName}", Method.Delete);
            }

            string fileNames = null;
            return (fileNames);
        }
    }
}
