using System.Collections.Generic;

namespace Core.DataStructures
{
    public class WeightedGraph
    {
        internal readonly List<WeightedVertex>[] Vertices;

        public WeightedGraph(int verticesCount)
        {
            Vertices = new List<WeightedVertex>[verticesCount + 1];
            for (int i = 1; i < Vertices.Length; i++)
            {
                Vertices[i] = new List<WeightedVertex>();
            }
        }

        public void AddAdj(int vertex, WeightedVertex adj)
        {
            Vertices[vertex].Add(adj);
        }

        public List<WeightedVertex> this[int vertex]
        {
            get { return Vertices[vertex]; }
        } 

        public int VerticesCount { get { return Vertices.Length - 1; } }
    }
}