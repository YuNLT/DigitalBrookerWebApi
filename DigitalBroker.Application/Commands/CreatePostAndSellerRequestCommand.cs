using DigitalBroker.Application.DTOs;
using DigitalBrooker.Domain.ValueObjects;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalBroker.Application.Commands
{
    public class CreatePostAndSellerRequestCommand : IRequest<Unit>
    {
        public PostRequest PostRequest { get; set; }
    }
}
