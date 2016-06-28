using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalysis;

namespace PatentDB
{
    public class Patent
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Center { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string Abstract { get; set; }

        public string Description { get; set; }

        public string Claims { get; set; }

        public List<string> Inventors { get; set; }

        public Patent()
        {
            Inventors = new List<string>();
        }
    }

    public class PatentHeader
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Center { get; set; }

        public DateTime ExpiryDate { get; set; }

        public List<string> Inventors { get; set; }

        public Dictionary<string, double> KeyWords { get; set; }

        public PatentHeader()
        {
            Inventors = new List<string>();
            KeyWords = new Dictionary<string, double>();
        }

        public PatentHeader(Patent patent, int keywordcount = 20)
        {
            ID = patent.ID;
            Title = patent.Title;
            Center = patent.Center;
            ExpiryDate = patent.ExpiryDate;
            Inventors = patent.Inventors;
            KeyWords = TextProcessor.Instance.GetKeyWords(patent.Abstract + ". " + patent.Description + "." + patent.Claims, keywordcount);
        }
    }
}