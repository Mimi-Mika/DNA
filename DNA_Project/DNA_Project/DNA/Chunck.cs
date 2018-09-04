using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNA_Project.DNA
{
    public class Chunck
    {
        public string id { get; }
        public int chromosome { get;  }
        public long position { get; }
        public string genotype { get; }

        public Chunck(string str)
        {
            int outInt;
        long outLong;
            //Parse data & store in chunck
            string[] split = str.Split('\t');
            this.id = split[0];

            Int32.TryParse(split[1], out outInt);
            chromosome = outInt;
            long.TryParse(split[2], out outLong);
            position = outLong;
            genotype = split[3];
        }
    }




}
