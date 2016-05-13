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
            if (null == root) root = new Node(data);
            //else insertN(root, data);
            else root = insert(root,data);
        }

        private Node balance(Node node)
        {
            //while(Math.Abs(node.Balance) < 2)
            //{
            //    if (null == node.Parent) return;
            //    node = node.Parent;
            //}
            if (node.Balance > 1 && node.Right.Balance > 0) RR(node);
            else if (node.Balance > 1 && node.Right.Balance < 0) RL(node);
            else if (node.Balance < -1 && node.Left.Balance < 0) LL(node);
            else if (node.Balance < -1 && node.Left.Balance > 0) LR(node);
            return node;
        }

        //private void rr(Node node)
        //{
        //    var p = node.Parent;
        //    var c = node.Left;
        //    node.Left = c.Right;
        //    c.Right = node;
        //    node.Parent = c;
        //    c.Parent = p;
        //    if (null != p)
        //        p.Left = c;
        //    else root = c;
        //}

        private Node RR(Node node)
        {
            var pivot = node.Right;
            node.Right = pivot.Left;
            pivot.Left = node;
            return pivot;
        }

        private Node LL(Node node)
        {
            var pivot = node.Left;
            node.Left = pivot.Right;
            pivot.Right = node;
            return pivot;
        }

        private Node RL(Node node)
        {
            var nnode = node.Right;
            node.Right = LL(nnode);
            return RR(node);
        }

        private Node LR(Node node)
        {
            var nnode = node.Left;
            node.Left = RR(nnode);
            return LL(node);
        }
        

        private void insertN(Node node, int data)
        { 
            if(data < node.Data)
            {
                if (null == node.Left)
                {
                    var c = new Node(data);
                    node.Left = c;
                   // c.Parent = node;
                   // Console.WriteLine(this.ToString());
                    balance(c);

                }
                else insertN(node.Left, data);
            }
            else
            {
                if (null == node.Right)
                {
                    var c = new Node(data);
                    node.Right = c;
                    //c.Parent = node;
                    //Console.WriteLine(this.ToString());
                    balance(c);
                }
                else insertN(node.Right, data);
            }
               
        }

        private Node insert(Node node, int data)
        {
            if (null == node) node = new Node(data);
            else if (data < node.Data)
            {
                node.Left = insert(node.Left, data);
                node = balance(node);
            }
            else if (data >= node.Data)
            {
                node.Right = insert(node.Right, data);
               node = balance(node);
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

        private void inOrderPrint(Node node)
        {
            if (null == node) return;
            inOrderPrint(node.Left);
            Console.Write(node.Data + " ");
            inOrderPrint(node.Right);
        }

        public void Print()
        {
            print(root);
        }

        public void PrintInOrder()
        {
            inOrderPrint(root);
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
                //else lst[n.Depth - 1] += "*";
                if (null != n.Right)
                {
                    //for (int i = 0; i <= count; i++) lst[n.Right.Depth] += "\t";
                    lst[n.Right.Depth] += n.Right.Data + "*";
                    q.Push(n.Right);
                }
               // else lst[n.Depth - 1] += "*";
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
        public Node Left
        {
            get;
            set;
        }
        public Node Right
        {
            get;
            set;
        }
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
                var lh = null == Left ? -1 : Left.Height;
                var rh = null == Right ? -1 : Right.Height;
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

        public override string ToString()
        {
            return string.Format("(Value: {0}, (Left: {1}, Right: {2}))", new object[] { Data.ToString(), Left == null ? "null" : Left.ToString(), Right == null ? "null" : Right.ToString() });
        }
    }
}
