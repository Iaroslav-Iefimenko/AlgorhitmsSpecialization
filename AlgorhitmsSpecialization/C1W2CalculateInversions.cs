using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AlgorhitmsSpecialization
{
    class C1W2CalculateInversions
    {
        public void Execute()
        {
            var intStrings = File.ReadAllLines(
                @"D:\Solutions\AlgorhitmsSpecialization\AlgorhitmsSpecialization\IntegerArray.txt");
            int[] ints = Array.ConvertAll(intStrings, int.Parse);// { 1, 3, 5, 2, 4, 6 };
            Console.WriteLine($"Length: {ints.Length}");

            /* BigInteger inv_count = 0;            
            for (int i = 0; i < ints.Length - 1; i++)
                for (int j = i + 1; j < ints.Length; j++)
                    if (ints[i] > ints[j])
                        inv_count++;
            Console.WriteLine($"Inversions correct count: {inv_count}"); */
            // 2407905288

            BigInteger invCnt = 0;
            var res = MergeSortWithInvCnt(ints, ref invCnt);
            Console.WriteLine($"Inversions count: {invCnt}");
            Console.ReadLine();
        }

        private int[] MergeSortWithInvCnt(int[] arr, ref BigInteger invCnt)
        {
            var n = arr.Length;
            if (n < 2)
            {
                return arr;
            }

            var m = n / 2;
            var arr1 = arr.Take(m).ToArray();
            var arr2 = arr.Skip(m).Take(n - m).ToArray();

            arr1 = MergeSortWithInvCnt(arr1, ref invCnt);
            arr2 = MergeSortWithInvCnt(arr2, ref invCnt);

            int i = 0, j = 0;
            int[] res = new int[n];
            for (int k = 0; k < n; k++)
            {
                if (i < m && j < n - m)
                {
                    if (arr1[i] <= arr2[j])
                    {
                        res[k] = arr1[i];
                        i++;
                    }
                    else
                    {
                        res[k] = arr2[j];
                        j++;
                        invCnt += (m - i);
                    }
                }
                else if (i == m)
                {
                    res[k] = arr2[j];
                    j++;
                }
                else if (j == n - m)
                {
                    res[k] = arr1[i];
                    i++;
                }
            }
            return res;
        }
    }
}
