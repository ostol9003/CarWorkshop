using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CarWorkshop.Application.CarWorkshopService.Querries
{
    public class GetCarWorkshopServiceQuery : IRequest<IEnumerable<CarWorkshopServiceDto>>
    {
        public string EncodedName { get; set; } = default!;
    }
}
