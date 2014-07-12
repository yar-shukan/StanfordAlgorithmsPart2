using System;
using System.Collections.Generic;

namespace Core
{
    public class MaxHeap<T> : Heap<T>
    {
        private static readonly Func<T, T, bool> CompareFunc;

        static MaxHeap()
        {
            CompareFunc = (x, y) => Comparer<T>.Default.Compare(x, y) < 0;
        }
         
        public MaxHeap()
            : base(CompareFunc)
        { }

        public T Max { get { return Extremum; } }

        public T RemoveMax()
        {
            return RemoveExtremum();
        }
    }
}