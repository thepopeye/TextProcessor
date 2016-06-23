using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TextAnalysis;

namespace TextApp.Controllers
{
    public class GraphController : ApiController
    {
        // GET: api/Graph
        public object Get()
        {
            var txt = File.ReadAllText(@"C:\etc\markov.txt");
            var obj = TextProcessor.Instance.GetTextGraph(txt, 30);
            return obj;
        }

        // GET: api/Graph/5
        public string Get(int id)
        {
            return "value";
        }

    }
}
