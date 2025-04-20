using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using MediatR;

namespace DigitalBroker.Application.Handlers
{
    public class CreatePostAndSellerRequestCommandHandler : IRequestHandler<CreatePostAndSellerRequestCommand, string>
    {
        private readonly IPostService _postService;
        public CreatePostAndSellerRequestCommandHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<string> Handle(CreatePostAndSellerRequestCommand request, CancellationToken cancellationToken)
        {
            await _postService.CreatePostAndSellerRequestAsync(request.Address, request.Price, request.Description,
                request.Image, request.PropertyTypeValue, request.Township, request.Title, request.UserId, request.AppointmentDate,
                request.RequestStatusValue);
            return "Post Requested Successfully";
        }
    }
}
