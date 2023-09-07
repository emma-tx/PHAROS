using Microsoft.Graph;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using System.Threading.Tasks;
using System.Linq;

namespace MyAPI.Api.Helpers
{
    public class GetSessionRequestGroup
    {
        public static async Task<List<string>> GetRequestorGroup(string requestorName)
        {
            var thisConfig = Utils.GetConfig().AsEnumerable();

            var tenantId = thisConfig.Where(o => o.Key == "AzureAd:TenantId").FirstOrDefault().Value.ToString();
            var clientId = thisConfig.Where(o => o.Key == "AzureAd:ClientId").FirstOrDefault().Value.ToString();
            var clientSecret = thisConfig.Where(o => o.Key == "AzureAd:ClientSecret").FirstOrDefault().Value.ToString();
            var ClientScopes = new string[] { "https://graph.microsoft.com/.default" };

            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret, options);
            GraphServiceClient graphClient = new GraphServiceClient(clientSecretCredential, ClientScopes);

            var graphRequest = graphClient.Users[requestorName].MemberOf.GetAsync().Result;

            var userGroups = graphRequest.Value.Select(o => o.Id).AsEnumerable();

            List<string> groupMemberships = new List<string>();

            if (userGroups != null)
            {
                foreach (var i in userGroups)
                {
                    groupMemberships.Add(i.ToString());
                }
            }

            return groupMemberships;
        }

        public static List<ApplicationGroups> GetApplicationGroups()
        {
            List<ApplicationGroups> groupsList = new List<ApplicationGroups>();

            var thisConfig = Utils.GetConfig().AsEnumerable();
            var applicationGroups = thisConfig.Where(o => o.Key.Contains("ApplicationGroups") && o.Value != null).ToList();

            foreach (var i in applicationGroups)
            {
                string groupName = i.Key.Replace("ApplicationGroups:", "");
                groupsList.Add(new ApplicationGroups() { GroupName = groupName, GroupId = i.Value });
            }

            return groupsList;
        }

        public static string GetAuthLevel(List<string> userGroups)
        {
            var ReadOnlyGroup = GetSessionRequestGroup.GetReadOnlyGroup();
            var ReadWriteGroup = GetSessionRequestGroup.GetReadWriteGroup();
            var AdminGroup = GetSessionRequestGroup.GetAdminGroup();

            string authLevel = "";

            if (userGroups.Any().ToString() == ReadOnlyGroup && userGroups.Any().ToString() != ReadWriteGroup && userGroups.Any().ToString() != AdminGroup && userGroups.Any().ToString() != SuperAdminGroup)
            {
                authLevel = "ReadOnly";
            }
              
            if (userGroups.Any().ToString() == ReadOnlyGroup && userGroups.Any().ToString() == ReadWriteGroup && userGroups.Any().ToString() != AdminGroup && userGroups.Any().ToString() != SuperAdminGroup)
            {
                authLevel = "ReadWrite";
            }

            if (userGroups.Any().ToString() == AdminGroup && userGroups.Any().ToString() != SuperAdminGroup)
            {
                authLevel = "Admin";
            }

            else
            {
                authLevel = "Unauthorised";
            }

            return authLevel;
        }

        public static string GetReadOnlyGroup()
        {
            var applicationGroups = GetApplicationGroups();
            var readOnlyGroup = applicationGroups.Where(o => o.GroupName == "READ_ONLY_USERS_GROUP_ID").FirstOrDefault().GroupId;
            return readOnlyGroup;
        }

        public static string GetReadWriteGroup()
        {
            var applicationGroups = GetApplicationGroups();
            var ReadWriteGroup = applicationGroups.Where(o => o.GroupName == "READ_WRITE_GROUP_ID").FirstOrDefault().GroupId;
            return ReadWriteGroup;
        }

        public static string GetAdminGroup()
        {
            var applicationGroups = GetApplicationGroups();
            var AdminGroup = applicationGroups.Where(o => o.GroupName == "ADMIN_GROUP_ID").FirstOrDefault().GroupId;
            return AdminGroup;
        }
    }

    public class ApplicationGroups
    {
        public string GroupName { get; set; }
        public string GroupId { get; set; }
    }
}
