﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WalloneInstaller.Services;

namespace WalloneInstaller.Views
{
    /// <summary>
    /// Логика взаимодействия для EmailSender.xaml
    /// </summary>
    public partial class EmailSender : UserControl
    {
        public EmailSender()
        {
            InitializeComponent();
        }
        private void LogoWallone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://wallone.app/?app=installer");
        }
    }
}
