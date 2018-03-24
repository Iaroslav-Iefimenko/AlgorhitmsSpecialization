using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorhitmsSpecialization
{
    public class C1W4KargerGraf
    {
        class Vertex
        {
            public int Num { get; set; }
            public List<int> Links { get; set; } = new List<int>();
        }

        class Edge
        {
            public int Tail { get; set; }
            public int Head { get; set; }
        }

        struct Subset
        {
            public int Parent;
            public int Rank;
        }

        public void Execute()
        {
            var edgeList = new List<Edge>();
            var vertexList = new List<Vertex>();
            var vertexStrings = File.ReadAllLines(
                @"D:\Solutions\Coursera\AlgorhitmsSpecialization\AlgorhitmsSpecialization\kargerMinCut.txt");
            foreach (var str in vertexStrings)
            {
                var arr = str.Split('\t');
                int tail = int.Parse(arr[0]) - 1;
                var vertex = new Vertex { Num = tail };
                for (int i = 1; i < arr.Length; i++)
                {
                    if (string.IsNullOrEmpty(arr[i]))
                    {
                        continue;
                    }

                    int head = int.Parse(arr[i]) - 1;
                    vertex.Links.Add(head);
                    if (!edgeList.Any(x => x.Head == tail && x.Tail == head))
                    {
                        edgeList.Add(new Edge { Head = head, Tail = tail });
                    }
                }
                vertexList.Add(vertex);
            }

            List<int> results = new List<int>();
            for (int i = 0; i < 40000; i++)
            {
                int num = kargerMinCut(vertexList, edgeList, i);
                results.Add(num);
            }
            int minNum = results.Min();

            Console.ReadLine();
        }

        // A utility function to find set of an element i
        // (uses path compression technique)
        private int Find(Subset[] subsets, int i)
        {
            // find root and make root as parent of i
            // (path compression)
            if (subsets[i].Parent != i)
              subsets[i].Parent = Find(subsets, subsets[i].Parent);
 
            return subsets[i].Parent;
        }

        // A function that does union of two sets of x and y
        // (uses union by rank)
        private void Union(Subset[] subsets, int x, int y)
        {
            int xroot = Find(subsets, x);
            int yroot = Find(subsets, y);

            // Attach smaller rank tree under root of high
            // rank tree (Union by Rank)
            if (subsets[xroot].Rank<subsets[yroot].Rank)
                subsets[xroot].Parent = yroot;
            else if (subsets[xroot].Rank > subsets[yroot].Rank)
                subsets[yroot].Parent = xroot;
 
            // If ranks are same, then make one as root and
            // increment its rank by one
            else
            {
                subsets[yroot].Parent = xroot;
                subsets[xroot].Rank++;
            }
        }

        private int kargerMinCut(List<Vertex> vList, List<Edge> eList, int seed)
        {
            // Get data of given graph
            int V = vList.Count, E = eList.Count;
            
            // Allocate memory for creating V subsets.
            var subsets = new Subset[V];
 
            // Create V subsets with single elements
            for (int v = 0; v<V; ++v)
            {
                subsets[v].Parent = v;
                subsets[v].Rank = 0;
            }

            // Initially there are V vertices in
            // contracted graph
            int vertices = V;

            Random rnd = new Random(seed);

            // Keep contracting vertices until there are
            // 2 vertices.
            while (vertices > 2)
            {
                // Pick a random edge
                int i = rnd.Next(E - 1);

                // Find vertices (or sets) of two corners
                // of current edge
                int subset1 = Find(subsets, eList[i].Tail);
                int subset2 = Find(subsets, eList[i].Head);
 
               // If two corners belong to same subset,
               // then no point considering this edge
               if (subset1 == subset2)
                 continue;
 
               // Else contract the edge (or combine the
               // corners of edge into one vertex)
               else
               {
                    //Console.WriteLine($"Contracting edge {eList[i].Tail}-{eList[i].Head}");
                    vertices--;
                    Union(subsets, subset1, subset2);
                }
            }
 
            // Now we have two vertices (or subsets) left in
            // the contracted graph, so count the edges between
            // two components and return the count.
            int cutedges = 0;
            for (int i = 0; i<E; i++)
            {
                int subset1 = Find(subsets, eList[i].Tail);
                int subset2 = Find(subsets, eList[i].Head);
                if (subset1 != subset2)
                {
                    cutedges++;
                }
            }
 
            return cutedges;
        }
    }
}
