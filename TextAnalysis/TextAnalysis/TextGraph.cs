using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis
{
    public class Node
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("label")]
        public string Name { get; set; }

        [JsonProperty("group")]
        public string Type { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        public Node()
        {
            Parents = new List<string>();
            Children = new List<string>();
        }

        public Node(string Id, object data)
        {
            this.ID = Id;
            this.Data = Data;
            Parents = new List<string>();
            Children = new List<string>();
        }

        [JsonProperty]
        public Object Data { get; set; }

        public List<string> Parents { get; set; }

        public List<string> Children { get; set; }

    }

    public class Edge
    {
        public int Weight { get; set; }

        [JsonProperty("from")]
        public string Source { get; set; }

        [JsonProperty("to")]
        public string Target { get; set; }

        public Edge()
        {
            Weight = 1;
        }

        public Edge(string source, string target)
        {
            Source = source;
            Target = target;
            Weight = 1;
        }
    }

    public class NodeCollection : IEnumerable<Node>
    {
        private Dictionary<string, Node> nodehash;

        public NodeCollection() { nodehash = new Dictionary<string, Node>(); }

        public IEnumerator<Node> GetEnumerator()
        {
            return nodehash.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Add(Node Node)
        {
            bool ret = false;
            if (!nodehash.ContainsKey(Node.ID))
            {
                nodehash.Add(Node.ID, Node);
                ret = true;
            }

            return ret;
        }

        public bool Remove(string id)
        {
            return nodehash.Remove(id);
        }

        public bool Remove(Node Node)
        {
            return nodehash.Remove(Node.ID);
        }

        public bool Contains(Node Node)
        {
            return nodehash.ContainsKey(Node.ID);
        }

        public bool Contains(string id)
        {
            return nodehash.ContainsKey(id);
        }

        public Node GetNode(string id)
        {
            return this[id];
        }

        public Node this[string id]
        {
            get
            {
                Node ret = null;
                nodehash.TryGetValue(id, out ret);
                return ret;
            }
        }

        public int Count { get { return nodehash.Count; } }
    }
    
    public class TextGraph : IEnumerable<Node>
    {
        private NodeCollection _nodes;

        public TextGraph() { _nodes = new NodeCollection(); }

        public TextGraph(NodeCollection nodes)
        {
            this._nodes = nodes;
        }

        public void AddNode(string Id, Object value)
        {
            _nodes.Add(new Node(Id, value));
        }

        public void AddNode(Node node)
        {
            if (_nodes.Add(node))
            {
                foreach (var parent in node.Parents)
                {
                    AddEdge(parent, node.ID);
                }

                foreach (var child in node.Children)
                {
                    AddEdge(node.ID, child);
                }
            }
        }

        public void AddEdge(string ID_From, string ID_To)
        {
            var from = _nodes[ID_From];
            var to = _nodes[ID_To];
            if (null != from && null != to)
            {
                if (!from.Children.Contains(ID_From))
                    from.Children.Add(ID_To);
                if (!to.Parents.Contains(ID_From))
                    to.Parents.Add(ID_From);
            }
        }

        public bool Contains(string id)
        {
            return _nodes.Contains(id);
        }

        public bool Remove(string id)
        {
            var ret = false;
            if (_nodes.Contains(id))
            {
                foreach (var key in _nodes[id].Parents)
                {
                    var parent = _nodes[key];
                    if (null != parent)
                    {
                        parent.Children.Remove(id);
                    }
                }

                foreach (var key in _nodes[id].Children)
                {
                    var child = _nodes[key];
                    if (null != child)
                    {
                        child.Parents.Remove(id);
                    }
                }
                ret = true;

                _nodes.Remove(id);
            }
            return ret;
        }

        public int Count
        {
            get { return _nodes.Count; }
        }



        private HashSet<string> visited = null;
        private HashSet<string> _keys = null;

        public Dictionary<string, double> GetRankedNodeList(List<string> nodes)
        {
            var ret = new Dictionary<string, double>();
            foreach (var key in nodes)
            {
                var node = _nodes.GetNode(key);
                if (null == node) continue;

            }

            return ret;
        }

        //return csv formatted graphg summary
        public string GetGraphSummary()
        {
            var srb = new StringBuilder();
            foreach (var node in _nodes)
            {
                srb.AppendLine(node.ID + "," + node.Name + "," + node.Weight.ToString() + "," + node.Type);
            }
            return srb.ToString();
        }

        public Node GetNode(string id)
        {
            return _nodes.GetNode(id);
        }

        public double GetWeight(string id)
        {
            var node = _nodes.GetNode(id);
            if (null != node) return node.Weight;
            else return -1;
        }

        public void UpdateWeights(Dictionary<string, double> weights = null)
        {
            foreach (var node in _nodes)
            {
                if (null != weights && weights.ContainsKey(node.ID))
                    node.Weight = weights[node.ID];
                else
                    node.Weight = 1.0 / _nodes.Count;
            }
            double delta = 1.0;
            double damping = 0.50;
            double epsilon = 0.001;
            int count = 0;
            while (delta > epsilon && count < 1000)
            {
                foreach (var node in _nodes)
                {
                    double term2 = 0.0;
                    foreach (var key in node.Parents)
                    {
                        var parent = _nodes[key];
                        if (parent == null) continue;
                        term2 += parent.Weight / parent.Children.Count;
                    }
                    term2 = term2 * damping;
                    var newWeight = (1 - damping) / (_nodes.Count) + term2;
                    delta += node.Weight - newWeight;
                    node.Weight = newWeight;
                }
                count++;
            }
        }

        public object GetGraphJSON(IEnumerable<string> keys, IEnumerable<string> flags = null)
        {
            visited = new HashSet<string>();
            var filter = new ExtractionFilter(keys, flags);
            _keys = new HashSet<string>(keys);
            edgehash = new HashSet<string>();
            var nodes = new List<Node>();
            var edges = new List<Edge>();
            foreach (var key in keys)
            {
                traverse(key, nodes, edges, filter);
            }

            var ret = new Dictionary<string, object>();
            ret.Add("nodes", nodes);
            ret.Add("edges", edges);

            return ret;
        }

        private HashSet<string> edgehash;

        private void traverse(string key, List<Node> nodes, List<Edge> edges, ExtractionFilter filter)
        {
            var node = _nodes.GetNode(key);
            if (null == node) return;
            if (visited.Contains(key) || !filter.ShouldTraverse(node))
                return;
            else
            {
                visited.Add(key);
                nodes.Add(node);
            }
            if (!filter.IgnoreAncestors)
            {
                foreach (var parent in node.Parents)
                {
                    if (!edgehash.Contains(parent + "_" + key))
                    {
                        edges.Add(new Edge(parent, key));
                        edgehash.Add(parent + "_" + key);
                    }
                    if (!visited.Contains(parent))
                    {
                        traverse(parent, nodes, edges, filter);
                    }
                }
            }
            if (!filter.IgnoreDescendants)
            {
                foreach (var child in node.Children)
                {
                    if (!edgehash.Contains(key + "_" + child))
                    {
                        edges.Add(new Edge(key, child));
                        edgehash.Add(key + "_" + child);
                    }

                    if (!visited.Contains(child))
                    {
                        traverse(child, nodes, edges, filter);
                    }
                }
            }
        }



        public IEnumerator<Node> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        private class ExtractionFilter
        {
            private HashSet<string> _keys;

            private bool _strict = false;

            private bool _ignoreAncestors = false;

            private bool _ignoreDescendants = false;

            public ExtractionFilter(IEnumerable<string> keys, IEnumerable<string> flags)
            {
                _keys = new HashSet<string>(keys);
                setFlags(flags);
            }

            private void setFlags(IEnumerable<string> flags)
            {
                if (null == flags) return;
                foreach (var flag in flags)
                {
                    switch (flag.ToLower())
                    {
                        case "ignoreancestors":
                            _ignoreAncestors = true;
                            break;
                        case "ignoredescendants":
                            _ignoreDescendants = true;
                            break;
                        case "strict":
                            _strict = true;
                            break;
                        default:
                            break;
                    }
                }
            }

            public bool ShouldTraverse(Node node)
            {
                if (!_strict) return true;
                if (node.Type != "covariate") return true;
                if (_keys.Contains(node.ID))
                    return true;
                else
                    return false;
            }

            public bool IgnoreAncestors
            {
                get
                {
                    return _ignoreAncestors;
                }
            }

            public bool IgnoreDescendants
            {
                get
                {
                    return _ignoreDescendants;
                }
            }
        }
    }
}
