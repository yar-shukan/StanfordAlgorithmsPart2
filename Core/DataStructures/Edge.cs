using System;

namespace Core.DataStructures
{
    public class Edge 
    {
        private readonly int _first; 
        private readonly int _second; 

        public Edge(int first, int second, int weight)
        {
            _first = first;
            _second = second;
            Weight = weight;
        }

        public int Weight { get; private set; }

        public int FirstVertex { get { return _first; } }

        public int SecondVertex { get { return _second; }}

        public int Other(int vertex)
        {
            if (_first == vertex)
            {
                return _second;
            }
            if (_second == vertex)
            {
                return _first;
            }
            throw new ArgumentException();
        }

        protected bool Equals(Edge other)
        {
            return (_first == other._first && _second == other._second) ||
                   (_first == other._second && _second == other._first);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Edge)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return _first ^ _second;
            }
        }
    }
}