using DigitalBrooker.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Abstracts
{
    public interface ISellerRequestRepository
    {
        Task<List<SellerRequest>> GetAllSellerRequestAsync();
        Task<List<SellerRequest>> GetPendingSellerRequest();
        Task<string> CrateSellerRequestAsync(Guid propertyId, Guid userId, DateTime appointmentDate,
            string status);
    }
}
