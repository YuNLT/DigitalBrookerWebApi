using DigitalBrooker.Domain.Entities.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Querirs
{
    public class GetAllPostQuery : IRequest<List<Post>>;
}
