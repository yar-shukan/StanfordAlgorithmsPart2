using System.Collections.Generic;
using System.Diagnostics;

namespace Core.Algorithms.Array
{
    static class TwoSumAlgorithm
    {
        public static int TwoSumCountBetween(this int[] array, int lower, int higher)
        {
            var hashSet = new HashSet<int>();
            foreach (int t in array)
            {
                if (t <= higher)
                    hashSet.Add(t);
            }
            var lookedUpSet = new HashSet<int>();
            foreach (int t in hashSet)
            {
                int start = lower - t;
                int end = higher - t;
                int currentLookingUp = start - 1;
                Debug.Assert(currentLookingUp <= end);
                while (++currentLookingUp <= end)
                {
                    bool isLooked = lookedUpSet.Contains(t + currentLookingUp);
                    if (currentLookingUp != t && !isLooked && hashSet.Contains(currentLookingUp))
                    {
                        lookedUpSet.Add(t + currentLookingUp);
                    }
                }
            }
            return lookedUpSet.Count;
        }
    }
}