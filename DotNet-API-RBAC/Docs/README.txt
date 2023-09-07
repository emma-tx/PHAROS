==== .NET API ROLE-BASED ACCESS CONTROL ====

## 1. CONFIGURATION / APPSETTINGS
The following need to be added to the appsettings file:

```
"Graph": {
    "BaseUrl": "https://graph.microsoft.com/v1.0",
    "Scopes": "https://graph.microsoft.com/.default"
},
"AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "TenantId": "[Tenant ID]",
    "Domain": "[Domain]"
},
"ApplicationGroups": {
    "READ_ONLY_USERS_GROUP_ID": "[Group ID]",
    "READ_WRITE_GROUP_ID": "[Group ID]",
    "ADMIN_GROUP_ID": "[Group ID]"
}
```


## 2. DEPENDENCIES AND ASSEMBLY REFERENCES
The following NuGet packages are required:
- Microsoft.Identity.Web
- Microsoft.Identity.Client
- Microsoft.IdentityModel.Clients
- Microsoft.IdentityModel.Clients.ActiveDirectory
- Microsoft.Graph
- Microsoft.Graph.Auth
- Microsoft.Azure.ActiveDirectory.GraphClient
- Microsoft.Extensions.Configuration.UserSecrets
- Microsoft.AspNetCore.Mvc.NewtonsoftJson

There are multiple versions of the Microsoft Graph package, with differences in how the Graph client is used in the code. Installing NewtonsoftJson prior to installing the latest version of Microsoft.Graph is recommended.


## 3. API Base Controller Method
This is the base controller method that other API methods are derived from. It is here that we can apply the security-related code that replaces the function of the *[Authorize]* attribute for access control.

In order to do achieve this, another class, *MicrosoftGroupsAuthentication*, is added, which performs the Microsoft Graph queries and returns the user's role according to the Active Directory group(s) returned.

First, the method needs to get the '*preferred_username*' value from the JWT token that's passed from the React/Node.JS application. This value is passed to the *MicrosoftGroupsAuthentication* methods, one of which queries Active Directory for that user via Microsoft Graph.

The following line can be modified to allow or disallow access to the main part of the API depending on what's returned by the *MicrosoftGroupsAuthentication.ReturnRole(requestorGroup)* method.

```
if (!roles.Any())
{
    _logger.LogError(prefUser + " is not a member of an authorised group");
    return Unauthorized(new Response { Status = false });
}
```

## 4. Microsoft Graph Client
The *GetRequestorGroup* method uses Microsoft Graph to query Azure Active Directory for information about a user. In this case the authentication requires the ID(s) for the group(s) that user is a member of.

Three parameters are required for the Graph Client:
- Azure Tenant ID
- Azure Client ID
- Azure Client Secret

These parameters are stored in the appsettings.json file.

As far as I can determine, the 'scope' is usually '*https://graph.microsoft.com/.default*'.

The above client parameters can be tested using Microsoft Graph Explorer.

With the parameters being provided, the GraphServiceClient can be instantiated and used to perform the query. Given the user would likely be a member of several Active Directory groups, the returned object should be typed as an enumerable and each value within that added to a List<> to be returned to the base controller method.

```
GraphServiceClient graphClient = new GraphServiceClient(clientSecretCredential, ClientScopes);
var graphRequest = graphClient.Users[requestorName].MemberOf.GetAsync().Result;
var userGroups = graphRequest.Value.Select(o => o.Id).AsEnumerable();
```


## 5. API User Role Handler Method
The base controller method, having called the Microsoft Graph handler and received a list of group IDs, will pass those IDs to another method, which, in turn, checks whether any of them matches an authorised group. If they do, the method will return a group name.

A list of authorised groups is stored in the appsettings.json file.


