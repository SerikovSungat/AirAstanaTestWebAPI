using Application.Abstract.Repositories;
using AutoMapper;
using Domain.JounraledEntities.Flight;
using MediatR;
using Shared.Dtos.Flight;
using Shared.Queries.Flight;
using System.Linq.Expressions;

namespace Application.QueryHandlers.Flights
{
	public class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, FlightListDto>
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public GetFlightsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public async Task<FlightListDto> Handle(GetFlightsQuery query, CancellationToken cancellationToken)
		{
			var repository = this.unitOfWork.FlightRepository;

			Expression<Func<FlightEntity, bool>> queryFilter = (!string.IsNullOrEmpty(query.Origin) || !string.IsNullOrEmpty(query.Destination)) ? (e) => (e.Origin == query.Origin || e.Destination == query.Destination) : null;

			Func<IQueryable<FlightEntity>, IOrderedQueryable<FlightEntity>> orderBy = (o) => o.OrderByDescending(d => d.Arrival);

			var entities = await repository.GetAsync(cancellationToken, filter: queryFilter, orderBy: orderBy);

			var totalCount = await repository.CountAsync(cancellationToken, filter: queryFilter);

			var items = this.mapper.Map<List<FlightDto>>(entities);

			var dto = new FlightListDto()
			{
				Items = items,
				TotalCount = totalCount
			};

			return dto;
		}
	}
}
