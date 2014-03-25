using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace outrun
{
    class Program
    {
        private static Node CurrentMaxNode;


        static void Main(string[] args)
        {
            Console.WriteLine("Hit it! tidididimdimdim-dim-dim");
            
            var outrun = BuildTree(@"C:\Users\verne_000\Desktop\tree.txt");
            
            CurrentMaxNode = outrun[0];

            var sw = new Stopwatch();
            sw.Start();
            TurboTurbo(outrun, null, 0);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "ms");
            
            Console.Write("\nTykkäyksiä reitillä: ");
            PrintTurbo(outrun, CurrentMaxNode);
            Console.WriteLine("\nTykkäyksien lukumäärä: " + CurrentMaxNode.Weight);
            
            Console.Write("\n\nThe end...");
            Console.ReadLine();
        }


        /// <summary>
        /// Create an arraylist containing the nodes. Sort of resembles a min heap
        /// </summary>
        /// <returns></returns>
        private static List<Node> BuildTree(String filepath)
        {
            var foo = new List<Node>();
            Int32 i = 1;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();  // Skip header             
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var nodes = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var n in nodes)
                    {
                        foo.Add(new Node { Value = Convert.ToInt32(n), Level = i });
                    }
                    i++;
                }
            }
            Console.WriteLine("\n\nLevels in tree: " + (i - 1) + " total nodes: " + foo.Count);
            return foo;
        }


        /// <summary>
        /// Calculate the longest path. The last node in the longest path is stored in currentlongestpath
        /// </summary>
        /// <param name="outrun"></param>
        /// <param name="parentindex"></param>
        /// <param name="index"></param>
        /// <param name="parentweight"></param>
        private static void TurboTurbo(List<Node> outrun, Node parent, Int32 index)
        {
            if (outrun.Count <= index) return; // bottom...
            var node = outrun[index];
            var parentweight = parent == null ? 0 : parent.Weight;

            // No point in checking nodes which already have a greater weight. Without this it would take around 700 trillion years on my laptop
            if (node.Weight >= parentweight + node.Value) return;
            
            node.Weight = parentweight + node.Value;
            if (node.Weight > CurrentMaxNode.Weight) CurrentMaxNode = node; // This is used later to easily reconstruct the path
 
            node.Parent = parent;

            TurboTurbo(outrun, node, index + node.Level);        // left child
            TurboTurbo(outrun, node, index + node.Level + 1);    // right child
        }


        /// <summary>
        /// Recursively prints the path from the start
        /// </summary>
        /// <param name="outrun"></param>
        /// <param name="node"></param>
        private static void PrintTurbo(List<Node> outrun, Node node)
        {
            if (node.Parent != null) PrintTurbo(outrun, node.Parent);
            Console.Write(node.Value + " ");
        }
    }
}
