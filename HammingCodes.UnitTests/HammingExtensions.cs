using System;
using System.Security.Cryptography;

namespace HammingCodes.UnitTests
{
    public static class HammingExtensions
    {
        public static bool Check(this HammingCode h, int checkCount = 10000)
        {
            for (int i = 0; i < checkCount; i++)
            {
                var information = GenerateRandomNumber(h.InformationBitsMask);
                var parity = h.Encode(information);
                var codeword = information + (parity << h.InformationBitsNumber);
                var error = GenerateRandomError(h.CodewordBitsNumber);
                var codewordWithError = (codeword ^ error);
                var decoded = h.Decode(codewordWithError);
                if ((decoded & h.InformationBitsMask) != information)
                {
                    return false;//fail
                }
            }
            return true;//ok
        }

        private static long GenerateRandomNumber(long mask)
        {
            return BitConverter.ToInt64(GenerateRandomBytes(8), 0) & mask;
        }

        private static long GenerateRandomError(int infoLength)
        {
            return (long)1 << (GenerateRandomBytes(1)[0] % infoLength);
        }

        private static byte[] GenerateRandomBytes(int count)
        {
            var bytes = new byte[count];
            _cryptoProvider.GetBytes(bytes);
            return bytes;
        }

        private static readonly RNGCryptoServiceProvider _cryptoProvider = new RNGCryptoServiceProvider((CspParameters)null);
    }
}
