using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using PatentDB;
using TextAnalysis;

namespace TextApp.Controllers
{
    public class GraphController : ApiController
    {
        [ActionName("textgraph")]
        public object GetTextGraph([FromUri]int id)
        {
            var patent = PatentDBController.Default.Load(id);
            if (null == patent) return null;
            var txt = patent.Abstract + "." + patent.Claims + "." + patent.Description;
            var obj = TextProcessor.Instance.GetTextGraph(txt, 30);
            return obj;
        }

        [ActionName("summary")]
        public object GetSummary([FromUri]int id)
        {
            var patent = PatentDBController.Default.Load(id);
            if (null == patent) return null;
            var txt = patent.Description;
            var obj = TextProcessor.Instance.GetSummary(txt);
            var srb = new StringBuilder();
            int count = 0;
            for(int i = 0; i < obj.Count; i++)
            {
                var line = obj.ElementAt(i).Key;
                if (line.Length > 40)
                {
                    srb.AppendLine(line);
                    count++;
                }
                if (count >= 10) break;
            }
            var dic = new Dictionary<string, object>();
            dic["summary"] = srb.ToString();
            dic["abstract"] = patent.Abstract;
            return dic;
        }

        [HttpGet]
        [ActionName("keywordgraph")]
        public object GetKeywordGraph([FromUri]string id)
        {
            var words = id.Split(',').ToList();
            var graph = PatentDBController.Default.GetDocumentsGraph(words);
            return graph;
        }

    }
}
