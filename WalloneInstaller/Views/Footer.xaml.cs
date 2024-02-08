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
    /// Логика взаимодействия для Footer.xaml
    /// </summary>
    public partial class Footer : UserControl
    {
        public Footer()
        {
            InitializeComponent();
        }

        private void LinkSite_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://wallone.ru/?app=installer");
        }

        private void LinkSite_MouseEnter(object sender, MouseEventArgs e)
        {
            LinkSite.Opacity = 1;
        }

        private void LinkSite_MouseLeave(object sender, MouseEventArgs e)
        {
            LinkSite.Opacity = 0.8;
        }
    }
}
