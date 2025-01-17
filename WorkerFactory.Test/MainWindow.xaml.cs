﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkerFactory.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            var wait = int.Parse(tbWait.Text);
            new IndeterminatePbarWorker(pbar, btnStart, tbWait)
                .Do((a, b) =>
                {
                    Thread.Sleep(wait);
                })
                .OnComplete((a, b) =>
                {
                    MessageBox.Show("Done!");
                })
                .Start();
        }
    }
}