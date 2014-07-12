using System;
using System.Diagnostics;

namespace Core.Algorithms.Array
{
    public static class CountInversionsAlgorithm<T> where T : IComparable<T>
    {
        public static long CountInversions(T[] array)
        {
            return new InversionsCounter().CountInversions(array);
        }

        private class InversionsCounter
        {
            private long _inversionsCount;
            private T[] _aux;

            public long CountInversions(T[] array)
            {
                _aux = new T[array.Length];
                _inversionsCount = 0;

                Sort(array, 0, array.Length - 1);
                _aux = null;
                return _inversionsCount;
            }

            private void Merge(T[] array, int leftBound, int middle, int rightBound)
            {
                int i = leftBound;
                int j = middle + 1;
                int leftLength = (middle + 1);

                for (int k = leftBound; k <= rightBound; k++)
                    _aux[k] = array[k];

                for (int k = leftBound; k <= rightBound; k++)
                {
                    if (i > middle)
                    {
                        array[k] = _aux[j++];
                    }
                    else if (j > rightBound)
                    {
                        array[k] = _aux[i++];
                    }
                    else if (_aux[i].CompareTo(_aux[j]) > 0) //split inversion
                    {
                        array[k] = _aux[j++];
                        checked
                        {
                            int i1 = (leftLength - i);
                            Debug.Assert(i1 > 0);
                            _inversionsCount += i1;
                        }
                    }
                    else
                    {
                        array[k] = _aux[i++];
                    }
                }
            }

            private void Sort(T[] array, int leftBound, int rightBound)
            {
                if (rightBound <= leftBound)
                    return;

                int middle = leftBound + (rightBound - leftBound) / 2;

                Sort(array, leftBound, middle);

                Sort(array, middle + 1, rightBound);

                Merge(array, leftBound, middle, rightBound);
            }
        }
    }
}