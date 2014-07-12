using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Algorithms.Graph
{
    static class MinCutAlgorithm
    {
        public static int GetMinCut(this DataStructures.Graph graph)
        {
            var repeatCount = (int)Math.Ceiling(graph.VertexCount * Math.Log(graph.VertexCount)*10);
            int minResult = int.MaxValue;
            object lockObj = new object();
            Parallel.For(0, repeatCount, (i) =>
                {
                    int currentResult = GetMinCutRandomly(graph.Copy());
                    lock (lockObj)
                    {
                        if (currentResult < minResult)
                            minResult = currentResult;
                    }
                });
            return minResult;
        }

        private static int GetMinCutRandomly(DataStructures.Graph graph)
        {
            while (graph.VertexCount > 2)
            {
                graph.ContractRandomEdge();
            }
            var cuts = graph.Where(adjList => adjList != null).ToList();
            if (cuts.Count != 2)
                throw new Exception();
            int firstCount = cuts[0].Count(vertex => vertex != DataStructures.Graph.Deleted);

            int secondCount = cuts[1].Count(vertex => vertex != DataStructures.Graph.Deleted);
            if (firstCount != secondCount)
                throw new Exception();
            return firstCount;
        }
    }

    class Program
    {
        static void Main()
        {
            //var Graph = new Graph(200);
            //Graph.InitFromStream(File.OpenRead("input.txt"));

            //int minCut = Graph.GetMinCut();
            //Console.WriteLine(minCut);
        }
    }
}