using System;
using System.Collections.Generic;
using System.Linq;

namespace GettingStarted.Crypto
{
    public delegate void OnMined();
    
    class Block
    {
        public string PrevBlockHash { get; set; }

        public Transaction[] Transactions;

        public string Hash
        {
            get
            {
                List<string> hashs = new List<string>() { PrevBlockHash };
                foreach (Transaction tx in Transactions)
                {
                    hashs.Add(tx.Hash);
                }

                return String.Join(".", hashs);
            }
        }

        public Block(Transaction[] txs) => Transactions = txs;
    }
    
    public class Blockchain
    {
        private List<Block> _blocks =  new List<Block>();

        public Blockchain(Wallet owner, int totalSupply)
        {
            Wallet blackhole = new Wallet("0x0000000000000000000000000000000000000000");
            blackhole.Transfer(owner, totalSupply, this);
        }

        public void append(Transaction tx, OnMined onMined)
        {
            // TODO: Verify the transaction (balance, double spend, ...)
            Transaction[] txs = new Transaction[] { tx };
            Block block = new Block(txs);
            mine(block);
            onMined();
        }

        private void mine(Block block)
        {
            block.PrevBlockHash = LastBlock?.Hash ?? "0x0000000000000000000000000000000000000000";
            _blocks.Add(block);
        }
    
        #nullable enable
        private Block? LastBlock => _blocks.LastOrDefault<Block>();
        #nullable disable

        public int BlockNumber => _blocks.Count;
        
    }
}