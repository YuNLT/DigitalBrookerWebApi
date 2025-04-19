using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Exception;
using DigitalBroker.Application.Querirs;
using DigitalBrooker.Domain.Entities.Request;
using MediatR;

namespace DigitalBroker.Application.Handlers
{
    public class GetAllPostQueryHandler : IRequestHandler<GetAllPostQuery, List<Post>>
    {
        private readonly IPostService _postService;
        public GetAllPostQueryHandler(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<List<Post>> Handle(GetAllPostQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postService.GetAllPostAsync();
            if(posts == null)
            {
                throw new NoPostFoundException("No posts found.");
            }
            return posts;
        }
    }
}
