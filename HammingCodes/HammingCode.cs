using System;
using System.Collections.Generic;

namespace HammingCodes
{
    public class HammingCode
    {
        internal HammingCode(long generatingPolynomial, int informationBitsNumber)
        {
            if (generatingPolynomial <= 0)
                throw new ArgumentException("generatingPolynomial must be greater than 0", "generatingPolynomial");

            GeneratingPolynomial = generatingPolynomial;
            GeneratingPolynomialMask = MakePolynomialMaskAndCalculateParityBitsNumber(GeneratingPolynomial);
            InformationBitsNumber = informationBitsNumber;
            InformationBitsMask = GenerateMask(InformationBitsNumber);
            ParityBitsMask = GenerateMask(ParityBitsNumber);
            CodewordBitsMask = GenerateMask(CodewordBitsNumber);

            // prepare syndromes
            _syndromes = new Dictionary<long, long> { { 0, 0 } };
            long information = -1 & InformationBitsMask;
            long codeword = information + (Encode(information) << InformationBitsNumber);
            for (int i = 0; i < CodewordBitsNumber; i++)
            {
                long correction = (long)1 << i;
                long codewordWithError = codeword ^ correction;
                long parity = codewordWithError >> InformationBitsNumber;
                long error = parity ^ Encode(codewordWithError);
                if (_syndromes.ContainsKey(error))
                    throw new ArgumentException("generatingPolynomial invalid", "generatingPolynomial");
                _syndromes.Add(error, correction);
            }
        }

        public int CodewordBitsNumber => InformationBitsNumber + ParityBitsNumber;
        public long CodewordBitsMask { get; }
        public long GeneratingPolynomial { get; }
        public long GeneratingPolynomialMask { get; }
        public int InformationBitsNumber { get; }
        public long InformationBitsMask { get; }
        public int ParityBitsNumber { private set; get; }
        public long ParityBitsMask { get; }

        private readonly Dictionary<long, long> _syndromes;

        public long? Decode(long codeword)
        {
            long parity = (codeword >> InformationBitsNumber) & ParityBitsMask;
            long error = parity ^ Encode(codeword);
            if (_syndromes.TryGetValue(error, out long correction))
            {
                return codeword ^ correction;
            }
            return null;
        }

        public long Encode(long information)
        {
            long info = information & InformationBitsMask;
            long poly = GeneratingPolynomial;
            long polyMask = GeneratingPolynomialMask;
            long mask = 1L;

            int i;
            for (i = 0; i < InformationBitsNumber; i++)
            {
                // division - bit by bit XOR
                //
                info ^= poly & (((info & mask) == 0) ? 0x00000000 : polyMask);

                mask <<= 1;
                poly <<= 1;

                polyMask <<= 1;
            }

            return ((~(info >> i)) & ParityBitsMask);
        }

        private static long GenerateMask(int length)
        {
            long mask = 1;
            for (int i = 1; i < length; i++)
            {
                mask = (mask << 1) + 1;
            }
            return mask;
        }

        private long MakePolynomialMaskAndCalculateParityBitsNumber(long data)
        {
            long mask = 1;
            int controlLengthInBits = 0;
            while (data > 1)
            {
                data >>= 1;
                controlLengthInBits += 1;
                mask = (mask << 1) + 1;
            }
            ParityBitsNumber = controlLengthInBits;
            return mask;
        }
    }
}
