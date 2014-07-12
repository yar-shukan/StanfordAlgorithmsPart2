using System;
using System.Collections.Generic;

namespace Core
{
    public class MinHeap<T> : Heap<T>
    {
        private static readonly Func<T, T, bool> CompareFunc;

        static MinHeap()
        {
            CompareFunc = (x, y) => Comparer<T>.Default.Compare(x, y) > 0;
        }

        public MinHeap()
            : base(CompareFunc)
        { }

        public T Min { get { return Extremum; } }

        public T RemoveMin()
        {
            return RemoveExtremum();
        }
    }
}