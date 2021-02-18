using System;
using System.Security.Cryptography;

namespace CryptoRandom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CryptoRandom random = new CryptoRandom();
            Console.WriteLine($"Hello {random.Next().ToString()}!");
        }
    }
}
