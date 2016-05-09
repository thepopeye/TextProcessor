using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackBench.Algorithms
{
    public class DynProg
    {
        private static DynProg _instance;
        public static DynProg Instance
        {
            get
            {
                if (null == _instance) _instance = new DynProg();
                return _instance;
            }
        }

        private DynProg()
        {

        }



        public int CountCoinChange(int[] vals, int Sum)
        {
            int m = vals.Length;
            var table = new int[Sum + 1, m];

            for (int i = 0; i < m; i++)
            {
                table[0, i] = 1;
                Console.Write("1 ");
            }
            Console.Write("\n");

            for (int i = 1; i < Sum + 1; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    // Count of solutions including S[j]
                    var x = (i - vals[j] >= 0) ? table[i - vals[j], j] : 0;

                    // Count of solutions excluding S[j]
                    var y = (j >= 1) ? table[i, j - 1] : 0;

                    // total count
                    table[i, j] = x + y;

                    Console.Write(table[i, j] + " ");
                }
                Console.Write("\n");
            }

            return table[Sum, m - 1];
        }

        private Dictionary<char, int> inputmap;

        private string[] alphanumbers = new string[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE" };
        private List<Tuple<int, char>> codes = new List<Tuple<int, char>>()
        {
            new Tuple<int, char>(0,'Z'),
            new Tuple<int, char>(2,'W'),
            new Tuple<int, char>(4,'U'),
            new Tuple<int, char>(6,'X'),
            new Tuple<int, char>(8,'G'),
            new Tuple<int, char>(5,'F'),
            new Tuple<int, char>(7,'V'),
            new Tuple<int, char>(3,'H'),
            new Tuple<int, char>(9,'I'),
            new Tuple<int, char>(1,'O'),
        };

        private void updateInputMap(int num)
        {
            string code = alphanumbers[num];
            foreach (var c in code)
            {
                if (inputmap.ContainsKey(c))
                {
                    inputmap[c] = inputmap[c] - 1;
                    if (inputmap[c] <= 0) inputmap.Remove(c);
                }
                else throw new Exception("Incorrect operation");
            }
        }

        private void createInputMap(string input)
        {
            inputmap = new Dictionary<char, int>();
            foreach (var c in input)
            {
                if (inputmap.ContainsKey(c))
                    inputmap[c] = inputmap[c] + 1;
                else inputmap.Add(c, 1);
            }
        }

        public string GetPhoneNumber(string input)
        {
            createInputMap(input);
            var phone = new List<int>();
            foreach (var pair in codes)
            {
                while (inputmap.ContainsKey(pair.Item2))
                {
                    phone.Add(pair.Item1);
                    updateInputMap(pair.Item1);
                }
            }
            phone.Sort();
            var srb = new StringBuilder();
            foreach (var num in phone) srb.Append(num);
            return srb.ToString();
        }

        private Dictionary<string, int> firstWords;

        private Dictionary<string, int> secondWords;

        private void updateMap(string input)
        {
            if (null == firstWords) firstWords = new Dictionary<string, int>();
            if (null == secondWords) secondWords = new Dictionary<string, int>();
            var splits = input.Split(' ');
            if (firstWords.ContainsKey(splits[0])) firstWords[splits[0]] = firstWords[splits[0]] + 1;
            else firstWords.Add(splits[0], 1);
            if (secondWords.ContainsKey(splits[1])) secondWords[splits[1]] = secondWords[splits[1]] + 1;
            else secondWords.Add(splits[1], 1);
        }

        private bool isUnique(string input)
        {
            var splits = input.Split(' ');
            return false;
        }

        public string MatchScores(string s1, string s2)
        {
            long num1 = 0;
            long num2 = 0;
            var l = s1.Length;
            for (int i = 0; i < l; i++)
            {
                long baseval = (long)Math.Pow(10, l - 1 - i);
                if (s1[i] == '?' && s2[i] == '?')
                {
                    if (num1 != num2)
                    {
                        double factor = l - 2 - i;
                        if (factor < 0) factor = 0;
                        long incr = (long)Math.Pow(10, factor);
                        if (num1 > num2)
                        {
                            num2 = num2 + 9 * incr;
                        }
                        else num1 = num1 + 9 * incr;
                    }
                }
                else if (s1[i] != '?' && s2[i] != '?')
                {
                    num1 += baseval * int.Parse(s1[i].ToString());
                    num2 += baseval * int.Parse(s2[i].ToString());
                }
                else if (s1[i] == '?' && s2[i] != '?')
                {
                    if (num1 == num2)
                    {
                        num1 += baseval * int.Parse(s2[i].ToString());
                        num2 += baseval * int.Parse(s2[i].ToString());
                    }
                    else
                    {
                        double factor = l - 2 - i;
                        if (factor < 0) factor = 0;
                        long incr = (long)Math.Pow(10, factor);
                        if (num1 > num2)
                        {
                            num2 = num2 + 9 * incr;
                        }
                        else num1 = num1 + 9 * incr;
                    }
                }
                else if (s1[i] != '?' && s2[i] == '?')
                {
                    if (num1 == num2)
                    {
                        num1 += baseval * int.Parse(s1[i].ToString());
                        num2 += baseval * int.Parse(s1[i].ToString());
                    }
                    else
                    {
                        double factor = l - 2 - i;
                        if (factor < 0) factor = 0;
                        long incr = (long)Math.Pow(10, factor);
                        if (num1 > num2)
                        {
                            num2 = num2 + 9 * incr;
                        }
                        else num1 = num1 + 9 * incr;
                    }
                }
            }

            var str1 = new StringBuilder();
            var str2 = new StringBuilder();

            for (int i = l - 1; i >= 0; i--)
            {
                var divisor = (long)Math.Pow(10, i);
                str1.Append(num1 / divisor);
                str2.Append(num2 / divisor);
                num1 = num1 % divisor;
                num2 = num2 % divisor;
            }

            return str1.ToString() + " " + str2.ToString();
        }

        //public void StockMax()
        //{
        //    var 
        //}

        //private long[,] J = new long[,];

        //long  InvestReturn(long  i, long  x, long * p, long  N)
        //{
        //    if (i == N)
        //    {
        //        J[N][x] = 0;
        //        return 0;
        //    }
        //    if (J[i][x] != -1)
        //    {
        //        return J[i][x];
        //    }
        //    long A = InvestReturn(i + 1, 1, p, N) - InvestReturn(i + 1, 0, p, N);
        //    long int B = InvestReturn(i + 1, 0, p, N);

        //    if (A - p[i] > 0)
        //    { //buying one stock is opt.
        //        J[i][x] = -p[i] + A * (x + 1) + B;
        //        return J[i][x];
        //    }
        //    else { //selling all x stocks is optimal
        //        J[i][x] = x * p[i] + B;
        //        return J[i][x];
        //    }
        //}

        public int RunBrickArrangements(int N)
        {
            if(null==primes)
                primes = new List<int>();
            var arrs = countBrickPermutations(N);
            var primecount = countPrimes((int)arrs);
            if(primecount == -1)
            {
                primecount = 0;
                for(int i=2; i <= arrs; i++)
                {
                    if(isPrime(i))
                    {
                        primes.Add(i);
                        primecount++;
                    }
                }
            }
            return primecount;
        }

        private List<int> primes;

        private bool isPrime(int a)
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
                while (index * index <= a)
                {
                    if (a % index == 0 | a % (index + 2) == 0)
                        retval = false;
                    index += 6;
                }
            }

            return retval;
        }

        private int countPrimes(int a, int start = -1, int end = -1)
        {
            
            if (primes.Count==0 || a > primes[primes.Count - 1]) return -1;
            if (start == -1 && end == -1)
            {
                start = 0;
                end = primes.Count - 1;
            }
            int mid = (start + end) / 2;
            if (mid <= 0) return -1;
            if (primes[mid] == a) return mid + 1;
            if (primes[mid+1] == a) return mid + 2;
            else if (primes[mid] < a && primes[mid + 1] > a) return mid + 1;
            else if (a < primes[mid]) return countPrimes(a, start, mid);
            else return countPrimes(a, mid + 1, end);
        }

        private List<long> factorials;

        private long factorial(int N)
        {
            if(null==factorials)
            {
                factorials = new List<long>();
                factorials.Add(1);
            }
            if (factorials.Count > N) return factorials[N];
            else
            {
                for(int i=factorials.Count;i<= N; i++)
                {
                    factorials.Add(i * factorials[i - 1]);
                }
                return factorials[N];
            }
        }

        private long multinomialCoefficient(int N, List<int> parts)
        {
            var numerator = new int[N];
            for (int i = 0; i < N; i++) numerator[i] = i + 1;
            int denom = 1;
            int denomprod = 1;
            foreach (var part in parts)
            {
                numerator = reduce(numerator, part, out denom);
                denomprod = denomprod * denom;
            }
            int prod = 1;
            for (int i = 0; i < numerator.Length; i++) prod = prod * numerator[i];
            return prod/denomprod;
        }

        private int[] reduce(int[] num, int d, out int denom)
        {
            denom = 1;
            var den = new int[d];
            for (int i = 0; i < d; i++) den[i] = i + 1;
            for (int i = 0; i < den.Length; i++)
            {
                if (den[i] == 1) continue;
                for(int j = 0; j < num.Length; j++)
                {
                    if (num[j] / den[i] >= 1 && num[j] % den[i] == 0)
                    {
                        num[j] = num[j] / den[i];
                        den[i] = 1;
                        break;
                    }
                }
            }
            for (int i = 0; i < den.Length; i++) denom = denom * den[i];
            return num;
        }

        private long countBrickPermutations(int N)
        {
            long count = 0;
            int multiplier = 0;
            while(N - 4*multiplier >= 0)
            {
                int set4 = multiplier;
                int set1 = N - 4*set4;
                long n = multinomialCoefficient(set1 + set4, new List<int> { set1, set4 });// factorial(set4 + set1) / (factorial(set4) * factorial(set1));
                count += n;
                multiplier++;
            }
            return count;
        }

        private int countBrickArrangements(int N)
        {
            var bricks = new int[] { 1, 4 };
            int m = bricks.Length;
            var table = new int[N + 1, m];
            int prevperms = 0;
            //init table
            for (int i = 0; i < m; i++) table[0, i] = 1;

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < m; j++)
                {
                    table[i, 0] = 0;
                    prevperms = table[i, j - 1];
                    prevperms += bricks[j-1] <= i ? table[i - bricks[j-1], j] : 0;
                    table[i, j] = prevperms;
                    ////including jth brick
                    //int x = 0;
                    //if (i - bricks[j] >= 0)
                    //{
                    //    //?
                    //    x = table[i - bricks[j], j];
                    //}
                    ////excluding jth brick
                    //int y = 0;
                    //if (j >= 1)
                    //{
                    //    //?
                    //    y = table[i, j - 1];
                    //}

                    //table[i, j] = x + y;
                }
            }
            return table[N, m - 1];
        }

        //******************************************************************************************************************//
        //***********************************Longest Increasing Subsequence O(NLogN)******************************************//
        //******************************************************************************************************************//

        private int ceilIndex(int[] A, int l, int r, int key)
        {
            while (r - l > 1)
            {
                int m = l + (r - l) / 2;
                if (A[m] >= key)
                    r = m;
                else
                    l = m;
            }

            return r;
        }

        public int LongestIncreasingSubsequenceLength(int[] A, int size)
        {
            // Add boundary case, when array size is one

            int[] tailTable = new int[size];
            int len; // always points empty slot

            tailTable[0] = A[0];
            len = 1;
            for (int i = 1; i < size; i++)
            {
                if (A[i] < tailTable[0])
                    // new smallest value
                    tailTable[0] = A[i];

                else if (A[i] > tailTable[len - 1])
                    // A[i] wants to extend largest subsequence
                    tailTable[len++] = A[i];

                else
                {
                    // A[i] wants to be current end candidate of an existing
                    // subsequence. It will replace ceil value in tailTable
                    int index = ceilIndex(tailTable, -1, len - 1, A[i]);
                    tailTable[index] = A[i];
                }
            }

            return len;
        }

        //******************************************************************************************************************//
        //***********************************Longest Palindroming Subsequence - recursive******************************************//
        //******************************************************************************************************************//
        
        public int LPS(string str, int start, int end)
        {
            if (start > end) return 0;
            else if (start == end) return 1;
            else if (str[start] == str[end]) return 2 + LPS(str, start + 1, end - 1);
            else
            {
                return Math.Max(LPS(str, start, end - 1), LPS(str, start + 1, end));
            }
        }

        //******************************************************************************************************************//
        //***********************************Longest Palindroming Substring - recursive******************************************//
        //******************************************************************************************************************//

        public int lps = 0;

        public int LPSb(string str, int start, int end)
        {
            if (start > end) return 0;
            else if (start == end && lps>0)
            {
                lps++;
                return 1;
            }
            else if (str[start] == str[end])
            {
                lps += 2;
                return LPSb(str, start + 1, end - 1);
            }
            else
            {
               lps = Math.Max(LPSb(str, start, end - 1), LPSb(str, start + 1, end));
               return 0;
            }
        }

        //******************************************************************************************************************//
        //***********************************Longest Palindroming Substring - DP******************************************//
        //******************************************************************************************************************//
        //    0 1 2 3
        //  3 4 0 0 0   
        //  2 0 2 0 0  
        //  1 0 0 2 0   
        //  0 0 0 0 4

        public int LPSDP(string str, int start, int end)
        {
            int mid = (str.Length - 1)/2;
            for(int i = 0; i <=mid; i++)
            {
                int j = str.Length - 1 - i;

            }

            if (start > end) return 0;
            else if (start == end && lps > 0)
            {
                lps++;
                return 1;
            }
            else if (str[start] == str[end])
            {
                lps += 2;
                return LPSDP(str, start + 1, end - 1);
            }
            else
            {
                lps = Math.Max(LPSDP(str, start, end - 1), LPSDP(str, start + 1, end));
                return 0;
            }
        }

        public string HTMLToLuna(string html)
        {
            var stack = new Stack<string>();
            int count = 0;
            string curr = string.Empty;
            var tmp = new StringBuilder();
            bool nested = true;
            while (count < html.Length)
            {
                if (html[count] == '<')
                {
                    count++;
                    if (nested) nested = false;
                    continue;
                }
                else if (html[count] == '>')
                {
                    var tag = tmp.ToString();
                    if (tag == "img /") { 
                        curr = "IMG({})";
                        tmp = new StringBuilder();
                }
                else if (!isClosingTag(tag))
                {
                    stack.Push(tmp.ToString());
                    tmp = new StringBuilder();
                }
                else
                {
                    var top = stack.Pop();
                    if (isComplement(tag, top))
                    {
                        if (!nested)
                        {
                            curr = top.ToUpper() + "([" + curr + "])";
                            nested = true;
                        }
                        else
                        {
                            curr = curr + ", " + top.ToUpper() + "([])";
                        }
                    }
                    tmp = new StringBuilder();
                }
                }
                else tmp.Append(html[count]);
                count++;
            }

            return curr;
        }

        private bool isClosingTag(string str)
        {
            return str.StartsWith("/");
        }

        private bool isComplement(string str1, string str2)
        {
            return "/" + str2 == str1;
        }

        public bool DeliveryFee(int[] intervals, int[] fees, int[][] deliveries)
        {
            var delcounts = new int[intervals.Length];
            foreach (var del in deliveries)
            {
                var deltime = del[0] * 60 + del[1];
                var interval = getInterval(intervals, deltime);
                delcounts[interval] += 1;
            }
            Console.WriteLine("step1");
            double ratio = 0;
            bool ret = true;
            for (int i = 0; i < fees.Length; i++)
            {
                double tmp = fees[i] / delcounts[i];
                if (ratio == 0)
                {
                    ratio = tmp;
                    continue;
                }
                else if (ratio != tmp)
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }

        int getInterval(int[] intervals, int time)
        {
            for (int i = 0; i < intervals.Length - 1; i++)
            {
                if (time >= intervals[i] * 60 && time < intervals[i + 1] * 60)
                    return i;
            }
            return intervals.Length - 1;
        }

    }
}
