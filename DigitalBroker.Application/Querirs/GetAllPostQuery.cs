using DigitalBroker.Application.DTOs;
using MediatR;

namespace DigitalBroker.Application.Querirs
{
    public class GetAllPostQuery : IRequest<List<Post>>;
}
