using System;
using System.Diagnostics;

namespace Core.DataStructures
{
    public abstract class Heap<T>
    {
        private T[] _keys;
        private int _count;
        private readonly Func<T, T, bool> _compareFunc;

        protected Heap(Func<T, T, bool> compareFunc)
        {
            _keys = new T[16];
            _compareFunc = compareFunc;
        }

        protected T Extremum
        {
            get
            {
                ThrowIfEmpty();
                return _keys[1];
            }
        }

        protected T RemoveExtremum()
        {
            ThrowIfEmpty();
            T extremum = _keys[1];
            _keys.Swap(1, _count--);
            Sink(1);
            _keys[_count + 1] = default(T);

            if (_count > 0 && (_count == (_keys.Length - 1) / 4))
                Resize(_keys.Length / 2);

            return extremum;
        }

        private void Sink(int index)
        {
            while (index * 2 <= _count)
            {
                int childIndex = index * 2;
                if (childIndex < _count && CompareKeys(childIndex, childIndex + 1))
                    childIndex++;
                if (!CompareKeys(index, childIndex))
                    break;
                _keys.Swap(index, childIndex);
                index = childIndex;
            }
        }

        public bool IsEmpty()
        {
            return _count == 0;
        }

        public int Count
        {
            get { return _count; }
        }

        public void Insert(T element)
        {
            if (_count == _keys.Length - 1)
                Resize(2 * _keys.Length);

            _keys[++_count] = element;
            Swim(_count);
        }

        private void ThrowIfEmpty()
        {
            if (IsEmpty())
                throw new InvalidOperationException();
        }

        private void Swim(int index)
        {
            while (index > 1 && CompareKeys(index / 2, index))
            {
                _keys.Swap(index, index / 2);
                index /= 2;
            }
        }

        private void Resize(int newCapacity)
        {
            var keys = new T[newCapacity];
            for (int i = 1; i <= _count; i++)
            {
                keys[i] = _keys[i];
            }
            _keys = keys;
        }

        private bool CompareKeys(int index1, int index2)
        {
           return _compareFunc(_keys[index1], _keys[index2]);
        }

        public static void AssertIsCorrectlyImplemented(bool isForMaxHeap)
        {
            var random = new Random();
            const int length = 1000000;
            Heap<int> heap;
            if (isForMaxHeap)
                heap = new MaxHeap<int>();
            else
                heap = new MinHeap<int>();
            for (int i = 0; i < length; i++)
            {
                heap.Insert(random.Next(1, length / 10));
            }
            for (int i = 0; i < length - 1; i++)
            {
                if (random.Next(0, 11) >= 5)
                    heap.RemoveExtremum();
                else
                    heap.Insert(random.Next(0, length / 10));
            }

            int current = heap.Extremum;
            while (!heap.IsEmpty())
            {
                if (isForMaxHeap)
                    Debug.Assert(current >= heap.Extremum);
                else
                    Debug.Assert(current <= heap.Extremum);
                current = heap.RemoveExtremum();
            }
        }
    }
}