using System;
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
    /// Логика взаимодействия для SelectDirectory.xaml
    /// </summary>
    public partial class SelectDirectory : UserControl
    {
        public SelectDirectory()
        {
            InitializeComponent();
        }

        private void Appwizard_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://extlinka.ru/r/32226/");
        }

        private void LogoWallone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://wallone.app/?app=installer");
        }
    }
}
