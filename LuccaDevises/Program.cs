using System;

namespace LuccaDevises
{

    class Program
    {
        public class Algorithms
        {
            public List<T> ShortestPathFunction<T>(Graph<T> graph, T start, T end)
            {
                var path = new List<T>();
                var alreadyVisitedNearValue = new Dictionary<T, T>();

                var queue = new Queue<T>();
                queue.Enqueue(start);

                while(queue.Count > 0)
                {
                    var currentVertex = queue.Dequeue();
                    foreach(var nearValue in graph.NearValueList[currentVertex])
                    {
                        if (alreadyVisitedNearValue.ContainsKey(nearValue))
                            continue;

                        alreadyVisitedNearValue[nearValue] = currentVertex;
                        queue.Enqueue(nearValue);
                    }
                }

                var current = end;
                while(!current.Equals(start))
                {
                    path.Add(current);
                    current = alreadyVisitedNearValue[current];
                }
                path.Add(start);
                path.Reverse();

                return path;
            }
        }

        public class Graph<T>
        {
            public Dictionary<T, HashSet<T>> NearValueList { get; } = new Dictionary<T, HashSet<T>>();

            public Graph() { }
            public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges)
            {
                foreach (var vertex in vertices)
                    AddVertex(vertex);

                foreach (var edge in edges)
                    AddEdge(edge);
            }

           
            public void AddVertex(T vertex)
            {
                NearValueList[vertex] = new HashSet<T>();
            }

            public void AddEdge(Tuple<T, T> edge)
            {
                if (NearValueList.ContainsKey(edge.Item1) && NearValueList.ContainsKey(edge.Item2))
                {
                    NearValueList[edge.Item1].Add(edge.Item2);
                    NearValueList[edge.Item2].Add(edge.Item1);
                }
            }
        }
        static void Main(string[] args)
        {
            string[] firstArgs = args[0].Split(';');
            string start = firstArgs[0];
            Double initialAmount = Double.Parse(firstArgs[1]);
            string end = firstArgs[2];

            Int32 devisesTabLength = Int32.Parse(args[1]);
            string[][] devisesTab = new string[devisesTabLength][];

            var vertices = new List<string>();
            var edges = new List<Tuple<string, string>>();
            for (int i = 0; i < devisesTabLength; i++)
            {
                devisesTab[i] = args[i + 2].Split(";").ToArray();
                vertices.Add(devisesTab[i][0]);
                vertices.Add(devisesTab[i][1]);
                edges.Add(Tuple.Create(devisesTab[i][0], devisesTab[i][1]));
            }
            vertices = vertices.Distinct().ToList();
            var algorithms = new Algorithms();
            
            var graph = new Graph<string>(vertices, edges);

            var shortestPath = algorithms.ShortestPathFunction(graph, start, end);

            Console.WriteLine(string.Join(", ", shortestPath));

            /*test2.ForEach(i => Console.WriteLine(i[0]));*/

            /*var vertices = new[] { devisesTab.Select(i => i.Split(";")) }*/

        }
    }
}
