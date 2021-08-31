namespace GettingStarted.Crypto
{
    public class Transaction
    {
        public Address From { get; }
        public Address To { get; }
        public int Amount { get; }
        public string Hash { get; }

        public Transaction(Address from, Address to, int amount, string hash) =>
            (From, To, Amount, Hash) = (from, to, amount, hash);
    }
}