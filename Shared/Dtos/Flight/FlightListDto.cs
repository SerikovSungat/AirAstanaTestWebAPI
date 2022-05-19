using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Flight
{
    public class FlightListDto : BaseListDto
    {
        public List<FlightDto> Items { get; set; }
    }
}
