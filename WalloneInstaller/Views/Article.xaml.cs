using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Article.xaml
    /// </summary>
    public partial class Article : UserControl
    {
        public Article()
        {
            InitializeComponent();
        }

        private void ArticleElem_OnMouseEnter(object sender, MouseEventArgs e)
        {
            //0x242424
            ArticleElem.Background = new SolidColorBrush(Color.FromRgb(29, 29, 29));
        }

        private void ArticleElem_OnMouseLeave(object sender, MouseEventArgs e)
        {
            ArticleElem.Background = new SolidColorBrush(Color.FromRgb(25, 25, 25));
        }
    }
}
