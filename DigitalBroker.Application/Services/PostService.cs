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
        public PostService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
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
    }
}
