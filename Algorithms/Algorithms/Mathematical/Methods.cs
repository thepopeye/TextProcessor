using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackBench.Mathematical
{
    public static class Methods
    {
        public static bool IsAnagram(string a, string b)
        {
            var a_chars = a.ToArray();
            Array.Sort(a_chars);
            var b_chars = b.ToArray();
            Array.Sort(b_chars);
            if (a_chars.Length != b_chars.Length) return false;
            bool ret = true;
            for (int i = 0; i < a_chars.Length; i++)
            {
                if (a_chars[i] != b_chars[i])
                {
                    ret = false;
                    break;
                }
            }

            return ret;
        }
        
        public static bool IsPalindrome(string a)
        {
            var chars = a.ToArray();
            int len = chars.Length - 1;
            int mid = (chars.Length-1) / 2;
            bool ret = true;
            for (int i = 0; i <= mid; i++)
            {
                if (chars[i] != chars[len - i])
                {
                    ret = false;
                    break;
                }
            }

            return ret;
        }

        public static int SumSquares(int a)
        {
            int sum = 0;
            var arr = a.ToString().ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                var val = int.Parse(arr[i].ToString());
                sum += (int)Math.Pow(val, 2);
            }

            return sum;   
        }

        public static bool IsPrime(int a)
        {
            bool retval = true;
            if (a <= 1)
                retval = false;
            else if (a <= 3)
                retval = true;
            else if (a % 2 == 0 || a % 3 == 0)
                retval = false;
            else
            {
                int index = 5;
                while(index*index <= a)
                {
                    if (a % index == 0 | a % (index + 2) == 0)
                        retval = false;
                    index += 6;
                }
            }

            return retval;
        }

        public static int GetFib(int index)
        {
            if (index == 1 || index == 2) return 1;
            int a = 1;
            int b = 1;
            int fib = 0;
            for(int i= 3; i<= index; i++)
            {
                fib = a + b;
                a = b;
                b = fib;
            }
            return fib;

        }


        public static List<int> SexyPrimes(int max)
        {
            var ret = new List<int>();
            for(int i = 2; i <= max - 6; i++)
            {
                if (IsPrime(i) && IsPrime(i + 6))
                    ret.Add(i);

            }

            return ret;
        }

        public static List<List<int>> PyTriplets(int max)
        {
            var lst = new List<List<int>>();
            for(int i=1;i<=max;i++)
            {
                for(int j = 1; j <= max; j++)
                {
                    int sumsquare = i * i + j * j;
                    double k = Math.Sqrt(sumsquare);
                    if(k % 1 == 0 && k <= max && i < j && (i + j) > k )
                    {
                        lst.Add(new List<int>() { i, j, (int)k });
                    }
                }
            }
            lst.Sort((a, b) => a.ElementAt(2).CompareTo(b.ElementAt(2)));
            return lst;
        }

        static List<List<int>> board = new List<List<int>>()
        {
            new List<int>() {0,0,0,0,0,0,0 },
            new List<int>() {0,0,1,1,1,0,0 },
            new List<int>() {0,1,0,0,1,0,0 },
            new List<int>() {0,0,0,0,0,0,0 },
            new List<int>() {0,0,0,0,0,0,0 },
            new List<int>() {0,0,0,0,0,0,0 },
            new List<int>() {0,0,0,0,0,0,0 },
        };

        static List<Tuple<int,int>> neighbors = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(-1,-1),
                new Tuple<int, int>(-1,0),
                new Tuple<int, int>(-1,1),
                new Tuple<int, int>(0,-1),
                new Tuple<int, int>(0,1),
                new Tuple<int, int>(1,-1),
                new Tuple<int, int>(1,0),
                new Tuple<int, int>(1,1),

            };

        static void playGameoflife()
        {

        }

        static List<int> GetNeighbors(int row, int col)
        {
            var ret = new List<int>();
            for(int i=0;i<neighbors.Count;i++)
            {
                var tup = neighbors[i];
                int rowindex = row + tup.Item1;
                int colindex = col + tup.Item2;
                if (rowindex >= 0 && rowindex <= 6 && colindex >= 0 && colindex >= 0 && board[rowindex][colindex]==1)
                    ret.Add(1);
            }

            return ret;
        }


       
        
    }
}