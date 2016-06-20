using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextAnalysis
{
    public class TextProcessor
    {
        private static TextProcessor _processor;

        private HashSet<string> stopwords;

        private TextGraph graph;

        private TextProcessor()
        {
            stopwords = new HashSet<string>();
            graph = new TextGraph();
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
                    if (tmp.Count > 0) {
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

        private void buildTextGraph(string text)
        {
            var freq = new Dictionary<string, double>();
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
                        if (!stopwords.Contains(word))
                        {
                            //if (word.EndsWith("s") && null != graph.GetNode(word.TrimEnd('s')))
                            //    word = word.TrimEnd('s');
                            graph.AddNode(word, null);
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
        }
        //TODO: Address singular/plural discrepancy
        public Dictionary<string, double> GetKeyWords(string text, int count = 5)
        {
            var keywords = new Dictionary<string, double>();
            buildTextGraph(text);
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

        public Dictionary<string, double> GetSummary(string text, int sentenceCount = 5, int count = 5)
        {
            var summary = new Dictionary<string, double>();
            buildTextGraph(text);
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
                    if (text[i] == '.' || text[i]=='?')
                    {
                        summary[new string(ret.ToArray())]= weight / wordcount;
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
            return (c == '.' || c == ',' || c == ';' || c == ':' || c=='?');
        }

        private void loadStopwords()
        {
            var lst = File.ReadAllLines("stopwords.txt");
            foreach (var line in lst)
                stopwords.Add(line);
        }
    }
}
