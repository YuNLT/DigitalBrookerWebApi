using DigitalBroker.Application.Abstracts;
using DigitalBrooker.Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task CreatePostAndSellerRequestAsync(string address, decimal price, string description, byte[] image,
            string propertyType, string township, string title, Guid userId, DateTime appointmentDate, string requestStatusValue)
        {
            var property = await _propertyRepository.CreatePropertyAsync(address, price, description, image,
               propertyType, township, title, userId);
            Guid propertyId = property.Id;
            await _sellerRequestRepository.CrateSellerRequestAsync(userId, propertyId, appointmentDate,
                requestStatusValue);
        }
    }
}
