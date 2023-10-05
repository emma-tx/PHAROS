using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace RestSharpExample
{
    class Program

        static void Main(string[] args)
        {
            string apiUrl = "https://myapi.myorganisation.net/api/allrecords";
            var apiToken = GetApiToken().access_token.ToString();
            var returnedData = GetApiData(apiUrl, apiToken);
        }

        public static Token GetApiToken()
        {
            RestClient _client = new RestClient();

            var request = new RestRequest($"https://login.microsoftonline.com/{Azure AD Tenant ID}/oauth2/v2.0/token", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			
            request.AddParameter("client_id", "{Client ID}");
            request.AddParameter("client_secret", "Client Secret");
            request.AddParameter("grant_type", "client_credentials", ParameterType.GetOrPost);
            request.AddParameter("scope", "https://graph.microsoft.com/.default", ParameterType.GetOrPost);

            RestSharp.RestResponse response = _client.ExecutePost(request);

            return JsonConvert.DeserializeObject<Token>(response.Content);
        }


        public static string GetApiData(string apiUrl, string apiToken)
        {
            RestClient _client = new RestClient();

            var request = new RestRequest(apiUrl, Method.Get);
            request.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(apiToken.ToString(), "Bearer");
            RestSharp.RestResponse response = _client.ExecuteGet(request);
            var returnedApiData = response.Content;

            return returnedApiData;
        }
    }


}