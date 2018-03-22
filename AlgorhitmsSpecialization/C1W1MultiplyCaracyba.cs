using System;
using System.Numerics;

namespace AlgorhitmsSpecialization
{
    public class C1W1MultiplyCaracyba
    {
        public void Execute()
        {
            string aStr = "3141592653589793238462643383279502884197169399375105820974944592";
            string bStr = "2718281828459045235360287471352662497757247093699959574966967627";
            Console.WriteLine($"A value: {aStr}, length: {aStr.Length}");
            Console.WriteLine($"B value: {bStr}, length: {bStr.Length}");
            BigInteger a = BigInteger.Parse(aStr);
            BigInteger b = BigInteger.Parse(bStr);
            BigInteger correctVal = a * b;
            BigInteger calculatedVal = Multiply(aStr, bStr);
            Console.WriteLine($"Result comparison: {correctVal == calculatedVal}");
            Console.WriteLine($"Result: {correctVal}");
            Console.ReadLine();
        }

        private BigInteger Multiply(string aStr, string bStr)
        {
            int m = aStr.Length / 2;
            if (m == 1)
            {
                return BigInteger.Parse(aStr) * BigInteger.Parse(bStr);
            }

            string a1str = aStr.Substring(0, m);
            string a0str = aStr.Substring(m, m);
            string b1str = bStr.Substring(0, m);
            string b0str = bStr.Substring(m, m);

            BigInteger a0 = BigInteger.Parse(a0str);
            BigInteger a1 = BigInteger.Parse(a1str);
            BigInteger b0 = BigInteger.Parse(b0str);
            BigInteger b1 = BigInteger.Parse(b1str);

            BigInteger a0b0 = Multiply(a0str, b0str);
            BigInteger a1b1 = Multiply(a1str, b1str);
            BigInteger sdv = BigInteger.Pow(10, m);

            return a0b0 + ((a0 + a1) * (b0 + b1) - a0b0 - a1b1) * sdv + a1b1 * BigInteger.Pow(sdv, 2);
        }
    }
}
