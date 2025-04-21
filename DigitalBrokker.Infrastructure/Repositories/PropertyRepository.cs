using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.DTOs;
using DigitalBroker.Application.Exception;
using DigitalBrokker.Infrastructure.DbContext;
using DigitalBrooker.Domain.Entities.Models;
using DigitalBrooker.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DigitalBrokker.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PropertyRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<Property>> GetAllPostAsync()
        {
            List<Property> posts = await _applicationDbContext.Properties
                .Where(p=>p.IsVerify==true).ToListAsync();
            return posts;
        }
        public async Task<List<Property>> GetPostByTownship(string township)
        {
            List<Property> posts = await _applicationDbContext.Properties
                .Where(p => p.IsVerify == true && p.IsActive== true && p.Township == township).ToListAsync();
            return posts;
        }
        public async Task<List<Property>> GetPostByUserId(Guid userId)
        {
            List<Property> posts = await _applicationDbContext.Properties
                .Where(p => p.IsVerify == true && p.IsActive == true && p.UserId == userId).ToListAsync();
            return posts;
        }

        public async Task<string> GetLatestPropertyViewIdAsync()
        {// Get all IDs that match the pattern
            var validIds = await _applicationDbContext.Properties
                .Where(p => p.PropertyViewId.StartsWith("PVD-"))
                .Select(p => p.PropertyViewId)
                .ToListAsync();

            int maxNumber = 0;

            foreach (var id in validIds)
            {
                var parts = id.Split('-');
                if (parts.Length == 2 && int.TryParse(parts[1], out int number))
                {
                    if (number > maxNumber)
                        maxNumber = number;
                }
            }

            // Generate next ID
            int nextNumber = maxNumber + 1;
            return $"PVD-{nextNumber:D8}";
        }
        public async Task<Property> CreatePropertyAsync(string address, decimal price, string description, IFormFile image,
            string propertyTypeValue, string township, string title, Guid userId)
        {
            var propertyViewId = await GetLatestPropertyViewIdAsync();
            //Create a new PropertyType opject and set it as PropertyType in model
            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            var imageByte = ms.ToArray();
            var property = new Property
            {
                PropertyViewId = propertyViewId,
                Address = address,
                Price = price,
                Description = description,
                Image = imageByte,
                PropertyTypeValue = propertyTypeValue,
                Township = township,
                Title = title,
                UserId = userId
            };
            if(property == null)
            {
                throw new PropertyCreationException("Property Creation falied");
            }
            _applicationDbContext.Properties.Add(property);
            await _applicationDbContext.SaveChangesAsync();
            return property;
        }
    }
}
