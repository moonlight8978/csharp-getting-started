namespace GettingStarted.Crypto
{
    public class Address
    {
        public string Value { get; }

        public Address(string address) => Value = address;

        public static bool operator ==(Address addr1, Address addr2) => addr1.Value.Equals(addr2.Value);

        public static bool operator !=(Address addr1, Address addr2) => !addr1.Value.Equals(addr2.Value);

        public override string ToString()
        {
            return this.Value;
        }
    }
}