namespace DigitalBrooker.Domain.ValueObjects
{
    public sealed class PaymentType
    {
        public string Value { get; }
        public PaymentType(string value)
        {
            Value = value;
        }
        public static PaymentType ListingFee => new("ListingFee");
        public static PaymentType DepositFee => new("DepositeFee");
        public static PaymentType ServiceFee => new("ServiceFee");
        public static PaymentType TourServiceFee => new("TourServiceFee");
        public static IEnumerable<PaymentType> List() => new[] {ListingFee, DepositFee, ServiceFee, TourServiceFee};
        public override string ToString()=>Value;
        public static implicit operator string(PaymentType type) => type.Value;
        public static PaymentType From(string value)
        {
            var type = List().FirstOrDefault(s=>s.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
            if (type == null)
                throw new ArgumentException($"Invalid payment type: {type}");
            return type;
        }
        public override bool Equals(object? obj) =>
            obj is PaymentType other && Value == other.Value;
        public override int GetHashCode()=> Value.GetHashCode();
    }
}
