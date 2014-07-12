using System;
using System.Collections.Generic;

namespace Core.DataStructures
{
    public class EdgeWeightedGraph
    {
        private readonly List<Edge>[] _adj;
        private readonly HashSet<Edge> _edges;

        public EdgeWeightedGraph(int vertexCount)
        {
            _adj = new List<Edge>[vertexCount];
            for (int v = 0; v < vertexCount; v++)
            {
                _adj[v] = new List<Edge>();
            }
            _edges = new HashSet<Edge>();
        }

        public int EdgeCount { get { return _edges.Count; } }

        public int VertexCount { get { return _adj.Length; } }

        public IReadOnlyList<Edge> Adj(int v)
        {
            return _adj[v];
        }

        public IEnumerable<Edge> Edges
        {
            get { return _edges; }
        }

        public IReadOnlyList<Edge> this[int vertex]
        {
            get { return _adj[vertex]; }
        }

        public void AddEdge(Edge e)
        {
            if (_edges.Contains(e))
            {
                throw new ArgumentException(
                    string.Format("Edge ({0}, {1}) was already added to the graph. " +
                "Current implementation doesn't allow self-loops", e.FirstVertex, e.SecondVertex));
            }
            _adj[e.FirstVertex].Add(e);
            _adj[e.SecondVertex].Add(e);
            _edges.Add(e);
        }
    }
}