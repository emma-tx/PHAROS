using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using AzureSSGTest.Model;
using System.Collections.Immutable;
using System.Net;
using System.Text;

namespace AzureSSGTest
{
    class GenerateSitePage
    {
        static void Main(string[] args)
        {
            var templateData = GetTemplateMarkup();
            var siteContentData = GetsiteContentData();
            var updatedPageMarkup = GeneratePage(templateData, siteContentData);
            return;
        }

        // Extract HTML markup from the page template JSON
        public static string GetTemplateMarkup()
        {
            String jsonString = new StreamReader("pagetemplate.json").ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(jsonString);
            var templateMarkup = data.body;
            return templateMarkup;
        }

        // Read current module listing from JSON returned by Vero API
        public static List<BlogListItem> GetsiteContentData()
        {
            // Get blogposts data returned by Vero API and convert it into a queryable model.
            String jsonString = new StreamReader("blogposts.json").ReadToEnd();
            JObject extractedsiteContentData = JObject.Parse(jsonString);
            var model = JsonConvert.DeserializeObject<Root>(jsonString);

            List<BlogListItem> BlogListItems = new List<BlogListItem>();

            // Adding list object twice, as the test data doesn't yet have multiple records
            // A loop can be used instead when there are muliple records
            BlogListItems.Add(new BlogListItem()
            {
                Title = model.Title.ToString(),
                description = model.created_at.ToString()
            });

            BlogListItems.Add(new BlogListItem()
            {
                Title = model.Title.ToString(),
                description = model.created_at.ToString()
            });

            return BlogListItems;
        }

        // Passing HTML template markup and content to this method, which replaces placeholders with
        // generated HTML.
        public static string GeneratePage(string pageMarkup, List<BlogListItem> siteContentData)
        {
            List<string> blogpostsTitles = new List<string>();

            foreach (var i in siteContentData)
            {
                blogpostsTitles.Add("<li>" + i.Title + "</li>");
            }

            var blogpostsTitleList = string.Join(" ", blogpostsTitles);
            string updatedHTML = pageMarkup.Replace("__blogpostlist__", "<ul>" + blogpostsTitleList + "</ul>");

            return updatedHTML;
        }

        // This is where we put a typed list for data items we want to display on the page
        public class BlogListItem
        {
            public string Title { get; set; }
            public string description { get; set; }
        }
    }
}
