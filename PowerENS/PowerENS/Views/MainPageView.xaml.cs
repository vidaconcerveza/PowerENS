﻿using LiveCharts;
using PowerENS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

namespace PowerENS.Views
{
    /// <summary>
    /// Interaction logic for ContentsView.xaml
    /// </summary>
    public partial class MainPageView : UserControl
    {
        public MainPageView()
        {
            InitializeComponent();
            this.DataContext = new MainPageViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        //Chart Initializing...
        private void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
