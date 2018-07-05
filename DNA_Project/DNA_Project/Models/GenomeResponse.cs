using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNA_Project.Models
{
    class GenomeResponse
    {
        public int from;
        public int to;
        public int work;
        public GenomeClass GenomeObj;

        /****JSON OBJECT STRUCTURE*****
        {
          "Response": {
            "from": "NODE_ID",
            "to": "ORCHESTRATOR_ID",
            "work": "WORK_ID",
            "genomeObj": {
              "?": "?",
              "?": "?",
              "?": "?",
              "?": "?",
              "?": "?"
            }
          }
        }
    */

    }
}
