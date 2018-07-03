using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNA_Project.Helper
{
    class Request
    {
        /****JSON OBJECT STRUCTURE*****
         *
         {
          "request": {
            "from": "ORCHESTRATOR_ID",
            "to": "NODE_ID",
            "request": "WORK_ID",
            "chunck": {
              "id": "CHUNK_ID",
              "first_byte_position": "int",
              "last_byte_position": "int",
              "datas": "RAW DATAS"
            }
          },
          "response": {
            "from": "ORCHESTRATOR_ID",
            "to": "NODE_ID",
            "response": "MESSAGE_ID => 0=OK, 1=Error, 404=KO ...",
            "chunck": {
              "id": "CHUNK_ID",
              "first_byte_position": "int",
              "last_byte_position": "int",
              "datas": "DATAS PARSED & TREATED"
            }
          }
        }
        */
    }
}
