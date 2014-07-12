using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Algorithms.Graph
{
    static class SCCAlgorithm
    {
        public static Dictionary<int, List<int>> GetStrongConnectedComponentsSizes(this DataStructures.Graph graph)
        {
            var dfsVisiter = new DfsVisiter(graph.ReverseEdges());

            for (int i = graph.VertexCount; i >= 1; i--)
            {
                if (!dfsVisiter.IsVisited(i))
                {
                    dfsVisiter.DfsVisit(i, -1, true);
                }
            }
            Console.WriteLine("Visited reversed");
            dfsVisiter.AssertEveryIsVisited();
            dfsVisiter.AssertFinishingTime();
            var finishingTime = dfsVisiter.FinishingTime;
           
            dfsVisiter = new DfsVisiter(graph);
            for (int i = graph.VertexCount; i >= 1; i--)
            {
                var leader = finishingTime[i];
                if (!dfsVisiter.IsVisited(leader))
                {
                    dfsVisiter.DfsVisit(leader, leader, false);
                }
            }
            dfsVisiter.AssertEveryIsVisited();
            return dfsVisiter.Sccs;
        }

        private class DfsVisiter
        {
            private readonly DataStructures.Graph _graph;
            internal readonly bool[] _isVisited;
            private int _previousFinishingTime;
            public int[] FinishingTime { get; private set; }
            public Dictionary<int, List<int>> Sccs { get; private set; }

            public DfsVisiter(DataStructures.Graph graph)
            {
                _graph = graph;
                _isVisited = new bool[_graph.VertexCount];
                FinishingTime = new int[_graph.VertexCount + 1];
                Sccs = new Dictionary<int, List<int>>();
            }

            public bool IsVisited(int vertex)
            {
                return _isVisited[vertex - 1];
            }

            public void DfsVisit(int startVertex, int leader, bool needCallStack)
            {
                var stack = new Stack<int>();

                if (leader >= 1 && !Sccs.ContainsKey(leader))
                {
                    Sccs[leader] = new List<int>();
                }

                stack.Push(startVertex);

                var callStack = new Stack<int>();
                var finishingTime = 0;

                while (stack.Count != 0)
                {
                    var currentVertex = stack.Pop();
                    if (IsVisited(currentVertex))
                        continue;
                    if (leader >= 1)
                    {
                        Sccs[leader].Add(currentVertex);
                    }
                    _isVisited[currentVertex - 1] = true;
                    callStack.Push(currentVertex);

                    foreach (int adj in _graph[currentVertex])
                    {
                        if (!IsVisited(adj))
                        {
                            if (needCallStack)
                            {
                                callStack.Push(currentVertex);
                            }
                            stack.Push(adj);
                        }
                    }

                    if (!needCallStack)
                        continue;

                    while (true)
                    {
                        if (callStack.Count != 0)
                            currentVertex = callStack.Pop();
                        else
                            break;
                        bool vertexIsFinished = _graph[currentVertex].All(IsVisited);
                        bool endVertex = _graph[currentVertex].Count == 0;
                        if (endVertex || vertexIsFinished)
                        {
                            FinishingTime[++finishingTime + _previousFinishingTime] = currentVertex;
                            while (callStack.Count != 0 && currentVertex == callStack.Peek())
                            {
                                callStack.Pop();
                            }
                            continue;
                        }
                        callStack.Push(currentVertex);
                        break;
                    }
                }
                _previousFinishingTime += finishingTime;
                if (_previousFinishingTime > _graph.VertexCount)
                    throw new Exception();
            }

            public void AssertEveryIsVisited()
            {
                if (_isVisited.Any(isVisited => !isVisited))
                {
                    throw new Exception();
                }
            }

            public void AssertFinishingTime()
            {
                for (int i = 1; i <= _graph.VertexCount; i++)
                {
                    if (FinishingTime[i] == 0)
                        throw new Exception("NO  FOR " + i);
                }
            }
        }
    }
}
