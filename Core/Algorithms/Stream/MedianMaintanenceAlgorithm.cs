using System.Collections.Generic;
using Core.DataStructures;

namespace Core.Algorithms.Stream
{
    public static class MedianMaintanenceAlgorithm
    {
        public static int GetMedianModulusSum(this IEnumerable<int> inputStream, int mediansCount, int modBase)
        {
            int sum = 0;
            var leftValues = new MaxHeap<int>();
            leftValues.Insert(int.MinValue);
            var rightValues = new MinHeap<int>();
            rightValues.Insert(int.MaxValue);
            int k = 0;
            foreach (int value in inputStream)
            {
                if (k++ > mediansCount)
                    break;

                if (value > leftValues.Max)
                    rightValues.Insert(value);
                else
                    leftValues.Insert(value);

                if (leftValues.Count - rightValues.Count == 2)
                    rightValues.Insert(leftValues.RemoveMax());
                else if (rightValues.Count - leftValues.Count == 2)
                    leftValues.Insert(rightValues.RemoveMin());

                if (k % 2 == 1)
                {
                    if (leftValues.Count > rightValues.Count)
                        checked
                        {
                            sum += leftValues.Max;
                        }
                        
                    else
                        checked
                        {
                            sum += rightValues.Min;
                        }
                        
                }
                else
                    checked
                    {
                        sum += leftValues.Max;
                    }
            }
            return sum % modBase;
        }
    }
}