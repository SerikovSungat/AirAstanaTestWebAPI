using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands.User;
using Shared.Dtos.User;
using Swashbuckle.AspNetCore.Annotations;

namespace AirAstanaWebAPI.Controllers
{
	/// <summary>
	/// API контроллер для аутентификации пользователя.
	/// </summary>
	[Route("/authentication")]
	[ApiController]
	[SwaggerTag("Аутентификация пользователя.")]
	public class AuthenticationController : ControllerBase
	{
		readonly IMediator mediator;

		/// <summary>
		/// Конструктор контроллера.
		/// </summary>
		/// <param name="mediator"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public AuthenticationController(IMediator mediator)
		{
			this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		/// <summary>
		/// Аутентификация пользователя по логину и паролю.
		/// </summary>
		/// <param name="query">Объект аутентификации для авторизации.</param>
		/// <param name="cancellationToken">Маркер отмены, используемый для отмены HTTP-запроса.</param>
		/// <returns></returns>
		[HttpPost("token")]
		[SwaggerResponse(StatusCodes.Status200OK, "Аутентификация прошла успешно.", typeof(UserDto))]
		public async Task<IActionResult> AuthenticationAsync([FromBody] GetUserCommand command, CancellationToken cancellationToken)
		{
			var result = await this.mediator.Send(command, cancellationToken);
			return this.Ok(result);
		}
	}
}
