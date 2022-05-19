using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace AirAstanaWebAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<string> _roles;

        public AuthorizeAttribute(params string[] roles)
        {
            _roles = roles ?? new string[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault().Split(" ").Last();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            var role = tokenS.Claims.First(x => x.Type == "role").Value;

            if (string.IsNullOrEmpty(role) || !_roles.Contains(role))
                context.Result = new JsonResult("Нет прав доступа.")
                {
                    Value = new
                    {
                        Status = "Нет прав доступа",
                        StatusCode = StatusCodes.Status401Unauthorized,
                       
                    },
                };
        }
    }
}
