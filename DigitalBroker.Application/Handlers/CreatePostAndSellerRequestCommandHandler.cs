using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using MediatR;

namespace DigitalBroker.Application.Handlers
{
    public class CreatePostAndSellerRequestCommandHandler : IRequestHandler<CreatePostAndSellerRequestCommand, Unit>
    {
        private readonly IPostService _postService;
        public CreatePostAndSellerRequestCommandHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<Unit> Handle(CreatePostAndSellerRequestCommand request, CancellationToken cancellationToken)
        {
            await _postService.CreatePostAndSellerRequestAsync(request.PostRequest);
            return Unit.Value;
        }
    }
}
