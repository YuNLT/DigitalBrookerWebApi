using DigitalBroker.Application.Abstracts;
using DigitalBrokker.Infrastructure.DbContext;
using DigitalBrooker.Domain.Entities.Models;
using DigitalBrooker.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DigitalBrokker.Infrastructure.Repositories
{
    public class SellerRequestRepository : ISellerRequestRepository
    {
        private readonly ApplicationDbContext _context;
        public SellerRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<SellerRequest>> GetAllSellerRequestAsync()
        {
            List<SellerRequest> sellerRequest = await _context.SellerRequests.ToListAsync();
            return sellerRequest;
        }

        public async Task<List<SellerRequest>> GetPendingSellerRequest()
        {
            List<SellerRequest> sellerRequest = await _context.SellerRequests.Where(s=>
            s.Status == RequestStatus.From("Pending")).ToListAsync();
            return sellerRequest;
        }
        public async Task<string> CrateSellerRequestAsync(Guid propertyId, Guid userId,DateTime appointmentDate,
            string status)
        {
            try
            {
                var appointmentStatus = RequestStatus.From(status);
                var sellerRequest = new SellerRequest
                {
                    PropertyId = propertyId,
                    UserId = userId,
                    AppointmentDate = appointmentDate,
                    Status = appointmentStatus
                };
                _context.Add(sellerRequest);
                await _context.SaveChangesAsync();
                return "PostRequest Successfully";
            }
            catch (Exception ex)
            {
                return $"Error on Requesting Post:{ex.Message}";
            }
        }
    }
}
