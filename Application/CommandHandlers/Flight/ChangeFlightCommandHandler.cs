using Application.Abstract.Repositories;
using AutoMapper;
using Domain.JounraledEntities.Flight.Events;
using MediatR;
using Shared.Commands.Flight;

namespace Application.CommandHandlers.Flight
{
    public class ChangeFlightCommandHandler : IRequestHandler<ChangeFlightCommand, int>
    {
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public ChangeFlightCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public async Task<int> Handle(ChangeFlightCommand command, CancellationToken cancellationToken)
		{
			var repository = this.unitOfWork.FlightRepository;
			var @event = this.mapper.Map<FlightChangedEvent>(command);
			return await repository.RaiseEvent(@event, cancellationToken);
		}
	}
}
