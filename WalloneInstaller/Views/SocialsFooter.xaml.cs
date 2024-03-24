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

namespace WalloneInstaller.Views
{
    /// <summary>
    /// Логика взаимодействия для SocialsFooter.xaml
    /// </summary>
    public partial class SocialsFooter : UserControl
    {
        public SocialsFooter()
        {
            InitializeComponent();
        }

        private void VKButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://vk.com/wallone");
        }

        private void TelegramButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://t.me/wallone");
        }
    }
}
