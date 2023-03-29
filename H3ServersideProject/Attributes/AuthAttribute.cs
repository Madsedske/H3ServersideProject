using Azure.Core;
using H3ServersideProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace H3ServersideProject.Attributes
{
    public class AuthAttribute : TypeFilterAttribute
    {
        public AuthAttribute() : base(typeof(AuthFilter))
        {
        }
    }

    public class AuthFilter : Attribute, IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        public AuthFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var config = _configuration.GetSection("AppSettings").GetValue<string>("Token");
            var giffelgaffelguffel = context.HttpContext.Request.Cookies.TryGetValue("AuthCookie", out string jwt);

            if (giffelgaffelguffel)
            {
                //try
                //{
                //    var tokenHandler = new JwtSecurityTokenHandler();
                //    var validationParameters = new TokenValidationParameters
                //    {
                //        ValidIssuer = "fedabe.dk",
                //        ValidateIssuer = true,
                //        ValidateIssuerSigningKey = true,
                //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config)),
                //        ValidateAudience = false,
                //    };

                //    SecurityToken validatedToken;
                //    var principal = tokenHandler.ValidateToken(jwt, validationParameters, out validatedToken);
                //    var identity = principal.Identity as ClaimsIdentity;

                //    // set the user principal on the context 
                //    context.HttpContext.User = principal;
                //}
                //catch (Exception)
                //{
                //    // redirect to login page or return 401 Unauthorized
                //    context.Result = new UnauthorizedResult();
                //    return;
                //}
                return;
            }
            else
            {                
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
