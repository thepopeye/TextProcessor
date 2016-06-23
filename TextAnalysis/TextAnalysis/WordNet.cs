using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis
{
    public class WordNet
    {
        private Dictionary<string, wordnetNode> words;

        private Dictionary<string, string> relationpairs = new Dictionary<string, string>()
        {
            {"istypeof", "isgenericfor"},
            {"isgenericfor", "istypeof"},
            {"issimilarto", "issimilarto"},
            {"ispartof", "contains"},
            {"contains", "ispartof"}
        };

        private static WordNet _instance;

        public WordNet()
        {
            words = new Dictionary<string, wordnetNode>();
        }

        public static WordNet Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new WordNet();
                return _instance;
            }
        }

        public void Add(Word word)
        {
            var parts = word.ID.Split('.');
            if (parts.Length == 0) return;


        }

        private void add(string word, string connectedword, string relation)
        {
            if (words.ContainsKey(word))
            {
                var member = words[word];
                member.Connections.Add(new wordnetEdge(connectedword, relation));
                add(connectedword, word, relationpairs[relation]);
            }
        }

        private List<string> getWords(string expression)
        {
            var words = new List<string>();
            var phrases = expression.Split(',');
            foreach (var phrase in phrases)
            {
                var parts = phrase.Split('.');
                if (parts.Length == 0) continue;
                words.Add(parts[0]);
            }
            return words;
        }

        private class wordnetNode
        {
            public string Name { get; set; }

            public List<wordnetEdge> Connections { get; set; }

            public wordnetNode()
            {
                Connections = new List<wordnetEdge>();
            }

            public bool ContainsConnection(wordnetEdge edge)
            {
                foreach (var conn in Connections)
                    if (conn.To == edge.To && conn.Relation == edge.Relation)
                        return true;
                return false;
            }
        }

        private class wordnetEdge
        {
            public string To { get; set; }

            public string Relation { get; set; }
            
            public wordnetEdge()
            {
                    
            }

            public wordnetEdge(string to, string relation)
            {
                To = to;
                Relation = relation;
            }
        }
    }

    
}
