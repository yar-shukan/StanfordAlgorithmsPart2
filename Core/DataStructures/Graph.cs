using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.DataStructures
{
    public class Graph : IEnumerable<List<int>>
    {
        internal readonly List<int>[] _adj;
        internal readonly Random _random;
        public const int Deleted = -1;

        public Graph(int vertexCount)
        {
            _adj = new List<int>[vertexCount];
            for (int i = 0; i < _adj.Length; i++)
            {
                _adj[i] = new List<int>();
            }
            VertexCount = vertexCount;
            _random = new Random();
        }

        public int VertexCount { get; set; }

        public void AddAdj(int vertex, int adj)
        {
            this[vertex].Add(adj);
        }

        public void ContractRandomEdge()
        {
            int v = GetRandomVertex();
            int w = GetRandomNeighbor(v);
            if (v == w)
                throw new Exception();
            Merge(v, w);
        }

        private int GetRandomVertex()
        {
            int randomVertex;
            do
            {
                randomVertex = _random.Next(1, _adj.Length);
            } while (this[randomVertex] == null || this[randomVertex].All(vertex => vertex == Deleted));

            return randomVertex;
        }

        private int GetRandomNeighbor(int vertex)
        {
            int randomNeighbor;
            do
            {
                randomNeighbor = _random.Next(0, this[vertex].Count);
            } while (this[vertex][randomNeighbor] == Deleted);
            return this[vertex][randomNeighbor];
        }

        internal List<int> this[int vertex]
        {
            get { return _adj[vertex - 1]; }
            set { _adj[vertex - 1] = value; }
        }

        public void Merge(int first, int second)
        {
            int indexOfSecond = this[first].IndexOf(second);
            this[first][indexOfSecond] = Deleted;

            this[first].AddRange(this[second]);
            foreach (int adjVertex in this[second].Where(vertex => vertex != Deleted))
            {
                int indexOfMerged = this[adjVertex].IndexOf(second);
                this[adjVertex][indexOfMerged] = first;
            }
            List<int> mergedAdj = this[first];
            for (int i = 0; i < mergedAdj.Count; i++)
            {
                if (mergedAdj[i] == first) // self-loop
                    mergedAdj[i] = Deleted;
            }
            this[second] = null;
            VertexCount--;
        }

        public void InitFromStream(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                while (true)
                {
                    var currentLine = reader.ReadLine();
                    if (currentLine == null)
                        return;
                    var verteces = currentLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();
                    AddAdj(verteces[0], verteces[1]);
                }
            }
        }

        public Graph Copy()
        {
            var copy = new Graph(VertexCount);
            for (int i = 0; i < VertexCount; i++)
            {
                copy._adj[i].AddRange(_adj[i]);
            }
            return copy;
        }

        public IEnumerator<List<int>> GetEnumerator()
        {
            return ((IEnumerable<List<int>>)_adj).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}