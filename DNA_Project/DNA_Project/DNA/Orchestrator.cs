using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DNA_Project.DNA;
using DNA_Project.Helper;


namespace DNA_Project.DNA
{
    class Orchestrator
    {
        private List<Node> nodes;
        private List<Chunck> chuncks;
        private List<String> fileData;
        private String filename { get; set; }
        private List<String> dataLines;

        Socket SocketServer;
        List<Socket> SocketsClient;

        public Orchestrator()
        {
            // ONLY FOR DEBUG MODE
            filename = @"C:\Users\mpauly\Documents\CESI RILA\Projet C# DNA\DNA-Data\genome-greshake.txt";

            // Start server mode here to listen new connexion
            SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            SocketServer.Bind(new IPEndPoint(LocalIPAddress.GetLocalIPAddressV2(), 54321));
            SocketServer.Listen(1);

            SocketServer.BeginAccept(new AsyncCallback(this.connexionAcceptCallback), SocketServer);

        }


        // Start work
        public void Process()
        {
            dataLines = System.IO.File.ReadAllLines(filename).ToList();
            
            // Mapping & send datas to Nodes

        }

        private void connexionAcceptCallback(IAsyncResult asyncResult)
        {
            SocketsClient.Add(SocketServer.EndAccept(asyncResult));
        }
    }



}
