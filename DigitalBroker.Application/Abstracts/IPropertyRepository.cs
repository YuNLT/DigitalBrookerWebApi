using DigitalBrooker.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Abstracts
{
    public interface IPropertyRepository 
    {
        Task<List<Property>> GetAllPostAsync();
        Task<List<Property>> GetPostByTownship(string township);
        Task<List<Property>> GetPostByUserId(Guid userId);
        Task<Property> CreatePropertyAsync(string address, decimal price, string description, byte[] image,
            string propertyType, string township, string title, Guid userId);
    }
}
