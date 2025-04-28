namespace DigitalBrooker.Domain.ValueObjects
{
    public sealed class BuyerRequestType
    {
        public string Value { get; }
        public BuyerRequestType(string value)
        {
            Value = value;
        }
        public static BuyerRequestType Tour => new("Tour");
        public static BuyerRequestType Purchase => new("Purchase");
        public static IEnumerable<BuyerRequestType> List() => new[] { Tour, Purchase };
        public override string ToString() => Value;
        public static implicit operator string(BuyerRequestType type) => type.Value;
        public static BuyerRequestType From(string value)
        {
            var type = List().FirstOrDefault(t => t.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
            if (type == null)
                throw new ArgumentException($"Invalid BuyerRequestType: {value}");

            return type;
        }
        public override bool Equals(object obj) =>
            obj is BuyerRequestType other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
