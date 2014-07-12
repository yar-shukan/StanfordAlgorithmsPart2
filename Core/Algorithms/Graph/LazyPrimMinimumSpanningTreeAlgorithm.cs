using System;
using System.Collections.Generic;
using System.Linq;
using Core.DataStructures;

namespace Core.Algorithms.Graph
{
    public static class LazyPrimMinimumSpanningTreeAlgorithm
    {
        public static IReadOnlyList<Edge> GetMinimumSpanningTree(this EdgeWeightedGraph graph)
        {
            return new SpanningTreeBuilder(graph).GetMinimumSpanningTree();
        }

        class SpanningTreeBuilder
        {
            private readonly EdgeWeightedGraph _graph;
            private readonly List<Edge> _spanningTree;
            private readonly bool[] _visited;
            private readonly MinPriorityQueue<int, Edge> _minPriorityQueue;

            public SpanningTreeBuilder(EdgeWeightedGraph graph)
            {
                _graph = graph;
                _spanningTree = new List<Edge>(_graph.VertexCount);
                _visited = new bool[_graph.VertexCount];
                _minPriorityQueue = new MinPriorityQueue<int, Edge>(_graph.EdgeCount);
            }

            public List<Edge> GetMinimumSpanningTree()
            {
                int start = _graph.Edges.First().FirstVertex;
                Visit(start);
                while (!_minPriorityQueue.IsEmpty())
                {
                    Edge minWeightEdge = _minPriorityQueue.RemoveMin().Value;
                    if (_visited[minWeightEdge.FirstVertex] && _visited[minWeightEdge.SecondVertex])
                    {
                        continue;
                    }
                    _spanningTree.Add(minWeightEdge);
                    VisitIfNotVisited(minWeightEdge.FirstVertex);
                    VisitIfNotVisited(minWeightEdge.SecondVertex);
                }
                if (_spanningTree.Count != _graph.VertexCount - 1)
                {
                    throw new ApplicationException();
                }
                return _spanningTree;
            }

            private void VisitIfNotVisited(int vertex)
            {
                if (!_visited[vertex])
                {
                    Visit(vertex);
                }
            }

            private void Visit(int vertex)
            {
                if (_visited[vertex])
                {
                    throw new InvalidOperationException();
                }
                _visited[vertex] = true;
                foreach (Edge edge in _graph[vertex])
                {
                    if (!_visited[edge.Other(vertex)])
                    {
                        _minPriorityQueue.Insert(edge.Weight, edge);
                    }
                }
            }
        }
    }
}