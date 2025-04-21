using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.DTOs;

namespace DigitalBroker.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly ISellerRequestRepository _sellerRequestRepository;
        public PostService(IPropertyRepository propertyRepository, ISellerRequestRepository sellerRequestRepository)
        {
            _propertyRepository = propertyRepository;
            _sellerRequestRepository = sellerRequestRepository;
        }
        public async Task<List<Post>> GetAllPostAsync()
        {
            var properties = await _propertyRepository.GetAllPostAsync();

            var posts = properties.Select(p => new Post
            {
                PropertyViewId = p.PropertyViewId,
                Address = p.Address,
                Price = p.Price,
                Description = p.Description,
                Image = p.Image,
                PropertyTypeValue = p.PropertyTypeValue,
                Township = p.Township,
                Title = p.Title
            }).ToList();

            return posts;
        }

        public async Task CreatePostAndSellerRequestAsync(PostRequest request)
        {
            var property = await _propertyRepository.CreatePropertyAsync(request.Address, request.Price, 
                request.Description, request.Image,
               request.PropertyTypeValue, request.Township, request.Title, request.UserId);
            Guid propertyId = property.Id;
            await _sellerRequestRepository.CrateSellerRequestAsync(request.UserId, propertyId, request.AppointmentDate,
                request.RequestStatusValue);
        }
    }
}
