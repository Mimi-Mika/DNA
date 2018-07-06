using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNA_Project.DNA
{
    class Chunck
    {
        public int id { get; set; }
        public int firstBytePosition { get; set; }
        public int lastBytePosition { get; set; }
        public string [] rawDatas { get; set; }
        public string parsedDatas { get; set; }
        
    }
}
