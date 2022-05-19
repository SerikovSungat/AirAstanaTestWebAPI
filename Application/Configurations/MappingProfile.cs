using AutoMapper;
using Domain.JounraledEntities.Flight;
using Domain.JounraledEntities.Flight.Events;
using Shared.Commands.Flight;
using Shared.Dtos.Flight;

namespace Application.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateFlightMaps();
        }
        private void CreateFlightMaps()
        {
            this.CreateMap<FlightEntity, FlightDto>();
            this.CreateMap<CreateFlightCommand, FlightCreatedEvent>();
            this.CreateMap<ChangeFlightCommand, FlightChangedEvent>()
            .ForMember(d => d.EntityId, o => o.MapFrom(s => s.Id));
        }
    }
}
