using application_programming_interface.Interfaces;
using application_programming_interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Text;

namespace application_programming_interface.Atributes
{
    public class AuthenticationAttribute :ActionFilterAttribute, IActionFilter
    {
        private string _role;
        public AuthenticationAttribute()
        {

        }
        //CAAAAAAAAAAAAARRRRRRRRRRRRRRRMMMMMMMMMMMMMMMMMMMEEEEEEEEEEEEEEEEEEEEENNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNnNnNnNnNnnNnN

        public AuthenticationAttribute(string role)
        {
            _role = role;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                throw new AuthenticationException("No auth header detected");
            }
            var handler = new JwtSecurityTokenHandler();
            //decryptedToken.EncodedPayload
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("852852-852852-852852-416534163")),
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("852852-852852-852852-416534163")),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            //TODO: get roles and stuff from db and dubble chekc information
            var authService = (AuthenticationService)context.HttpContext.RequestServices.GetService(typeof(IAuthenticationService));
            var claim = claims.Claims.ToList();
            var name = claim.Where(x => x.Type.Equals("Name", System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value;
            var roles = claim.Where(x => x.Type.Equals("Roles", System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value;

            if (!string.IsNullOrEmpty(_role))
            {
                if (!roles.Contains(_role))
                {
                    throw new ValidationException("Sorry , you do not have permision to access this shit.");
                }
            }
            authService.User.SetData(0, name,"Empty for now", roles);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
    }
}
