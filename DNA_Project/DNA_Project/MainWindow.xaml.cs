﻿using System;
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
            // Call Node view


            // Show Node hardware & network infos
            MessageBox.Show("Node Mode \nProcessor Number : " + Environment.ProcessorCount
                + "\nIP Addr : " + LocalIPAddress.GetLocalIPAddress()
                + "\n Max clock speed per core: " + CPUMaxClockFrequency.GetCPUMaxClockFrequency());
        }
    }
}