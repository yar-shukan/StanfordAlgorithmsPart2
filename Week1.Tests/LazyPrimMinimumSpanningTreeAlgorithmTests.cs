using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Core;
using Core.Algorithms.Graph;
using Core.DataStructures;
using NUnit.Framework;

namespace Week1.Tests
{
    [TestFixture]
    public class LazyPrimMinimumSpanningTreeAlgorithmTests
    {
        [Description("Question 3")]
        [Test]
        public void GetMinimumSpanningTree_InputFromQuestionFile_CorrectEdgeWeightSum()
        {
            long sum = GetGraphFromFile().GetMinimumSpanningTree().Sum(edge => (long)edge.Weight);
            Assert.AreEqual(-3612829, sum);
        }

        private static EdgeWeightedGraph GetGraphFromFile()
        {
            using (var fileStream = new FileStream("edges.txt", FileMode.Open, FileAccess.Read))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    int[] vertexEdgeCountPair = streamReader.ReadLine().SplitAsInts();
                    int vertexCount = vertexEdgeCountPair[0];
                    int edgeCount = vertexEdgeCountPair[1];
                    var graph = new EdgeWeightedGraph(vertexCount);
                    while (!streamReader.EndOfStream)
                    {
                        var edgeWeightTuple = streamReader.ReadLine().SplitAsInts();
                        graph.AddEdge(new Edge(edgeWeightTuple[0] - 1, edgeWeightTuple[1] - 1, edgeWeightTuple[2]));
                    }
                    Debug.Assert(graph.EdgeCount == edgeCount);
                    return graph;
                }
            }
        }
    }
}