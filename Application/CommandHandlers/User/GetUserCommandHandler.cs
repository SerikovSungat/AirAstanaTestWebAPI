using System.Linq.Expressions;
using AutoMapper;
using Application.Abstract.Repositories;
using Domain.JounraledEntities.User;
using Shared.Dtos.User;
using MediatR;
using Application.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Application.Options;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Shared.Commands.User;

namespace Application.CommandHandlers.User
{
	public class GetUserCommandHandler : IRequestHandler<GetUserCommand, UserDto>
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly AuthenticationOptions appSettings;

		public GetUserCommandHandler(IOptions<AuthenticationOptions> appSettings, IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			this.appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
		}

		public async Task<UserDto> Handle(GetUserCommand command, CancellationToken cancellationToken)
		{
			var repository = this.unitOfWork.UserRepository;
			Expression<Func<UserEntity, bool>> queryFilter = (e) => e.UserName.ToLower() == command.UserName.ToLower().Trim();
			var data = await repository.GetAsync(cancellationToken, filter: queryFilter);
			var userInfo = data.FirstOrDefault();

			if (userInfo == null)
				throw new UserNotFoundException();
			else if (userInfo.UserName.Equals(command.UserName) && Decrypt(userInfo.Password).Equals(command.Password))
			{
				var userDto = new UserDto
				{
					UserId = userInfo.Id,
					UserName = userInfo.UserName
				};
				userDto.Role = userInfo.RoleEntity.Code;
				userDto.Token = this.GenerateJwtToken(userDto.UserName, userDto.Role);

				return userDto;
			}
			else
				throw new UserNotAuthorizedException("Неправильный логин или пароль");
		}

		private string GenerateJwtToken(string username, string role)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(this.appSettings.SecretKey);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim("username", username), new Claim("role", role) }),
				Expires = DateTime.UtcNow.AddMinutes(15),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

        #region раздел для пароля
        private string Encrypt(string text)
		{
			using (var md5 = new MD5CryptoServiceProvider())
			{
				using (var tdes = new TripleDESCryptoServiceProvider())
				{
					tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(this.appSettings.SecretKey));
					tdes.Mode = CipherMode.ECB;
					tdes.Padding = PaddingMode.PKCS7;

					using (var transform = tdes.CreateEncryptor())
					{
						byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
						byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
						return Convert.ToBase64String(bytes, 0, bytes.Length);
					}
				}
			}
		}

		private string Decrypt(string cipher)
		{
			using (var md5 = new MD5CryptoServiceProvider())
			{
				using (var tdes = new TripleDESCryptoServiceProvider())
				{
					tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(this.appSettings.SecretKey));
					tdes.Mode = CipherMode.ECB;
					tdes.Padding = PaddingMode.PKCS7;

					using (var transform = tdes.CreateDecryptor())
					{
						byte[] cipherBytes = Convert.FromBase64String(cipher);
						byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
						return UTF8Encoding.UTF8.GetString(bytes);
					}
				}
			}
		}
		#endregion
	}
}
