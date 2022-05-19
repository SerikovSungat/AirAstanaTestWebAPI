using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AirAstanaWebAPI.Middlewares
{
	public class JwtMiddleware
	{
		private readonly RequestDelegate next;
		private readonly AuthenticationOptions appSettings;

		public JwtMiddleware(RequestDelegate next, IOptions<AuthenticationOptions> appSettings)
		{
			this.next = next ?? throw new ArgumentNullException(nameof(next));
			this.appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
		}

		public async Task Invoke(HttpContext context)
		{
			var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
			if (token != null)
			{
				this.AttachUserToContext(token);
			}

			await this.next(context);
		}

		private void AttachUserToContext(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(this.appSettings.SecretKey);

			tokenHandler.ValidateToken(token, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
				ClockSkew = TimeSpan.Zero,
			}, out SecurityToken validatedToken);
		}
	}
}
