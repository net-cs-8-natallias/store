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
                new ApiResource("Catalog Api")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("catalogApi")
                    }
                }, 
                new ApiResource("Basket Api")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("basketApi")
                    }
                }, 
                new ApiResource("Order Api")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("orderApi")
                    }
                }, 
                new ApiResource("CatalogApi")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("catalog")
                    }
                }, 
                new ApiResource("BasketApi")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("basket")
                    }
                }, 
                new ApiResource("OrderApi")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("order")
                    }
                }
                
            };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new[]
            {
                new Client
                {
                    ClientId = "catalog.client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("catalogSecret".Sha256()) },
                    AllowedScopes = { "catalogApi" }
                },
                new Client
                {
                    ClientId = "basket.client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("basketSecret".Sha256()) },
                    AllowedScopes = { "basketApi" }
                },
                new Client
                {
                    ClientId = "order.client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("orderSecret".Sha256()) },
                    AllowedScopes = { "orderApi" }
                },
                new Client
                {
                    ClientId = "OrderClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        "basket", "catalog"
                    }
                    
                },
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
                },
                new Client
                {
                    ClientId = "orderswaggerui",
                    ClientName = "Order Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"http://localhost:5230/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"http://localhost:5230/swagger/" },

                    AllowedScopes =
                    {
                        "order"
                    }
                }
            };
        }
    }
}

