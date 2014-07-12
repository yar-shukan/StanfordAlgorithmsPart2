using System;
using System.Collections;
using System.Collections.Generic;

namespace Core
{
    public class MinPriorityQueue<TPriority, TValue> : IEnumerable<KeyValuePair<TPriority, TValue>>
        where TPriority : IComparable<TPriority>
    {
        private KeyValuePair<TPriority, TValue>[] _keys;
        private int _count;

        public MinPriorityQueue(int capacity = 16)
        {
            _keys = new KeyValuePair<TPriority, TValue>[capacity + 1];
        }

        public bool IsEmpty()
        {
            return _count == 0;
        }

        public KeyValuePair<TPriority, TValue> Min
        {
            get
            {
                if (IsEmpty())
                    throw new Exception("Queue is empty");
                return _keys[1];
            }
        }

        public void Insert(KeyValuePair<TPriority, TValue> elem)
        {
            if (_count == _keys.Length - 1)
                Resize(2 * _keys.Length);

            _keys[++_count] = elem;
            Swim(_count);
        }

        public KeyValuePair<TPriority, TValue> RemoveMin()
        {
            if (IsEmpty())
                throw new Exception("Queue is empty.");

            _keys.Swap(1, _count);
            KeyValuePair<TPriority, TValue> min1 = _keys[_count--];
            Sink(1);
            _keys[_count + 1] = default(KeyValuePair<TPriority, TValue>);
            KeyValuePair<TPriority, TValue> min = min1;

            if (_count > 0 && _count == (_keys.Length - 1) / 4)
                Resize(_keys.Length / 2);

            AssertIsMinHeap(1);
            return min;
        }

        public void DecreaseKey(TPriority newKey, TValue value)
        {
            if (IsEmpty())
                throw new Exception("Queue is empty.");
            int index = -1;
            for (int i = 1; i <= _count; i++)
            {
                if (_keys[i].Value.Equals(value))
                {
                    index = i;
                    break;
                }
            }
            if (index < 1)
                throw new ArgumentException();
            if (_keys[index].Key.CompareTo(newKey) <= 0)
                throw new ArgumentException();
            _keys[index] = new KeyValuePair<TPriority, TValue>(newKey, value);
            Swim(index);
            AssertIsMinHeap(1);
        }

        private IEnumerable<T> Traverse(Node<T> node)
        {
            if (node.left != null)
                yield return Traverse(node.left);
            yield return Traverse(node);
            if (node.right != null)
                yield return Traverse(node.right);
        }

        private void Heapify()
        {
            var newQueue = new MinPriorityQueue<TPriority, TValue>(_count);
            for (int i = 1; i <= _count; i++)
            {
                newQueue.Insert(_keys[i]);
            }
            _keys = newQueue._keys;
        }

        private void Sink(int index)
        {
            while (index * 2 <= _count)
            {
                int childIndex = index * 2;
                if (childIndex < _count && Greater(childIndex, childIndex + 1))
                    ++childIndex;
                if (!Greater(index, childIndex))
                    break;
                _keys.Swap(index, childIndex);
                index = childIndex;
            }
        }

        private bool Greater(int i, int j)
        {
            return _keys[i].Key.CompareTo(_keys[j].Key) > 0;
        }

        private void Swim(int index)
        {
            while (index > 1 && Greater(index / 2, index))
            {
                _keys.Swap(index, index / 2);
                index /= 2;
            }
        }

        private void Resize(int newCapacity)
        {
            var keys = new KeyValuePair<TPriority, TValue>[newCapacity];
            for (int i = 1; i <= _count; i++)
            {
                keys[i] = _keys[i];
            }
            _keys = keys;
        }

        public IEnumerator<KeyValuePair<TPriority, TValue>> GetEnumerator()
        {
            var copy = new MinPriorityQueue<TPriority, TValue>(_count);
            for (int i = 1; i <= _count; i++)
            {
                copy._keys[i] = _keys[i];
            }
            for (int i = 1; i <= copy._count; i++)
            {
                yield return copy.RemoveMin();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void AssertIsMinHeap(int rootIndex)
        {
            if (rootIndex > _count)
                return;
            int left = rootIndex*2;
            int right = rootIndex*2 + 1;
            if (left <= _count && Greater(rootIndex, left))
                throw new Exception("Not right");
            if (right <= _count && Greater(rootIndex, right))
                throw  new Exception("Not right");
            AssertIsMinHeap(left);
            AssertIsMinHeap(right);
        }
    }
}