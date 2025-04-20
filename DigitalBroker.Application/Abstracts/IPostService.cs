using DigitalBrooker.Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Abstracts
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostAsync();
        Task CreatePostAndSellerRequestAsync(string address, decimal price, string description, byte[] image,
            string propertyType, string township, string title, Guid userId, DateTime appointmentDate, string requestStatusValue);
    }
}
