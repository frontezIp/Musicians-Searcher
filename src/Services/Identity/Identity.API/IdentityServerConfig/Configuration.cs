using Identity.Domain.IdentityConstants;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Identity.API.IdentityServerConfig
{
    public static class Configuration
    {

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource> {
                new ApiResource(ApiResourcesConstants.MUSICIANS_API_RESOURCE_NAME),
                new ApiResource(ApiResourcesConstants.CHAT_API_RESOURCE_NAME)
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client> 
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
                }
            };
    }
}
