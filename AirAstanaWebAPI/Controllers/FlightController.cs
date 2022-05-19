using AirAstanaWebAPI.Middlewares.ResourceFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands.Flight;
using Shared.Dtos.Flight;
using Shared.Queries.Flight;
using Swashbuckle.AspNetCore.Annotations;
using AuthorizeAttribute = AirAstanaWebAPI.Attributes.AuthorizeAttribute;

namespace AirAstanaWebAPI.Controllers
{
    /// <summary>
	/// API контроллер для рейсов.
	/// </summary>
	[Route("/flight")]
    [ApiController]
    [SwaggerTag("Рейсы.")]
    public class FlightController : ControllerBase
    {
		readonly IMediator mediator;

		/// <summary>
		/// Конструктор контроллера.
		/// </summary>
		/// <param name="mediator"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public FlightController(IMediator mediator)
		{
			this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		/// <summary>
		/// Получение списка рейсов.
		/// </summary>
		/// <param name="query">Запрос для получения списка рейсов.</param>
		/// <param name="cancellationToken">Маркер отмены, используемый для отмены HTTP-запроса.</param>
		/// <returns></returns>
		[HttpGet("items")]
		[SwaggerResponse(StatusCodes.Status200OK, "Коллекция рейсов.", typeof(FlightListDto))]
		public async Task<IActionResult> GetItemsAsync([FromQuery] GetFlightsQuery query, CancellationToken cancellationToken)
		{
			var result = await this.mediator.Send(query, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Создание нового мерчанта (платежи в бюджет).
		/// </summary>
		/// <param name="command">Объект мерчанта (платежи в бюджет) для создания.</param>
		/// <param name="cancellationToken">Маркер отмены, используемый для отмены HTTP-запроса.</param>
		/// <returns></returns>
		[HttpPost("create")]
		[AuthorizeAttribute("Moderator")]
		[ServiceFilter(typeof(IdempotencyFilterAttribute))]
		[SwaggerResponse(StatusCodes.Status201Created, "Рейс был создан.", typeof(FlightDto))]
		public async Task<IActionResult> CreateAsync([FromBody] CreateFlightCommand command, CancellationToken cancellationToken)
		{
			await this.mediator.Send(command, cancellationToken);
			return this.Created(string.Empty, null);
		}

		/// <summary>
		/// Обновление существующего мерчанта (платежи в бюджет) с указанным уникальным идентификатором.
		/// </summary>
		/// <param name="command">Объект мерчанта (платежи в бюджет) для обновления.</param>
		/// <param name="cancellationToken">Маркер отмены, используемый для отмены HTTP-запроса.</param>
		/// <returns></returns>
		[HttpPost("change")]
		[AuthorizeAttribute("Moderator")]
		[SwaggerResponse(StatusCodes.Status200OK, "Рейс с указанным уникальным идентификатором был обновлен.", typeof(FlightDto))]
		public async Task<IActionResult> ChangeAsync([FromBody] ChangeFlightCommand command, CancellationToken cancellationToken)
		{
			await this.mediator.Send(command, cancellationToken);
			return this.Ok();
		}
	}
}
