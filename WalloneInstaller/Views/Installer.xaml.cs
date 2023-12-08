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

namespace WalloneInstaller.Views
{
    /// <summary>
    /// Логика взаимодействия для Installer.xaml
    /// </summary>
    public partial class Installer : UserControl
    {
        public Installer()
        {
            InitializeComponent();
        }


        private void Appwizard_OnMouseDown(object sender, MouseEventArgs e)
        {
            Process.Start("https://extlinka.ru/r/32226/");
        }
    }
}
