using DNA_Project.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DNA_Project.DNA
{
    class Node
    {
        public String ipAddr { get; }
        public int socketPort { get; }
        public String ipAddrServer { get; }
        public int socketPortServer { get; }
        public StateEnum state { get; set; }
        public CPUInfos cpuInfos { get; }
        public Chunck chunck;


        public Node(String ipAddr, int socketPort, String ipAddrServer, int socketPortServer)
        {
            this.ipAddr = ipAddr;
            this.socketPort = socketPort;
            this.ipAddrServer = ipAddrServer;
            this.socketPortServer = socketPortServer;
            cpuInfos = new CPUInfos();
            chunck = new Chunck();

            state = StateEnum.AVAILABLE;

            //For test ONLY !
            chunck.rawDatas = System.IO.File.ReadAllLines(@"C:\Users\mpauly\Documents\CESI RILA\Projet C# DNA\DNA-Data\genome-greshake.txt");
            ParseRawChunkParallel();
        }

        public void ParseRawChunkParallel()
        {
            Parallel.ForEach(chunck.rawDatas, line =>
            {
                // Add here algorithm.
                //line = line.Substring(line.Length, -2);
            });
        }
    }
}
