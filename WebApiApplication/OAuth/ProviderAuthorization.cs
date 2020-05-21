using Microsoft.Owin.Security.OAuth;
using RepositoryLibrary;
using System;
using System.Security.Claims;
using System.Threading.Tasks; 
using EntityLibrary;

namespace WebApiApplication.OAuth
{
    public class ProviderAuthorization : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                UserLoginRepo ulr = new UserLoginRepo();
                UserLogin ul = ulr.GetLogin(context.UserName, context.Password);
                if (ul != null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Role Ayarlanicak"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "Username and Password is Incorrect");
                    return;
                }

            }
            catch (Exception ex)
            {

            }

        }
    }
}