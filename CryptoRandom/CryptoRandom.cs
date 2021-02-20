using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CryptoRandom
{
    // https://docs.microsoft.com/en-us/archive/msdn-magazine/2007/september/net-matters-tales-from-the-cryptorandom
    // Adapted from above article - fulfills Random interface using the RNGCryptoServiceProvider
    public class CryptoRandom : Random
    {
        private RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();

        public CryptoRandom() { }
        public CryptoRandom(Int32 ignoredSeed) { }

        public override Int32 Next()
        {
            byte[] uint32Buffer = new byte[4];
            _rng.GetBytes(uint32Buffer);
            return BitConverter.ToInt32(uint32Buffer, 0) & 0x7FFFFFFF;
        }

        public override Int32 Next(Int32 maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException("maxValue");
            }

            return Next(0, maxValue);
        }

        public override Int32 Next(Int32 minValue, Int32 maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException("minValue");
            }

            if (minValue == maxValue)
            {
                return minValue;
            }

            Int64 diff = maxValue - minValue;
            Int64 max = (1 + (Int64)UInt32.MaxValue);
            byte[] uint32Buffer = new byte[4];
            while (true)
            {   
                _rng.GetBytes(uint32Buffer);
                UInt32 rand = BitConverter.ToUInt32(uint32Buffer, 0);

                Int64 remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (Int32)(minValue + (rand % diff));
                }
            }
        }

        public override double NextDouble()
        {
            double result;
            byte[] uint64Buffer = new byte[8];
            do
            {   
                _rng.GetBytes(uint64Buffer);
                result = (double)BitConverter.ToUInt64(uint64Buffer, 0) / ulong.MaxValue;
            }
            while (result == 1.0d);

            return result;
        }

        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            _rng.GetBytes(buffer);
        }

        protected override double Sample()
        {
            return NextDouble();
        }
    }
}
