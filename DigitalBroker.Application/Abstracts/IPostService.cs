using DigitalBroker.Application.DTOs;

namespace DigitalBroker.Application.Abstracts
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostAsync();
        Task CreatePostAndSellerRequestAsync(PostRequest request);
    }
}
