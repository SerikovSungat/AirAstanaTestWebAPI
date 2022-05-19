using MediatR;
using Shared.Dtos.Flight;

namespace Shared.Queries.Flight
{
    public class GetFlightsQuery : IRequest<FlightListDto>
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}
