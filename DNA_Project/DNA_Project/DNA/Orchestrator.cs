using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DNA_Project.DNA;
using DNA_Project.Helper;

namespace DNA_Project.DNA
{
    internal class Orchestrator
    {
        //private List<Node> nodes;
        //private List<Chunck> chuncks;
        //private List<String> fileData;
        //private String filename { get; set; }
        //private List<String> dataLines;

        public Socket SocketServer { get; private set; }
        private Module1 mod1;
        private Module2 mod2;
        //private List<Socket> SocketsClient;

        public Orchestrator(int _port)
        {
            // ONLY FOR DEBUG MODE
            //filename = @"C:\Users\mpauly\Documents\CESI RILA\Projet C# DNA\DNA-Data\genome-greshake.txt";

            //SocketsClient = new List<Socket>();
            //nodes = new List<Node>();
            //chuncks = new List<Chunck>();
            //fileData = new List<string>();

            // Start server mode here to listen new connexion
            SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            SocketServer.Bind(new IPEndPoint(LocalIPAddress.GetLocalIPAddressV2(), _port));
            SocketServer.Listen(1);

            SocketServer.BeginAccept(new AsyncCallback(this.connexionAcceptCallback), SocketServer);

            mod1 = new Module1();
            mod2 = new Module2();
            //TODO Creer les nodes et s'y connecte
            /* Creer des classes modules, héritant de la classe Node
             * Chaque module est un node  qui  realise le traitement demandé dans le sujet (module 1 réponds à l'énnoncé de module 1)
             * Donc chaque module découpe si besoin et à sa façon le contenue, puis retourne son résultat à la classe Orcherstrator, qui elle la transferera au client
             */
        }

        // Start work
        public async void Process(String content)
        {
            Console.WriteLine("Read {0} bytes from socket. \n Data : {1}");
            var process1Task = mod1.Process(content);
            var process2Task = mod2.Process(content);
            //TODO a completer

            await Task.WhenAll(process1Task, process2Task);

            Object resMod1 = await process1Task;
            Object resMod2 = await process2Task;

            //dataLines = System.IO.File.ReadAllLines(filename).ToList();

            //TODO Envoyer à chaque node le contenue du fichier
        }

        private void connexionAcceptCallback(IAsyncResult ar)
        {
            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);

            //SocketsClient.Add(SocketServer.EndAccept(ar));
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            // Read data from the client socket.
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read
                // more data.
                content = state.sb.ToString();
                Console.WriteLine("C PAS FINI");
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the
                    //Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                    //content.Length, content);
                    Console.WriteLine("C FINI");
                    Process(content);
                }
                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }
    }
}