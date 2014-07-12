using System;

namespace Core.Algorithms.Array
{
    /// <summary>
    /// Using quicksort implementation, count the number 
    /// of exchanges depending on pivot element choosing
    /// </summary>
    public static class CountExchangesAlgorithm
    {
        public static long CountExchanges<T>(this T[] array, Func<T[], int, int, int> pivotSelectFunc) where T : IComparable<T>
        {
            long totalExchanges = 0;
            CountExchanges(array, 0, array.Length - 1, pivotSelectFunc, ref totalExchanges);
            return totalExchanges;
        }

        private static void CountExchanges<T>(this T[] array, int lo, int hi, Func<T[], int, int, int> pivotSelectFunc, ref long exchangesCount)
            where T : IComparable<T>
        {
            if (hi <= lo)
                return;

            checked { exchangesCount += (hi - lo); }

            int pivotIndex = array.Partition(lo, hi, pivotSelectFunc);

            array.CountExchanges(lo, pivotIndex - 1, pivotSelectFunc, ref exchangesCount);

            array.CountExchanges(pivotIndex + 1, hi, pivotSelectFunc, ref exchangesCount);
        }

        private static int Partition<T>(this T[] array, int lo, int hi, Func<T[], int, int, int> pivotSelectFunc)
            where T : IComparable<T>
        {
            int pivotIndex = pivotSelectFunc(array, lo, hi);

            array.Swap(lo, pivotIndex);
            T pivotElement = array[lo];

            int i = lo + 1;
            int j = lo;
            while (++j <= hi)
            {
                if (pivotElement.CompareTo(array[j]) > 0)
                {
                    array.Swap(i, j);
                    i++;
                }
            }
            pivotIndex = i - 1;
            array.Swap(lo, pivotIndex);
            return pivotIndex;
        }
    }
}