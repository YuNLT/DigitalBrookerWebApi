using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.DTOs
{
    public record PostRequest
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
