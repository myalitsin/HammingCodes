namespace HammingCodes
{
    public static class Hamming
    {
        //2^m-1, 2^m-1-m

        public static HammingCode Create3X1()
        {
            // m=2, generating polynomial x^2+x+1 as binary 111 and as hex - 7
            return new HammingCode(0x7, 1);
        }

        public static HammingCode Create7X4()
        {
            // m=3, generating polynomial x^3+x+1 as binary 1011 and as hex - B
            return new HammingCode(0xB, 4);
        }

        public static HammingCode Create15X11()
        {
            // m=4, generating polynomial x^4+x+1 as binary 10011 as hex - 13
            return new HammingCode(0x13, 11);
        }

        public static HammingCode Create24X16()
        {
            // generating polynomial x^8+x^4+x+1 as binary 1 0001 0011 and as hex - 113
            return new HammingCode(0x113, 16);
        }
        public static HammingCode Create31X26()
        {
            // m=5, generating polynomial x^5+x^2+1 as binary 100101 as hex - 25
            return new HammingCode(0x25, 26);
        }

        public static HammingCode Create63X57()
        {
            // m=6, generating polynomial x^6+x+1 as binary 1000011 as hex - 43
            return new HammingCode(0x43, 57);
        }

        public static HammingCode Create(long generatingPolynomial, int informationBitsNumber)
        {
            return new HammingCode(generatingPolynomial, informationBitsNumber);
        }
    }
}
