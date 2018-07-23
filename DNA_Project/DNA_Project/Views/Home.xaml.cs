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



namespace DNA_Project
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Orchestrator(object sender, RoutedEventArgs e)
        {
            // Orchestrator Mode
            // Call Orchestrator view
            MessageBox.Show("Orchestrator Mode");


        }
        private void Button_Click_Node(object sender, RoutedEventArgs e)
        {
            // Node Mode
            Node node = new Node("IPaddr",54321,"IPaddr",54321);

            // Call Node view


            // Show Node hardware & network infos
            MessageBox.Show("Node Mode \nProcessor Threads Number : " + node.cpuInfos.maxThreads
                + "\nIP Addr : " + LocalIPAddress.GetLocalIPAddress()
                + "\n Max clock speed per core: " + node.cpuInfos.maxClockFrequency
                + "\nPerformance index : " + node.cpuInfos.performanceIndex
                + "\nCPU Usage : " + node.cpuInfos.GetCpuUsage());
        }
    }
}
