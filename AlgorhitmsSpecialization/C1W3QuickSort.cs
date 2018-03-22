using System;
using System.IO;
using System.Numerics;
using System.Linq;

namespace AlgorhitmsSpecialization
{
    enum PivotType {
        FirstElement,
        LastElement,
        MedianOfThree
    }

    public static class IntArrayExtension
    {
        public static void Swap(this int[] arr, long first, long second)
        {
            int length = arr.Length;
            if (first < 0 || first > length - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(first));
            }

            if (second < 0 || second > length - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(second));
            }

            int temp = arr[first];
            arr[first] = arr[second];
            arr[second] = temp;
        }
    }

    class C1W3QuickSort
    {
        public void Execute()
        {
            var intStrings = File.ReadAllLines(
                @"D:\Solutions\AlgorhitmsSpecialization\AlgorhitmsSpecialization\QuickSort.txt");
            int[] ints = Array.ConvertAll(intStrings, int.Parse); // { 1, 3, 5, 2, 4, 6 };
            int length = ints.Length;
            Console.WriteLine($"Length: {length}");

            BigInteger comparisonsCnt = 0;
            int[] ints1 = new int[length];
            Array.Copy(ints, ints1, length);
            QuickSortWithCnt(ref ints1, 0, length - 1, PivotType.FirstElement, ref comparisonsCnt);
            Console.WriteLine($"Comparisons count for first element pivot: {comparisonsCnt}");

            BigInteger comparisonsCnt2 = 0;
            int[] ints2 = new int[length];
            Array.Copy(ints, ints2, length);
            QuickSortWithCnt(ref ints2, 0, length - 1, PivotType.LastElement, ref comparisonsCnt2);
            Console.WriteLine($"Comparisons count for last element pivot: {comparisonsCnt2}");

            BigInteger comparisonsCnt3 = 0;
            int[] ints3 = new int[length];
            Array.Copy(ints, ints3, length);
            QuickSortWithCnt(ref ints3, 0, length - 1, PivotType.MedianOfThree, ref comparisonsCnt3);
            Console.WriteLine($"Comparisons count for median of three element pivot: {comparisonsCnt3}");

            Console.ReadLine();
        }

        private void QuickSortWithCnt(
            ref int[] ints,
            int firstInx,
            int lastInx,
            PivotType pType,
            ref BigInteger cCnt)
        {
            if (firstInx == lastInx)
            {
                return;
            }

            if (firstInx + 1 == lastInx)
            {
                cCnt++;
                if (ints[firstInx] > ints[lastInx])
                {
                    ints.Swap(firstInx, lastInx);
                }

                return;
            }


            cCnt += lastInx - firstInx;
            int length = lastInx - firstInx + 1;

            int pivot = 0;
            switch (pType)
            {
                case PivotType.FirstElement: { pivot = ints[firstInx]; break; }
                case PivotType.LastElement: { pivot = ints[lastInx]; ints.Swap(firstInx, lastInx); break; }
                case PivotType.MedianOfThree:
                    {
                        long medianInx = firstInx + length / 2 + length % 2 - 1;
                        int[] pivots = { ints[firstInx], ints[medianInx], ints[lastInx] };
                        pivots = pivots.OrderBy(n => n).ToArray();
                        pivot = pivots[1];
                        if (pivot == ints[lastInx])
                        {
                            ints.Swap(firstInx, lastInx);
                        }

                        if (pivot == ints[medianInx])
                        {
                            ints.Swap(firstInx, medianInx);
                        }

                        break;
                    }
            }

            int i = firstInx + 1;
            for (int j = i; j <= lastInx; j++)
            {
                if (ints[j] < pivot)
                {
                    ints.Swap(i, j);
                    i++;
                }
            }

            ints.Swap(firstInx, i - 1);
            int pivotInx = i - 1;
            QuickSortWithCnt(ref ints, firstInx, pivotInx > firstInx ? pivotInx - 1 : pivotInx, pType, ref cCnt);
            QuickSortWithCnt(ref ints, pivotInx < lastInx ? pivotInx + 1 : pivotInx, lastInx, pType, ref cCnt);
        }
    }
}
