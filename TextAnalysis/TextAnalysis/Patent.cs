using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis
{
    public class Patent
    {
        public string PatentNumber { get; set; }

        public string Abstract { get; set; }

        public string Description { get; set; }

        public string Claims { get; set; }

        public Patent()
        {

        }
    }
}
