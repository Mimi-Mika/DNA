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
using DNA_Project.Comm;
using DNA_Project.Helper;
using Microsoft.Win32;

namespace DNA_Project
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Boolean connect = false;
        private OpenFileDialog dlg;
        private AsyncServer orchestrator;
        private AsynchronousClient client;
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendBegin = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private System.Net.IPAddress ipAdress = null;
        private int port;

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
        private void BtnClickStartServerAsync(object sender, RoutedEventArgs e)
        {
            if (!connect)
            {
                int port;
                if (int.TryParse(portClient.Text, out port))
                {
                    orchestrator = new AsyncServer(port);
                    IPEndPoint localIpEndPoint = orchestrator.SocketServer.LocalEndPoint as IPEndPoint;
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
            else
            {
                //TODO doite de dialogue connected
            }
        }

        // client Mode
        private void BtnClickConnectToServer(object sender, RoutedEventArgs e)
        {
            if(!connect)
            {
                if (IPAddress.TryParse(adressServer.Text, out ipAdress) && int.TryParse(portServer.Text, out port))
                {
                    logServer.Text += "Connexion en cours";
                    logServer.Text += Environment.NewLine;

                    client = new AsynchronousClient(ipAdress, port);

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
            else
            {
                //TODO boite de dialogue connected
            }
        }

        private void BtnClickFileOk(object sender, RoutedEventArgs e)
        {
            if (urlFile != null && urlFile.Text != "")
            {
                logServer.Text += "Envoie du fichier au serveur, patientez...";

                logServer.Text += Environment.NewLine;
                // TODO MAP
                String[] lines = File.ReadAllLines(urlFile.Text);
                StringBuilder strToSend = new StringBuilder();
                int i = 0;
                String response="";

                while (lines.Count() != i)
                {
                    // Begin sending the data to the remote device.
                    //response = client.SendToServer(lines[i]);
                    response = response + client.SendToServer(lines[i]) + "\n";
                    // TODO REDUCE
                    i = i+1;
                    strToSend.Clear();
                }
                Console.WriteLine("MAINWINDOWS Server response : " + response);


                logServer.Text += Environment.NewLine;
                // RECEIVE RESULT & reduce
                //results
                //logServer.Text += result;
                //logServer.Text += Environment.NewLine;

            }
            else{
                logServer.Text += "Fichier introuvable";
                logServer.Text += Environment.NewLine;
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