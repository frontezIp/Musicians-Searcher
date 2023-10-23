using Identity.Domain.IdentityConstants;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Identity.API.IdentityServerConfig
{
    public static class Configuration
    {

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope(ApiResourcesConstants.MUSICIANS_API_RESOURCE_NAME, "Full Access"),
                new ApiScope(ApiResourcesConstants.CHAT_API_RESOURCE_NAME, "Full Access")
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource> {
                new ApiResource(ApiResourcesConstants.MUSICIANS_API_RESOURCE_NAME)
                {
                    ApiSecrets = new List<Secret>
                    {
                        new Secret(ApiResourcesConstants.API_RESOURCES_SECRET.ToSha256())
                    },
                    Scopes =
                    {
                        ApiResourcesConstants.MUSICIANS_API_RESOURCE_NAME
                    }
                },
                new ApiResource(ApiResourcesConstants.CHAT_API_RESOURCE_NAME)
                {
                    ApiSecrets = new List<Secret>
                    {
                        new Secret(ApiResourcesConstants.API_RESOURCES_SECRET.ToSha256())
                    },
                    Scopes =
                    {
                        ApiResourcesConstants.CHAT_API_RESOURCE_NAME
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = SwaggerClientConstants.CLIENT_ID,
                    ClientSecrets = { new Secret(SwaggerClientConstants.CLIENT_SECRET.ToSha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireConsent = false,
                    AllowedScopes =
                    {
                        ApiResourcesConstants.CHAT_API_RESOURCE_NAME,
                        IdentityServerConstants.LocalApi.ScopeName,
                        ApiResourcesConstants.MUSICIANS_API_RESOURCE_NAME,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                 new Client
                {
                    ClientId = JavascriptClientConstants.CLIENT_ID,
                    ClientSecrets = { new Secret(JavascriptClientConstants.CLIENT_SECRET.ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireConsent = false,
                    AllowedScopes =
                    {
                        ApiResourcesConstants.CHAT_API_RESOURCE_NAME,
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}
