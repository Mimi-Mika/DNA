using System;
using System.Collections.Generic;
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

namespace DNA_Project.Views
{
    /// <summary>
    /// Logique d'interaction pour Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            String adressIP = LocalIPAddress.GetLocalIPAddress();
            if (adressIP != null && adressIP != "")
            {
                adressServer.Text = adressIP;
            }
            else
            {
                logServer.Text = "Error when retrieving the IP address !";
                connectServer.IsEnabled = false;
            }
            portServer.Text = "54321";
        }

        private void BtnClickConnectClient(object sender, RoutedEventArgs e)
        {
            Boolean connect = false;
            // TODO : action connect client
            MessageBox.Show("Client connecté");

            if (connect)
            {
                tabServer.IsEnabled = false;
                connectServer.IsEnabled = false;
            }
        }

        private void BtnClickConnectServer(object sender, RoutedEventArgs e)
        {
            Boolean connect = false;
            // TODO : action connect server
            MessageBox.Show("Serveur connecté");

            if (connect)
            {
                tabClient.IsEnabled = false;
                adressclient.IsEnabled = false;
                portClient.IsEnabled = false;
                connectClient.IsEnabled = false;
            }
        }
    }
}