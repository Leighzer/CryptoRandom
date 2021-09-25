using CryptoRandomLibrary;
using System;

namespace CryptoRandomExample
{
    class Program
    {
        static void Main(string[] args)
        {
            CryptoRandom random = new CryptoRandom();
            Console.WriteLine($"Hello {random.Next().ToString()}!");
        }
    }
}
