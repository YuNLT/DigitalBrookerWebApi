using Microsoft.AspNetCore.Http;

namespace DigitalBrooker.Domain.Entities.Request
{
    public class PostRequest
    {
        public required string Address { get; set; }//From Property Table
        public decimal Price { get; set; }
        public required string Description { get; set; }
        public IFormFile Image { get; set; } //use IFile for GraphQL
        public string PropertyTypeValue { get; set; } 
        public required string Township { get; set; }
        public required string Title { get; set; }
        public Guid UserId { get; set; }
        public DateTime AppointmentDate { get; set; }//From seller request Table
        public string RequestStatusValue { get; set; } 
    }
}
