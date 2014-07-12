using Core.DataStructures;

namespace Core
{
    public static class GraphExtensions
    {
        public static Graph ReverseEdges(this Graph graph)
        {
            var reversed = new Graph(graph.VertexCount);
            for (int i = 1; i <= graph.VertexCount; i++)
            {
                foreach (int vertex in graph[i])
                {
                    reversed.AddAdj(vertex, i);
                }
            }
            return reversed;
        }
    }
}