using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace TextAnalysis
{
    public class TextProcessor
    {
        private static TextProcessor _processor;

        private static HashSet<string> stopwords;

        public TextProcessor()
        {
            WindowSize = 3;
            loadStopwords();
        }

        public static TextProcessor Instance
        {
            get
            {
                if (null == _processor)
                    _processor = new TextProcessor();
                return _processor;
            }
        }

        public int WindowSize { get; set; }

        public List<string> Tokenize(string text, bool removeStopwords = true)
        {
            int i = 0;
            var tmp = new List<char>();
            var ret = new List<string>();

            while (i < text.Length)
            {
                if (char.IsLetterOrDigit(text[i])) tmp.Add(text[i]);
                else
                {
                    if (tmp.Count > 0)
                    {
                        var word = new String(tmp.ToArray());
                        if (removeStopwords)
                        {
                            if (!stopwords.Contains(word.ToLower()))
                                ret.Add(word);
                        }
                        else ret.Add(word);
                        tmp = new List<char>();
                    }
                }
                i++;
            }
            return ret;
        }

        private TextGraph buildTextGraph(string text)
        {
            var freq = new Dictionary<string, double>();
            var graph = new TextGraph();
            int wordcount = 0;
            int i = 0;
            var tmp = new List<char>();
            var q = new Queue<string>();

            while (i < text.Length)
            {
                if (char.IsLetterOrDigit(text[i])) tmp.Add(text[i]);
                else
                {
                    if (tmp.Count > 0)
                    {
                        var word = new String(tmp.ToArray()).ToLower();
                        double d = 0;
                        if (!stopwords.Contains(word) && word.Length > 2 && !double.TryParse(word, out d))
                        {
                            //if (word.EndsWith("s") && null != graph.GetNode(word.TrimEnd('s')))
                            //    word = word.TrimEnd('s');
                            var node = new Node();
                            node.ID = word;
                            node.Name = word;
                            node.Type = "keyword";
                            graph.AddNode(node);
                            if (!freq.ContainsKey(word)) freq[word] = 1;
                            else freq[word] += 1;
                            wordcount++;
                            foreach (var w in q)
                            {
                                graph.AddEdge(w, word);
                            }
                            q.Enqueue(word);
                            if (q.Count > WindowSize)
                                q.Dequeue();
                        }
                        tmp = new List<char>();
                    }
                    if (isPhraseSeparator(text[i]))
                        q = new Queue<string>();
                }
                i++;
            }
            foreach (var node in graph)
                freq[node.ID] = freq[node.ID] / wordcount;
            graph.UpdateWeights(freq);
            return graph;
        }

        public object GetTextGraph(string text, int count = -1)
        {
           var graph = buildTextGraph(text);
            if (count == -1)
                return graph.GetGraphJSON();
            else
            {
                var ordered = graph.OrderByDescending(a => a.Weight);
                int cnt = 0;
                var keys = new List<string>();
                foreach (var node in ordered)
                {
                    keys.Add(node.ID);
                    cnt++;
                    if (cnt == count) break;
                }
                var ret = graph.GetGraphJSON(keys, new string[] { "strict" });
                return ret;
            }
        }

        public Dictionary<string, double> GetKeyPhrases(string text, int size)
        {
            var summary = new Dictionary<string, double>();
            var graph = buildTextGraph(text);
            int i = 0;
            var tmp = new List<char>();
            var ret = new List<char>();
            var phrases = new PhraseCollection(size);
            int wordcount = 0;
            double weight = 0;

            while (i < text.Length)
            {
                ret.Add(text[i]);
                if (char.IsLetterOrDigit(text[i])) tmp.Add(text[i]);
                else
                {
                    if (tmp.Count > 0)
                    {
                        var word = new String(tmp.ToArray()).ToLower();
                        if (!stopwords.Contains(word))
                        {
                            var node = graph.GetNode(word);
                            if (null != node)
                            {
                                wordcount++;
                                weight += node.Weight;
                            }

                        }
                        tmp = new List<char>();
                    }
                    if (text[i] == '.' || text[i] == '?')
                    {
                        summary[new string(ret.ToArray())] = weight / wordcount;
                        ret = new List<char>();
                        weight = 0;
                        wordcount = 0;
                    }
                }
                i++;
            }
            return summary;
        }
        //TODO: Address singular/plural discrepancy
        public Dictionary<string, double> GetKeyWords(string text, int count = 5)
        {
            var keywords = new Dictionary<string, double>();
            var graph = buildTextGraph(text);
            var ordered = graph.OrderByDescending(a => a.Weight);
            int cnt = 0;
            foreach (var node in ordered)
            {
                keywords.Add(node.ID, node.Weight);
                cnt++;
                if (cnt == count) break;
            }
            return keywords;
        }

        public Dictionary<string, double> GetSummary(string text, int sentenceCount = 5)
        {
            var summary = new Dictionary<string, double>();
            var graph = buildTextGraph(text);
            int i = 0;
            var tmp = new List<char>();
            var ret = new List<char>();
            int wordcount = 0;
            double weight = 0;


            while (i < text.Length)
            {
                ret.Add(text[i]);
                if (char.IsLetterOrDigit(text[i])) tmp.Add(text[i]);
                else
                {
                    if (tmp.Count > 0)
                    {
                        var word = new String(tmp.ToArray()).ToLower();
                        if (!stopwords.Contains(word))
                        {
                            var node = graph.GetNode(word);
                            if (null != node)
                            {
                                wordcount++;
                                weight += node.Weight;
                            }

                        }
                        tmp = new List<char>();
                    }
                    if (text[i] == '.' || text[i] == '?')
                    {
                        summary[new string(ret.ToArray())] = weight / wordcount;
                        ret = new List<char>();
                        weight = 0;
                        wordcount = 0;
                    }
                }
                i++;
            }
            return summary;
        }

        private bool isPhraseSeparator(char c)
        {
            return (c == '.' || c == ',' || c == ';' || c == ':' || c == '?');
        }

        private void loadStopwords()
        {
            if (null == stopwords) stopwords = new HashSet<string>();
            var path = HttpContext.Current.Server.MapPath("~/App_Data/stopwords.txt");
            var lst = File.ReadAllLines(path);
            foreach (var line in lst)
                stopwords.Add(line);
        }
    }

    public class PhraseCollection
    {
        private bool dirty = false;

        private int _size;

        private Queue<Tuple<string,double>> queue;

        private Dictionary<string, double> hash;

        private IOrderedEnumerable<KeyValuePair<string, double>> ordered;

        public PhraseCollection(int size)
        {
            _size = size;
            queue = new Queue<Tuple<string, double>>();
            hash = new Dictionary<string, double>();
        }

        public void Add(string word, double weight)
        {
            queue.Enqueue(new Tuple<string, double>(word,weight));
            if (queue.Count > _size)
            {
                queue.Dequeue();
            }
            if(queue.Count == _size)
            {
                var srb = new StringBuilder();
                double wt = 0;
                foreach(var pair in queue)
                {
                    srb.Append(pair.Item1 + " ");
                    wt += pair.Item2;
                }

                var str = srb.ToString();
                if (!hash.ContainsKey(str))
                {
                    hash.Add(str, wt / _size);
                    dirty = true;
                }
            }
        }

        public List<string> GetTopNPhrases(int count)
        {
            if (dirty)
            {
                ordered = hash.OrderByDescending(a => a.Value);
                dirty = false;
            }
            var ret = new List<string>();
            for (int i = 0; i < count; i++) ret.Add(ordered.ElementAt(i).Key);
            return ret;
        }
    }
}