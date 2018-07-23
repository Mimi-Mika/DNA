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
        }

        private void BtnClickConnectClient(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Client connecté");
        }

        private void BtnClickConnectServer(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Serveur connecté");
        }
    }
}