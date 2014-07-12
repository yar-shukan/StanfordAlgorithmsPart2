using System;

namespace Core
{
    public static class Extensions
    {
        public static void Swap<T>(this T[] array, int i, int j)
        {
            Swap(ref array[i], ref array[j]);
        }

        public static void AssertIsSorted<T>(this T[] array) where T : IComparable<T>
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i].CompareTo(array[i + 1]) > 0)
                    throw new ApplicationException("Array is not sorted properly");
            }
        }

        public static void Swap<T>(ref T first, ref T second)
        {
            T temp = first;
            first = second;
            second = temp;
        }

        public static bool Greater<T>(this T[] array, int i, int j) where T : IComparable<T>
        {
            return array[i].CompareTo(array[j]) > 0;
        }

        public static bool Less<T>(this T[] array, int i, int j) where T : IComparable<T>
        {
            return array[i].CompareTo(array[j]) < 0;
        }
    }
}