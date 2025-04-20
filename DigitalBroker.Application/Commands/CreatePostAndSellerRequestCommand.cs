using DigitalBrooker.Domain.ValueObjects;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalBroker.Application.Commands
{
    public class CreatePostAndSellerRequestCommand : IRequest<string>
    {
        public string Address { get; set; }//From Property Table
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string PropertyTypeValue { get; set; }
        public string Township { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public DateTime AppointmentDate { get; set; }//From seller request Table
        public string RequestStatusValue { get; set; }
        public CreatePostAndSellerRequestCommand(string address, decimal price, string description, 
            byte[] image, string propertyTypeValue, string township, string title, Guid userId, 
            DateTime appointmentDate, string requestStatusValue)
        {
            Address = address;
            Price = price;
            Description = description;
            Image = image;
            PropertyTypeValue = propertyTypeValue;
            Township = township;
            Title = title;
            UserId = userId;
            AppointmentDate = appointmentDate;
            RequestStatusValue = requestStatusValue;
        }
    }
}
