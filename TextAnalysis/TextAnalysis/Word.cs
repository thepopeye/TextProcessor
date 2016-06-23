using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TextAnalysis
{
    public class Word
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("lexname")]
        public string LexName { get; set; }

        [JsonProperty("words")]
        public List<string> RelatedWords { get; set; }

        [JsonProperty("relationships")]
        public WordRelations Relations { get; set; }

        public Word()
        {
            RelatedWords = new List<string>();
            Relations = new WordRelations();
        }
    }

    public class WordRelations
    {
        [JsonProperty("type_of")]
        public List<string> OfType { get; set; }

        [JsonProperty("made_with")]
        public List<string> MadeWith { get; set; }

        [JsonProperty("parts")]
        public List<string> Parts { get; set; }

        [JsonProperty("members")]
        public List<string> Members { get; set; }

        public WordRelations()
        {
            OfType = new List<string>();
            MadeWith = new List<string>();
            Parts = new List<string>();
            Members = new List<string>();
        }
    }
}
