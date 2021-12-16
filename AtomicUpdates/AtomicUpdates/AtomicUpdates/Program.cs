using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AtomicUpdates
{
    class Bucket
    {
        Random rand = new Random();
        int[] Buckets;
        object[] locks;
        public int BucketCount { get; set; }
        
        public Bucket(int bucketcount)
        {
            BucketCount = bucketcount;
            Buckets = new int[bucketcount];
            locks = new object[bucketcount];
            int startingtotal = 0;
            for(int i = 0; i <BucketCount; i++)
            {
                locks[i] = new object();
                Buckets[i] = rand.Next(30);
                startingtotal += Buckets[i];
            }
            Console.WriteLine("Starting total: " + startingtotal);
        }

        public int GetBucketValue(int i)
        {
            return Buckets[i];
        }
        public void Transfer(int i, int j, int amount)
        {
            if (i > BucketCount || j > BucketCount
                || i < 0 || j < 0 || i == j || amount < 0)
                return;

            lock(locks[Math.Min(i,j)])
                lock (locks[Math.Max(i, j)])
                {
                    amount = Math.Min(amount, Buckets[i]);

                    Buckets[i] -= amount;
                    Buckets[j] += amount;
                }
        }

        public void PrintBuckets()
        {
            int couter = 0;
            for (int i = 0; i < BucketCount; i++)
            {
                Monitor.Enter(locks[i]);
                Console.Write(Buckets[i] + " ");
                couter += Buckets[i];
            }
            Console.Write("= " + couter);
            Console.WriteLine();

            foreach (var l in locks)
                Monitor.Exit(l);
        }
    }

    class Program
    {
        static Bucket TSBs;

        public static void Main()
        {
            //Create the thread-safe bucket list
            TSBs = new Bucket(10);
            TSBs.PrintBuckets();
            //Create and start the Equalizing Thread
            new Thread(new ThreadStart(EqualizerThread)).Start();
            Thread.Sleep(1);
            //Create and start the Randamizing Thread
            new Thread(new ThreadStart(RandomizerThread)).Start();
            //Use this thread to do the printing
            PrinterThread();
        }
        //EqualizerThread runs on it's own thread and randomly averages two buckets
        static void EqualizerThread()
        {
            Random rand = new Random();
            while (true)
            {
                //Pick two buckets
                int b1 = rand.Next(TSBs.BucketCount);
                int b2 = rand.Next(TSBs.BucketCount);
                //Get the difference
                int diff = TSBs.GetBucketValue(b1) - TSBs.GetBucketValue(b2);
                //Transfer to equalize
                if (diff < 0)
                    TSBs.Transfer(b2, b1, -diff / 2);
                else
                    TSBs.Transfer(b1, b2, diff / 2);
            }
        }
        //RandomizerThread redistributes the values between two buckets
        static void RandomizerThread()
        {
            Random rand = new Random();
            while (true)
            {
                int b1 = rand.Next(TSBs.BucketCount);
                int b2 = rand.Next(TSBs.BucketCount);
                int diff = rand.Next(TSBs.GetBucketValue(b1));
                TSBs.Transfer(b1, b2, diff);
            }
        }
        //PrinterThread prints the current bucket contents
        static void PrinterThread()
        {
            while (true)
            {
                Thread.Sleep(50); //Only print every few milliseconds to let the other threads work
                TSBs.PrintBuckets();
            }
        }
    }
}
