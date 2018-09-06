using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DNA_Project.DNA;
using DNA_Project.Helper;
using Microsoft.Win32;

namespace DNA_Project
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Boolean connect;
        private OpenFileDialog dlg;
        private Orchestrator server;
        private Socket socketConnection;
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendBegin = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);

        public MainWindow()
        {
            InitializeComponent();

            if (!this.connect)
            {
                if (urlFile == null || urlFile.Text == "")
                {
                    fileOk.IsEnabled = false;
                }
            }
        }

        // Orchestrator Mode
        private void BtnClickConnectClient(object sender, RoutedEventArgs e)
        {
            int port;
            if (int.TryParse(portClient.Text, out port))
            {
                server = new Orchestrator(port);
                IPEndPoint localIpEndPoint = server.SocketServer.LocalEndPoint as IPEndPoint;
                if (localIpEndPoint != null)
                {
                    connect = true;
                    tabServer.IsEnabled = false;
                    connectServer.IsEnabled = false;

                    logClient.Text = "Serveur démarré !";
                    logClient.Text += Environment.NewLine;
                    logClient.Text += "Adresse ip du cluster : " + localIpEndPoint.Address + " (localhost)";
                    logClient.Text += Environment.NewLine;
                    logClient.Text += "Port du cluster : " + localIpEndPoint.Port;
                }
                else
                {
                    logClient.Text = "Erreur impossible de démarré le serveur, vérifiez que le port est disponible !";
                }
            }
            else
            {
                logClient.Text = "Le port doit etre un numéro !";
            }
        }

        // Node Mode
        private void BtnClickConnectServer(object sender, RoutedEventArgs e)
        {
            System.Net.IPAddress ipAdress = null;
            int port;

            if (IPAddress.TryParse(adressServer.Text, out ipAdress) && int.TryParse(portServer.Text, out port))
            {
                logServer.Text += "Connexion en cours";
                logServer.Text += Environment.NewLine;

                IPEndPoint remoteEP = new IPEndPoint(ipAdress, port);

                // Create a TCP/IP socket.
                socketConnection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                socketConnection.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), socketConnection);
                connectDone.WaitOne();

                connect = true;

                tabClient.IsEnabled = false;
                portClient.IsEnabled = false;
                connectClient.IsEnabled = false;
                urlFile.IsEnabled = true;
                browse.IsEnabled = true;

                logServer.Text += "Connecté au serveur !";
                logServer.Text += Environment.NewLine;
            }
            else
            {
                logServer.Text += "Valeurs incorrectes (ip /port)";
                logServer.Text += Environment.NewLine;
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void BtnClickFileOk(object sender, RoutedEventArgs e)
        {
            if (urlFile != null && urlFile.Text != "")
            {
                /*logServer.Text += "Envoie du fichier au serveur, patientez...";
                logServer.Text += Environment.NewLine;
                // TODO MAP

                byte[] byteData = File.ReadAllBytes(urlFile.Text);
                // Convert the string data to byte data using ASCII encoding.
                // Begin sending the data to the remote device.
                socketConnection.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), socketConnection);
                sendBegin.WaitOne();

                byte[] endFile = Encoding.UTF8.GetBytes("<EOF>");
                socketConnection.BeginSend(endFile, 0, endFile.Length, 0,
                    new AsyncCallback(EndSendCallback), socketConnection);

                sendDone.WaitOne();
                logServer.Text += "Envoie de fichier terminé, Traitement en cours, patientez ...";
                logServer.Text += Environment.NewLine;

                // TODO REDUCE
                // RECEIVE RESULT & COMPUTE*/
 
                logServer.Text += "Envoie du fichier au serveur, patientez...";
                logServer.Text += Environment.NewLine;

                // MAP
                byte[] byteData;
                String[] lines = File.ReadAllLines(urlFile.Text);
                StringBuilder strToSend = new StringBuilder();

                int i = 0;
                int j = 0;
                char[] MyChar = { ' ' };
                object[] result = new object[lines.Count()]; 
                while (lines.Count() != i)
                {
  
                    strToSend.Append(lines[i]/*.Replace(" ", "")*/);
                    //Console.WriteLine(lines[i].Replace(" ", ""));

                    //Converte string to byte array
                    byteData = Encoding.UTF8.GetBytes(strToSend.ToString());
                    //Send byte array
                    // Convert the string data to byte data using ASCII encoding.
                    // Begin sending the data to the remote device.
                    socketConnection.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), socketConnection);
                    sendBegin.WaitOne();

                    byte[] endFile = Encoding.UTF8.GetBytes("<EOF>");
                    socketConnection.BeginSend(endFile, 0, endFile.Length, 0, new AsyncCallback(EndSendCallback), socketConnection);

                    sendDone.WaitOne();

                    // TODO REDUCE
                    //Receive resulte
                    result[i] = server.Process(lines[i]);


                    // increment i + 500
                    i = i+1;
                    strToSend.Clear();
                }
                logServer.Text += "Envoie de fichier terminé, Traitement en cours, patientez ...";
                logServer.Text += Environment.NewLine;
                // RECEIVE RESULT and reduce this & COMPUTE
                //results
                //logServer.Text += result;
                //logServer.Text += Environment.NewLine;

            }
            else{
                logServer.Text += "Fichier introuvable";
                logServer.Text += Environment.NewLine;
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);

                // Signal that all bytes have been sent.
                sendBegin.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void EndSendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void BtnClickBrowse(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                urlFile.Text = filename;
                fileOk.IsEnabled = true;
            }
        }
    }
}