using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.ValueObjects
{
    public sealed class RequestStatus
    {
        public string Value { get; }
        private RequestStatus(string value) { Value = value; }
        public static RequestStatus Pending => new("Pending");
        public static RequestStatus Accepted => new("Accepted");
        public static RequestStatus Rejected => new("Rejected");
        public static IEnumerable<RequestStatus> List() => new[] {Pending, Accepted, Rejected};
        public override string ToString()=>Value;
        public static implicit operator string(RequestStatus value) => value.Value;
        public static RequestStatus From(string value)
        {
            var status = List().FirstOrDefault(s => s.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
            if (status == null)
                throw new ArgumentException($"Invalid BuyerRequestStatus: {value}");

            return status;
        }
        public override bool Equals(object obj) =>
            obj is RequestStatus other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
