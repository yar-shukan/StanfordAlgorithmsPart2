using System.Collections.Generic;

namespace Core
{
    public class Vertex
    {
        private readonly int _number;
        private readonly int _weight;

        public Vertex(int number, int weight)
        {
            _number = number;
            _weight = weight;
        }

        public int Number { get { return _number; } }

        public int Weight { get { return _weight; } }

        public override bool Equals(object obj)
        {
            var other = obj as Vertex;
            if (other == null)
                return false;
            return _number == other._number;
        }
    }

    public class WeightedGraph
    {
        internal readonly List<Vertex>[] Vertices;

        public WeightedGraph(int verticesCount)
        {
            Vertices = new List<Vertex>[verticesCount + 1];
            for (int i = 1; i < Vertices.Length; i++)
            {
                Vertices[i] = new List<Vertex>();
            }
        }

        public void AddAdj(int vertex, Vertex adj)
        {
            Vertices[vertex].Add(adj);
        }

        public List<Vertex> this[int vertex]
        {
            get { return Vertices[vertex]; }
        } 

        public int VerticesCount { get { return Vertices.Length - 1; } }
    }
}