using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DNA_Project.Comm;
using DNA_Project.DNA;
using DNA_Project.Helper;



namespace DNA_Project.Comm
{
    public class AsyncServer
    {
        // Thread signal.  
        public Socket SocketServer { get; private set; }
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public AsyncServer(int _port)
        {
            StartListening(_port);
        }

        public void StartListening(int _port)
        {

            IPEndPoint localEndPoint = new IPEndPoint(LocalIPAddress.GetLocalIPAddressV2(), _port);
            Console.WriteLine("IP ADDRESSE : " + LocalIPAddress.GetLocalIPAddress());
            // Create a TCP/IP socket.  

            SocketServer = new Socket(LocalIPAddress.GetLocalIPAddressV2().AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            SocketServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                SocketServer.Bind(localEndPoint);
                SocketServer.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    SocketServer.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        SocketServer);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket SocketServer = (Socket)ar.AsyncState;
            Socket handler = SocketServer.EndAccept(ar);

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallbackAsync), state);
        }

        public static async void ReadCallbackAsync(IAsyncResult ar)
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
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the   
                    // client. Display it on the console.  
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);
                    content.Replace("<EOF>", "");

                    Object countbases = await Process(content);

                    // Echo the data back to the client.
                    Send(handler, countbases.ToString());
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallbackAsync), state);
                }
            }
        }

        private async static Task<object> Process(string content)
        {

            Module1 mod1 = new Module1();
            //mod2 = new Module2();

            var process1Task = mod1.Process(content);
            //var process2Task = mod2.Process(content);
            //TODO a completer

            await Task.WhenAll(process1Task);
            //await Task.WhenAll(process1Task, process2Task);

            var resMod1 = await process1Task;
            //Object resMod2 = await process2Task;

            //dataLines = System.IO.File.ReadAllLines(filename).ToList();

            //TODO Envoyer à chaque node le contenue du fichier

            return resMod1;
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                //handler.Shutdown(SocketShutdown.Both);
                handler.Disconnect(true);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}