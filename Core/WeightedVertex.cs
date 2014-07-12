namespace Core
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
            var other = obj as WeightedVertex;
            if (other == null)
                return false;
            return _number == other._number;
        }
    }
}