using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.DataStructures;
using HackBench.Algorithms;

namespace HackBench
{
    class Program
    {
        static void Main(string[] args)
        {

            //var srb = new StringBuilder();
            //var lines = File.ReadAllLines(@"C:\Users\parij\Downloads\cj1b\B-small-practice.in");
            //var num = int.Parse(lines[0]);
            //for (int i = 1; i <= num; i++)
            //{
            //    var splits = lines[i].Split(' ');
            //    var phone = DynProg.Instance.MatchScores(splits[0], splits[1]);
            //    srb.AppendLine("Case #" + i.ToString() + ": " + phone);
            //}
            //File.WriteAllText(@"C:\Users\parij\Downloads\cj1b\B-small-practice-out.txt", srb.ToString());
            //Console.WriteLine("done");
            // DynProg.Instance.MatchScores("?2?", "??3");
            // int[] A = { 2, 5, 3, 7, 1, 11, 3, 8, 4, 10, 5, 13, 6 };
            //int n = A.Length;
            //int len = DynProg.Instance.LongestIncreasingSubsequenceLength(A, n);
            ///int len = DynProg.Instance.LPSb("abefgba", 0, 6);
            ///
            // var intervals = new int[] { 0, 10, 22 };
            //var fees = new int[] { 1, 3, 1 };
            //var deliveries = new int[5][]
            //{
            //    new int[] { 8, 15 }, new int[] { 12, 21}, new int[] { 15, 48},new int[] { 20, 17}, new int[] { 23, 43 }
            //};

            //var str = DynProg.Instance.DeliveryFee(intervals,fees,deliveries);
            var tree = new AVLTree();
            var rand = new Random(1);
            for(int i = 0; i < 15; i++)
            {
                var r = rand.Next(0, 100);
                Console.WriteLine(r);
                tree.Insert(r);
                tree.PrintInOrder();
                Console.WriteLine();
            }
            tree.Print();
            //Console.WriteLine(tree.ToString());
            //int n = int.Parse(Console.ReadLine());
            //while (n > 0)
            //{
            //    int count = int.Parse(Console.ReadLine());
            //    int arrs = DynProg.Instance.RunBrickArrangements(count);
            //    Console.WriteLine(arrs);
            //    n--;
            //}
            
            Console.ReadKey();
        }
    }
}
