using IdentityServer4.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("CatalogApi")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("catalog")
                    }
                }, new ApiResource("BasketApi")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("basket")
                    }
                }
                
            };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new[]
            {
                // new Client
                // {
                //     ClientId = "ApiClient",
                //     AllowedGrantTypes = GrantTypes.ClientCredentials,
                //     ClientSecrets =
                //     {
                //         new Secret("secret".Sha256())
                //     }
                // },
                new Client
                {
                    ClientId = "catalogswaggerui",
                    ClientName = "Catalog Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"http://localhost:5288/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"http://localhost:5288/swagger/" },

                    AllowedScopes =
                    {
                        "catalog"
                    }
                },
                new Client
                {
                    ClientId = "basketswaggerui",
                    ClientName = "Basket Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"http://localhost:5286/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"http://localhost:5286/swagger/" },

                    AllowedScopes =
                    {
                        "basket"
                    }
                }
            };
        }
    }
}

