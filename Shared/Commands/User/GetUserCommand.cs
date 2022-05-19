using Shared.Dtos.User;
using MediatR;

namespace Shared.Commands.User
{
	public class GetUserCommand : IRequest<UserDto>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
