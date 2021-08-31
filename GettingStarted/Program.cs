using System;
using GettingStarted.Crypto;

namespace GettingStarted
{
    class Program
    {
        static void walletOnSuccess(object subject, SuccessEventArgs e)
        {
            Wallet owner = (Wallet) subject;
            Console.WriteLine(e.Context == NotificationTypes.Received
                ? $"{owner.Address} received from {e.Target}: {e.Amount}"
                : $"{owner.Address} sent to {e.Target}: {e.Amount}");
        }

        static void Main(string[] args)
        {
            Wallet alice = new Wallet("0xdAC17F958D2ee523a2206206994597C13D831ec7");
            alice.Success += walletOnSuccess;
            Wallet bob = new Wallet("0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2");
            bob.Success += walletOnSuccess;
            Wallet carlos = new Wallet("0xbb4CdB9CBd36B01bD1cBaEBF2De08d9173bc095c");
            carlos.Success += walletOnSuccess;
            Blockchain blockchain = new Blockchain(alice, 10_000_000);
            alice.Transfer(bob, 3_000_000, blockchain);
            alice.Transfer(bob, 2_000_000, blockchain);
            alice.Transfer(carlos, 3_000_000, blockchain);
            carlos.Transfer(alice, 1_000_000, blockchain);
            carlos.Transfer(bob, 1_000_000, blockchain);
            bob.Transfer(alice, 1_000_000, blockchain);
            Console.WriteLine(blockchain.BlockNumber);
        }
    }
}
