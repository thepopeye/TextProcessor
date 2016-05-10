using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DataStructures
{
    public class AVLTree
    {
        private Node root { get; set; }

        public void Insert(int data)
        {
            root = insert(root,data);
        }

        private Node insert(Node node, int data)
        {
            if (null == node) node = new Node(data);
            else if (data < node.Data)
            {
                var child = insert(node.Left, data);
                node.Left = child; child.Parent = node;
            }
            else if (data >= node.Data)
            {
                var child = insert(node.Right, data);
                node.Right = child; child.Parent = node;
            }
            return node;
        }

        private void print(Node node=null, String prefix = null)
        {
            if (node == null)
            {
                Console.WriteLine(prefix + "+- <null>");
                return;
            }

            Console.WriteLine(prefix + "+- " + node.Data);
            print(node.Left, prefix + "|  ");
            print(node.Right, prefix + "|  ");
        }

        public void Print()
        {
            print(root);
        }

        public override string ToString()
        {
            if (null == root) return string.Empty;
            var lst = new string[root.Height+1];
            var q = new Stack<Node>();
            q.Push(root);
            //for (int i = 0; i <= root.Height; i++) lst[0] += "\t";
            lst[0] += "/" + root.Data;
            while (q.Count > 0)
            {
                var n = q.Pop();
                var count = n.Height - 1;
                if (null != n.Left)
                {
                    //for (int i = 0; i <= count; i++) lst[n.Left.Depth] += "\t";
                    if(string.IsNullOrEmpty(lst[n.Left.Depth])) lst[n.Left.Depth] = "/";
                    lst[n.Left.Depth] += n.Left.Data + "*";
                    q.Push(n.Left);
                }
                else lst[n.Depth - 1] += "*";
                if (null != n.Right)
                {
                    //for (int i = 0; i <= count; i++) lst[n.Right.Depth] += "\t";
                    lst[n.Right.Depth] += n.Right.Data + "*";
                    q.Push(n.Right);
                }
                else lst[n.Depth - 1] += "*";
            }
            var srb = new StringBuilder();
            int max = (int)Math.Pow(2, root.Height + 1);
            for (int i=0;i<lst.Length;i++)
            {
                var line = lst[i];
                int spaces = (int)Math.Pow(2, root.Height - i);
                var rep = new char[spaces];
                for (int j = 0; j < spaces; j++) rep[j] = (char)32;
                line = line.Replace("/", new string(rep));
                var interval = max / (int)Math.Pow(2, i);
                rep = new char[interval];
                for (int j = 0; j < interval; j++) rep[j] = (char)32;
                line = line.Replace("*", new string(rep));
                srb.AppendLine(line);
            }
            return srb.ToString();
        }
    }

    public class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }
        public int Data { get; set; }
        public int Height
        {
            get
            {
                var lh = null == Left ? -1 : Left.Height;
                var rh = null == Right ? -1 : Right.Height;
                return Math.Max(lh,rh) + 1;
            }
        }
        public int Depth
        {
            get
            {
                if (null == Parent) return 0;
                else return Parent.Depth + 1;
            }
        }
        public int Balance
        {
            get
            {
                var lh = null == Left ? 0 : Left.Height;
                var rh = null == Right ? 0 : Right.Height;
                return rh - lh;
            }
        }

        public Node()
        {

        }

        public Node(int data)
        {
            Data = data;
        }
    }
}
