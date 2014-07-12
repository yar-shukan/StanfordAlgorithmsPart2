namespace Core.DataStructures
{
    public class WeightedVertex
    {
        private readonly int _number;
        private readonly int _weight;

        public WeightedVertex(int number, int weight)
        {
            _number = number;
            _weight = weight;
        }

        public int Number { get { return _number; } }

        public int Weight { get { return _weight; } }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((WeightedVertex) obj);
        }

        protected bool Equals(WeightedVertex other)
        {
            return _number == other._number && _weight == other._weight;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_number * 397) ^ _weight;
            }
        }

    }
}