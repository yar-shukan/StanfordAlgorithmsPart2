using System.Collections.Generic;
using System.Linq;
using Core.DataStructures;

namespace Core.Algorithms.Graph
{
    public static class DijkstrasAlgorithm
    {
        private const int MaxDistance = int.MaxValue;

         public static int[] GetShortestPathes(this WeightedGraph graph, int startVertex)
         {
             var distances = new int[graph.VerticesCount + 1];
             var visited = new bool[graph.VerticesCount + 1];
             for (int i = 1; i < distances.Length; i++)
             {
                 if (i != startVertex)
                    distances[i] = MaxDistance;
             }
             var queue = new MinPriorityQueue<int, int>(graph.VerticesCount);
             for (int i = 1; i <= graph.VerticesCount; i++)
             {
                 if (i == startVertex)
                     queue.Insert(new KeyValuePair<int, int>(0, i));
                 else
                     queue.Insert(new KeyValuePair<int, int>(MaxDistance, i));
             }

             while (!queue.IsEmpty())
             {
                 var min = queue.RemoveMin();
                 var distance = min.Key;
                 var vertexNumber = min.Value;
                 visited[vertexNumber] = true;

                 foreach (WeightedVertex adj in graph[vertexNumber].Where(adj => !visited[adj.Number]))
                 {
                     int score = distance + adj.Weight;
                     if (score < distances[adj.Number])
                     {
                         distances[adj.Number] = score;
                         queue.DecreasePriority(adj.Number, score);
                     }
                 }
             }
             return distances;
         }
    }
}