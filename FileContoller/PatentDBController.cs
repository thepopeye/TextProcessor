using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TextAnalysis;
using System.Web;
using System.Net;

namespace PatentDB
{
    public class PatentDBController
    {
        private Dictionary<string, PatentHeader> headers;

        private Dictionary<string, List<int>> revlookup;

        private string storagelocation;

        private string headerpath
        {
            get
            {
                if (string.IsNullOrEmpty(storagelocation)) return HttpContext.Current.Server.MapPath("~/App_Data/headers.json");
                else return Path.Combine(storagelocation, "headers.json");
            }
        }

        private string revlookuppath
        {
            get
            {
                if (string.IsNullOrEmpty(storagelocation)) return HttpContext.Current.Server.MapPath("~/App_Data/revlookup.json");
                else return Path.Combine(storagelocation, "revlookup.json");
            }
        }

        private static PatentDBController _instance;

        public static PatentDBController Default
        {
            get
            {
                if (null == _instance)
                    _instance = new PatentDBController();
                return _instance;
            }
        }

        private PatentDBController()
        {
            headers = new Dictionary<string, PatentHeader>();
            revlookup = new Dictionary<string, List<int>>();
        }

        public void Initialize(string location)
        {
            storagelocation = location;   

            //load headers and lookup;
            if (File.Exists(headerpath) && File.Exists(revlookuppath))
            {
                var txt = File.ReadAllText(headerpath);
                var obj = JsonConvert.DeserializeObject(txt) as JObject;
                if (null != obj)
                {
                    headers = obj.ToObject(typeof(Dictionary<string, PatentHeader>)) as Dictionary<string, PatentHeader>;
                }
                //load revlookup
                txt = File.ReadAllText(revlookuppath);
                var rev = JsonConvert.DeserializeObject(txt) as JObject;
                if(null!= rev)
                {
                    revlookup = rev.ToObject(typeof(Dictionary<string, List<int>>)) as Dictionary<string, List<int>>;
                }
            }
        }

        public void Register(Patent patent, bool commit = false)
        {
            var header = new PatentHeader(patent);
            headers[patent.ID.ToString()] = header;
            foreach (var word in header.KeyWords)
            {
                if (revlookup.ContainsKey(word.Key))
                    revlookup[word.Key].Add(patent.ID);
                else
                    revlookup.Add(word.Key, new List<int>() { patent.ID });
            }
            var str = JsonConvert.SerializeObject(patent);
            var path = Path.Combine(storagelocation, "patent_" + patent.ID.ToString() + ".json");
            File.WriteAllText(path, str);
            if (commit) Commit();
        }

        public void Commit()
        {
            if (string.IsNullOrEmpty(storagelocation)) return;
            var txt = JsonConvert.SerializeObject(headers);
            File.WriteAllText(headerpath, txt);
            txt = JsonConvert.SerializeObject(revlookup);
            File.WriteAllText(revlookuppath, txt);
        }

        //load patent
        public Patent Load(int ID)
        {
            var txt = getURLContent(ID);
            if (string.IsNullOrEmpty(txt)) return null;
            var obj = JsonConvert.DeserializeObject<Patent>(txt);
            return obj;
        }

        private string getURLContent(int patentid)
        {
            string url = "https://s3.amazonaws.com/nasapatents/patentsv2/patent_" + patentid.ToString() + ".json";
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webresponse.Close();
            return result;
        }

        ////build graph from keywords
        //items in graph to include authors, year of expiry, center, title (document), words

        public object GetDocumentsGraph(List<string> keywords)
        {
            var nodehash = new HashSet<string>();
            var edgehash = new HashSet<string>();

            var nodes = new List<Node>();
            var edges = new List<Edge>();
            var headercoll = new List<PatentHeader>();
            foreach (var key in keywords)
            {
                if (!revlookup.ContainsKey(key)) continue;
                foreach (var id in revlookup[key])
                {
                    var header = headers[id.ToString()];
                    bool process = true;
                    foreach (var word in keywords)
                    {
                        if (!header.KeyWords.ContainsKey(word))
                            process = false;
                    }
                    if (!process) continue;
                    var node = new Node();
                    node.ID = header.ID.ToString();
                    node.Name = header.Title;
                    node.Type = "doc";
                    if (!nodehash.Contains(node.ID.ToString()))
                    {
                        nodes.Add(node);
                        headercoll.Add(header);
                        nodehash.Add(node.ID.ToString());
                    }
                    foreach (var inventor in header.Inventors)
                    {
                        node = new Node();
                        node.ID = inventor;
                        node.Name = inventor;
                        node.Type = "inventor";
                        if (!nodehash.Contains(node.ID.ToString()))
                        {
                            nodes.Add(node);
                            nodehash.Add(node.ID.ToString());
                        }
                        var edgekey = inventor + "_" + header.ID.ToString();
                        if (!edgehash.Contains(edgekey))
                        {
                            var edge = new Edge(inventor, header.ID.ToString());
                            edges.Add(edge);
                            edgehash.Add(edgekey);
                        }
                    }

                    for (int k= 0; k < 5;k++)
                    {
                        var word = header.KeyWords.ElementAt(k);
                        node = new Node();
                        node.ID = word.Key;
                        node.Name = word.Key;
                        node.Type = "keyword";
                        if (!nodehash.Contains(node.ID.ToString()))
                        {
                            nodes.Add(node);
                            nodehash.Add(node.ID.ToString());
                        }
                        var edgekey = header.ID.ToString() + "_" + word.Key;
                        if (!edgehash.Contains(edgekey))
                        {
                            var edge = new Edge(header.ID.ToString(), word.Key);
                            edges.Add(edge);
                            edgehash.Add(edgekey);
                        }
                    }

                    node = new Node();
                    node.ID = header.ExpiryDate.Year.ToString();
                    node.Name = header.ExpiryDate.Year.ToString();
                    node.Type = "year";
                    if (!nodehash.Contains(node.ID.ToString()))
                    {
                        nodes.Add(node);
                        nodehash.Add(node.ID.ToString());
                    }
                    var edgekey1 = header.ID.ToString() + "_" + node.ID;
                    if (!edgehash.Contains(edgekey1))
                    {
                        var edge = new Edge(header.ID.ToString(), node.ID);
                        edges.Add(edge);
                        edgehash.Add(edgekey1);
                    }
                }
            }
            var ret = new Dictionary<string, object>();
            ret.Add("nodes", nodes);
            ret.Add("edges", edges);
            ret.Add("headers", headercoll);
            return ret;
        }
    }
}
