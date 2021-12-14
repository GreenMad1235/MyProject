using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGrey
{
    class Program
    {

        public static ulong GrayEncode(ulong n) 
        {
            return n ^ (n >> 1);
        }

        public static ulong GrayDecode(ulong n)
        {
            ulong i = 1 << 8 * 64 - 2;
            ulong p,
            b = p = n & i;

            while ((i >>= 1) > 0)
            {
                b |= p = n & i ^ p >> 1;
            }
            return b;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Number\tBinary\tGray\tDecoded");
            for (ulong i = 0; i < 32; i++)
            {
                Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", i, Convert.ToString((long)i, 2), Convert.ToString((long)GrayEncode(i), 2), GrayDecode(GrayEncode(i))));
                Console.ReadKey();
            }
        }
    }
}
