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

            if (args == null || args.Length == 0)
            {
                Console.WriteLine("This has no argument");
                Console.ReadKey();
                return;
            }

            StreamReader src = new StreamReader(args[0]);
            List<string> arguments = new List<string>();
            while (!src.EndOfStream)
            {
                string line = src.ReadLine();
                arguments = line.Split(' ').ToList();
            }

            string[] firstArgs = arguments[0].Split(';');
            string start = firstArgs[0];
            decimal amount = decimal.Parse(firstArgs[1], System.Globalization.CultureInfo.InvariantCulture);
            string end = firstArgs[2];

            Int32 devisesTabLength = Int32.Parse(arguments[1]);
            string[][] devisesTab = new string[devisesTabLength][];

            var vertices = new List<string>();
            var edges = new List<Tuple<string, string>>();
            for (int i = 0; i < devisesTabLength; i++)
            {
                devisesTab[i] = arguments[i + 2].Split(";").ToArray();
                vertices.Add(devisesTab[i][0]);
                vertices.Add(devisesTab[i][1]);
                edges.Add(Tuple.Create(devisesTab[i][0], devisesTab[i][1]));
            }
            vertices = vertices.Distinct().ToList();
            var algorithms = new Algorithms();
            
            var graph = new Graph<string>(vertices, edges);

            var shortestPath = algorithms.ShortestPathFunction(graph, start, end);

            decimal changeRate = 1;

            for (int i = 0; i < shortestPath.Count - 1; i++)
            {
                 var conversionTab = devisesTab.FirstOrDefault(d => d[0] == shortestPath[i] && d[1] == shortestPath[i + 1]);
                if (conversionTab != null)
                    changeRate = Math.Round(changeRate * decimal.Parse(conversionTab[2], System.Globalization.CultureInfo.InvariantCulture), 4);
                else
                {
                    conversionTab = devisesTab.FirstOrDefault(d => d[1] == shortestPath[i] && d[0] == shortestPath[i + 1]);
                    if (conversionTab != null)
                        changeRate = changeRate * Math.Round(1 / decimal.Parse(conversionTab[2], System.Globalization.CultureInfo.InvariantCulture), 4);
                    else throw new Exception("Information insuffisante pour permettre le calcul du changement de devise");
                }
            }

            Console.WriteLine(Math.Round(amount * changeRate));

        }
    }
}
