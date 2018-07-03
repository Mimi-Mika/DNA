using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNA_Project.DNA;

namespace DNA_Project.Models
{
    class RequestResponse
    {
        public int from;
        public int to;
        public int work;
        public Chunck chunk;

        /****JSON OBJECT STRUCTURE*****
        {
          "RequestResponse": {
            "from": "ORCHESTRATOR_ID or NODE_ID",
            "to": "ORCHESTRATOR_ID or NODE_ID",
            "work": "WORK_ID",
            "chunck": {
              "id": "CHUNK_ID",
              "first_byte_position": "int",
              "last_byte_position": "int",
              "datas": "RAW DATAS or TREATED DATAS"
            }
          }
        }


        */
    }
}
