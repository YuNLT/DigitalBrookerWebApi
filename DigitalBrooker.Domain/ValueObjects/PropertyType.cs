using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.ValueObjects
{
    public sealed class PropertyType
    {
        public string Value { get; }
        public PropertyType(string value) { Value = value; }
        public static PropertyType House => new("House");
        public static PropertyType Yard => new("Yard");
        public static PropertyType HouseAndYard => new("HouseAndYard");
        public static IEnumerable<PropertyType> List() => new[] { House, Yard, HouseAndYard };
        public override string ToString() => Value;
        public static implicit operator string(PropertyType type) => type.Value;

        public static PropertyType From(string value)
        {
            var type = List().FirstOrDefault(t => t.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
            if (type == null)
                throw new ArgumentException($"Invalid BuyerRequestType: {value}");

            return type;
        }
        public override bool Equals(object obj) =>
        obj is PropertyType other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
