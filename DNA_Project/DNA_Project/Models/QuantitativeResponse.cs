using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNA_Project.Models
{
    class QuantitativeResponse
    {
        public int from;
        public int to;
        public int work;
        public QuantitativeClass quantitativeObj;

        /****JSON OBJECT STRUCTURE*****
        {
          "Response": {
            "from": "NODE_ID",
            "to": "ORCHESTRATOR_ID",
            "work": "WORK_ID",
            "quantitativeObj": {
              "aCounter": "int",
              "cCounter": "int",
              "tdcCounter "int",
              "gCounter": "int",
              "unknowCounter": "int"
            }
          }
        }
    */

    }
}
