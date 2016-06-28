using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalysis;
using Newtonsoft.Json.Linq;
using PatentDB;
using System.Net;
//0.custAge	1.profession	2.marital	3.schooling	4.default	5.housing	6.loan	7.contact	8.month	9.day_of_week	10.campaign	11.pdays	12.previous	13.poutcome	14.emp.var.rate	15.cons.price.idx	16.cons.conf.idx	17.euribor3m	18.nr.employed	19.pmonths	20.pastEmail	21.responded	22.profit	23.id


namespace TextConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //var txt = File.ReadAllText(@"C:\etc\markov.txt");
            //var obj = TextProcessor.Instance.GetSummary(txt);
            //foreach(var o in obj)
            //    Console.WriteLine(o);
            PatentDBController.Default.Initialize(@"C:\etc\patent\db");
            var patent = PatentDBController.Default.Load(5585083);
            //var str = CallRestMethod("https://s3.amazonaws.com/nasapatents/patents/patent_5585083");
            Console.ReadKey();
            //var lines = File.ReadAllLines(@"C:\etc\palindrome1.txt");
            //var srb = new StringBuilder();
            //for (int i = 1; i < lines.Length; i++)
            //{
            //    string str = lines[i];
            //    if (checkPalindrome(str))
            //        srb.AppendLine("Case #" + i.ToString() + ": is a palindrome");
            //    else
            //        srb.AppendLine("Case #" + i.ToString() + ": is not a palindrome");
            //}
            //Console.WriteLine("done");
            //File.WriteAllText(@"C:\etc\palindromeans.txt", srb.ToString());
            //var txt = File.ReadAllText(@"c:\etc\patentlarge.txt");
            //var lst = new List<Patent>();
            //var dic = new Dictionary<string, object>();
            //dic.Add("patents", lst);
            //var obj = JsonConvert.DeserializeObject(txt) as List<Patent>;
            //var patents = obj;// obj["patents"].ToObject(typeof(List<Patent>));
            //var srb = new StringBuilder();
            //foreach(var patent in patents as List<Patent>)
            //{
            //    var keywords = TextProcessor.Instance.GetKeyWords(patent.Abstract, 20);
            //    foreach (var word in keywords)
            //    {
            //        Console.Write(word.Key + ", ");
            //        srb.Append(word.Key + ", ");
            //    }
            //    Console.Write(Environment.NewLine);
            //    srb.Append(Environment.NewLine);
            //}
            ////obj.ToObject(typeof(List<Patent>));
            //File.WriteAllText(@"C:\etc\keywords.csv", srb.ToString());
            //Console.ReadKey();
            //var keywords = TextProcessor.Instance.GetKeyWords(txt, 10);
            //foreach (var word in keywords) Console.WriteLine(word);
            //var summary = TextProcessor.Instance.GetSummary(txt);
            //var ordered = summary.OrderByDescending(a => a.Value);
            //foreach (var pair in ordered)
            //{
            //    Console.WriteLine(pair);
            //}
            // var map = new Dictionary<int, Dictionary<string, int>>();
            // var prof = new Dictionary<string, int>()
            // {
            //     {"admin.", 0},
            //     {"services", 1},
            //     {"blue-collar", 2},
            //     {"entrepreneur", 3},
            //     {"technician", 4},
            //     {"retired", 5},
            //     {"housemaid", 6},
            //     {"student", 7},
            //     {"unknown", -1},
            //     {"unemployed", 8},
            //     {"self-employed", 9},
            //     {"management", 10}
            // };
            // map.Add(1, prof);
            // var marital = new Dictionary<string, int>()
            // {
            //     {"single", 0},
            //     {"married", 1},
            //     {"divorced", 2},
            //     {"unknown", -1}
            // };
            // map.Add(2, marital);
            // var schooling = new Dictionary<string, int>()
            // {
            //     {"illiterate",0},
            //     {"basic.4y",1},
            //     {"basic.9y",2},
            //     {"basic.6y",3},
            //     {"high.school",4},
            //     {"university.degree",5},
            //     {"professional.course",6},
            //     {"unknown",-1},
            //     {"NA",-1}
            // };
            // map.Add(3, schooling);

            // var defaultmap = new Dictionary<string, int>()
            // {
            //     {"no", 0},
            //     {"yes", 1},
            //     {"unknown", -1}
            // };
            // map.Add(4, defaultmap);
            // map.Add(5, defaultmap);
            // map.Add(6, defaultmap);
            //// map.Add(21, defaultmap);

            // var contact = new Dictionary<string, int>()
            // {
            //     {"cellular", 0},
            //     {"telephone", 1}
            // };

            // map.Add(7, contact);

            // var months = new Dictionary<string, int>()
            // {
            //     {"jan", 0},
            //     {"feb", 1},
            //     {"mar", 2},
            //     {"apr", 3},
            //     {"may", 4},
            //     {"jun", 5},
            //     {"jul", 6},
            //     {"aug", 7},
            //     {"sep", 8},
            //     {"oct", 9},
            //     {"nov", 10},
            //     {"dec", 11},
            //     {"NA", -1},
            //     {"unknown", -1}
            // };

            // map.Add(8, months);

            // var days = new Dictionary<string, int>()
            // {
            //     {"sun", 0},
            //     {"mon", 1},
            //     {"tue", 2},
            //     {"wed", 3},
            //     {"thu", 4},
            //     {"fri", 5},
            //     {"sat", 6},
            //     {"NA", -1},
            //     {"unknown", -1}
            // };

            // map.Add(9, days);

            // var outcome = new Dictionary<string, int>()
            // {
            //     {"failure", 0},
            //     {"success", 1},
            //     {"nonexistent", -1}
            // };
            // map.Add(13, outcome);

            // var classsrb = new StringBuilder();
            // var srb = new StringBuilder();
            // var regsrb = new StringBuilder();
            // var lines = File.ReadAllLines(@"");
            // classsrb.AppendLine(lines[0]);
            // regsrb.AppendLine(lines[0]);
            // for(int i = 1; i < lines.Length; i++)
            // {
            //     var words = lines[i].Split(',');
            //     for(int j = 0; j < words.Length; j++)
            //     {
            //         var word = words[j].Trim('"');
            //         if (map.ContainsKey(j))
            //         {
            //             srb.Append(map[j][word]);
            //         }
            //         else {
            //             if (j == 0 && word == "NA")
            //                 word = getAge(words[2].Trim('"'));
            //             if (j == 22 && word == "NA")
            //                 word = "0";
            //             srb.Append(word);
            //         }
            //         if (j < words.Length - 1)
            //             srb.Append(",");
            //         //else srb.Append(Environment.NewLine);
            //     }
            //     classsrb.AppendLine(srb.ToString());
            //   //  if (words[21].Trim('"') == "yes") regsrb.AppendLine(srb.ToString());
            //     srb = new StringBuilder();

            // }
            // File.WriteAllText(@"", classsrb.ToString());
            //// File.WriteAllText(@"", regsrb.ToString());
            Console.ReadKey();
        }

        private static bool checkPalindrome(string str)
        {
            str = str.ToLower();
            int start = 0;
            int end = str.Length - 1;
            while(start <= end)
            {
                while (!char.IsLetterOrDigit(str[start])) start++;
                while (!char.IsLetterOrDigit(str[end])) end--;
                if (start > end) break;
                if (str[start] != str[end]) return false;
                else
                {
                    start++;
                    end--;
                }
            }
            return true;
        }

        private static void oddeven()
        {
            var lines = File.ReadAllLines(@"C:\etc\odd1.txt");
            var srb = new StringBuilder();
            for (int i = 1; i < lines.Length; i++)
            {
                var n = int.Parse(lines[i][lines[i].Length - 1].ToString());
                if (n % 2 == 0)
                    srb.AppendLine("Case #" + i.ToString() + ": even");
                else
                    srb.AppendLine("Case #" + i.ToString() + ": odd");
            }
            Console.WriteLine("done");
            File.WriteAllText(@"C:\etc\odd1ans.txt", srb.ToString());
        }

        private static string getAge(string status)
        {
            var ret = "-1";
            switch (status)
            {
                case "single":
                    ret = "32";
                    break;
                case "married":
                    ret = "42";
                    break;
                case "divorced":
                    ret = "45";
                    break;
                case "unknown":
                    ret = "38";
                    break;
            }
            return ret;
        }

        public static string CallRestMethod(string url)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            //webrequest.ContentType = "application/x-www-form-urlencoded";
            //webrequest.Headers.Add("Username", "xyz");
            //webrequest.Headers.Add("Password", "abc");
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webresponse.Close();
            return result;
        }
    }
}
