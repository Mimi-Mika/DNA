using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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

        public MainWindow()
        {
            InitializeComponent();

            if (!this.connect)
            {
                urlFile.IsEnabled = false;
                browse.IsEnabled = false;
                if (urlFile == null || urlFile.Text == "")
                {
                    fileOk.IsEnabled = false;
                }
            }

            String adressIP = LocalIPAddress.GetLocalIPAddress();
            if (adressIP != null && adressIP != "")
            {
                adressServer.Text = adressIP;
            }
            else
            {
                logServer.Text += "Error when retrieving the IP address !";
                connectServer.IsEnabled = false;
            }
            portServer.Text = "54321";
        }

        // Orchestrator Mode
        private void BtnClickConnectClient(object sender, RoutedEventArgs e)
        {
            // TODO : action connect client
            MessageBox.Show("Orchestrator Mode");

            if (this.connect)
            {
                tabServer.IsEnabled = false;
                connectServer.IsEnabled = false;

                logClient.Text += "Client connecté !";
            }
        }

        // Node Mode
        private void BtnClickConnectServer(object sender, RoutedEventArgs e)
        {
            // TODO : action connect server
            MessageBox.Show("Node Mode");

            //TODO
            this.connect = true;

            /* // Node Mode
            Node node = new Node("IPaddr", 54321, "IPaddr", 54321);

             // Show Node hardware & network infos
             MessageBox.Show("Node Mode \nProcessor Threads Number : " + node.cpuInfos.maxThreads
                 + "\nIP Addr : " + LocalIPAddress.GetLocalIPAddress()
                 + "\n Max clock speed per core: " + node.cpuInfos.maxClockFrequency
                 + "\nPerformance index : " + node.cpuInfos.performanceIndex
                 + "\nCPU Usage : " + node.cpuInfos.GetCpuUsage());*/

            if (this.connect)
            {
                tabClient.IsEnabled = false;
                adressclient.IsEnabled = false;
                portClient.IsEnabled = false;
                connectClient.IsEnabled = false;
                urlFile.IsEnabled = true;
                browse.IsEnabled = true;

                logServer.Text += "Serveur connecté !";
            }
        }

        private void BtnClickFileOk(object sender, RoutedEventArgs e)
        {
            // TODO : action add file
            MessageBox.Show("File ok");

            if (urlFile != null && urlFile.Text != "")
            {
                logServer.Text += "Fichier ajouté !";
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