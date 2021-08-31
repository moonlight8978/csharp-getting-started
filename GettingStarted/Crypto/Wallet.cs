using System;

namespace GettingStarted.Crypto
{
    public enum NotificationTypes
    {
        Received,
        Sent
    }

    public class SuccessEventArgs : EventArgs
    {
        public Address Target { get; }
        public int Amount { get; }

        public NotificationTypes Context { get; }

        public SuccessEventArgs(Address target, int amount, NotificationTypes context) =>
            (Target, Amount, Context) = (target, amount, context);
    }

    public class Wallet
    {
        private static int autoincrement;

        public Address Address { get; }

        private int PrivateKey;

        public event EventHandler<SuccessEventArgs> Success;


        public Wallet(string address)
        {
            Address = new Address(address);
            PrivateKey = autoincrement++;
        }

        protected void onSuccess(SuccessEventArgs e)
        {
            Success?.Invoke(this, e);
        }

        public void notifySuccess(SuccessEventArgs e)
        {
            onSuccess(e);
        }

        public void Transfer(Wallet to, int amount, Blockchain blockchain)
        {
            string signature = Sign(to.Address, amount);
            Transaction tx = new Transaction(this.Address, to.Address, amount, signature);
            blockchain.append(tx, OnMined);
            
            void OnMined() {
                SuccessEventArgs e = new SuccessEventArgs(to.Address, amount, NotificationTypes.Sent);
                notifySuccess(e);

                e = new SuccessEventArgs(this.Address, amount, NotificationTypes.Received);
                to.notifySuccess(e);
            };
        }

        private string Sign(Address to, int amount)
        {
            long timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            return $"{Address}.{PrivateKey}.{to}.{amount}.#{timestamp}";
        }
    }
}