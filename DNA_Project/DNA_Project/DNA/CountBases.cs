using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNA_Project.DNA
{
    public class CountBases
    {
        public long basesA { get;  set; }
        public long basesT { get; set; }
        public long basesG { get; set; }
        public long basesC { get; set; }
        public long basesUndefine { get; set; }

        public double TotalBases()
        {
            return basesA + basesT + basesG + basesC+ basesUndefine;
        }
        public double PercentBaseA()
        {
            double total = TotalBases();
            if (total == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(basesA / total * 100d, 2);
            }
        }
        public double PercentBaseT()
        {
            double total = TotalBases();
            if (total == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(basesT / total * 100d, 2);
            }
        }
        public double PercentBaseG()
        {
            double total = TotalBases();
            if (total == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(basesG / total * 100d, 2);
            }
        }
        public double PercentBaseC()
        {
            double total = TotalBases();
            if (total == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(basesC/ total * 100d, 2);
            }
        }
        public double PercentBaseundefine()
        {
            double total = TotalBases();
            if (total == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(basesUndefine / total * 100d, 2);
            }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("A : ").Append(PercentBaseA()).Append("%\n");
            str.Append("T : ").Append(PercentBaseT()).Append("%\n");
            str.Append("G : ").Append(PercentBaseG()).Append("%\n");
            str.Append("C : ").Append(PercentBaseC()).Append("%\n");
            str.Append("Undefine : ").Append(PercentBaseundefine()).Append("%\n");
            str.Append("Total Bases Count : ").Append(TotalBases()).Append("\n");
            return str.ToString();
        }

    }



}
