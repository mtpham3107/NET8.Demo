using IdentityServer4.Models;
using IdentityServer4;

namespace NET8.Demo.Admin;

public class IdentityServerConfig
{
    public static IEnumerable<IdentityResource> IdentityResources => [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    ];

    public static IEnumerable<ApiResource> ApiResources => [
        new ApiResource("service1_api", "Service 1 API") {
            Scopes =  { "service1" }
        },
         new ApiResource("service2_api", "Service 2 API") {
            Scopes =  { "service2" }
        }
    ];

    public static IEnumerable<ApiScope> ApiScopes => [
        new ApiScope("service1", "Full access to service 1"),
        new ApiScope("service2", "Full access to service 2")
    ];

    public static IEnumerable<Client> Clients(Dictionary<string, string> clientUrls) => [
        new Client {
            ClientId = "client_service1",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
            ClientSecrets = {
                new Secret("client_secret_api".Sha256())
            },
            AllowedScopes = {
                IdentityServerConstants.StandardScopes.OpenId,
               IdentityServerConstants.StandardScopes.Profile,
            }
        },
        new Client {
            ClientId = "client_service2",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
            ClientSecrets = {
                new Secret("client_secret_api".Sha256())
            },
            AllowedScopes = {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
            }
        },
        new Client {
            ClientId = "client_app1",
            AllowedGrantTypes = GrantTypes.Code,
            ClientSecrets = {
                new Secret("client_secret_app1".Sha256())
            },
            AllowedScopes = {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "service1", "service2"
            },
            RedirectUris = { $"{clientUrls["App1"]}/login-callback" },
            PostLogoutRedirectUris = { $"{clientUrls["App1"]}/logout-callback" },
            AllowedCorsOrigins = { $"{clientUrls["App1"]}" },
        },
        new Client {
            ClientId = "client_app2",
            AllowedGrantTypes = GrantTypes.Code,
            ClientSecrets = {
                new Secret("client_secret_app2".Sha256())
            },
            AllowedScopes = {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "service1", "service2"
            },
            RedirectUris = { $"{clientUrls["App2"]}/login-callback" },
            PostLogoutRedirectUris = { $"{clientUrls["App2"]}/logout-callback" },
            AllowedCorsOrigins = { $"{clientUrls["App2"]}" },
        },
    ];
}
