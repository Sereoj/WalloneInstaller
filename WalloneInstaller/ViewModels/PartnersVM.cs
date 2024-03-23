using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WalloneInstaller.Commands;
using WalloneInstaller.Services;
using WalloneInstaller.ViewModels.Base;

namespace WalloneInstaller.ViewModels
{
    public class PartnersVM : ViewModel
    {
        private readonly MainWindowVM _mainWindowVm;

        public PartnersVM()
        {
            Articles.Add(new ArticleVM()
            {
                Name = "Test",
                Category = "partner.category",
            });
            Articles.Add(new ArticleVM()
            {
                Name = "Test",
                Category = "partner.category",
            });
            Articles.Add(new ArticleVM()
            {
                Name = "Test",
                Category = "partner.category",
            });
            Articles.Add(new ArticleVM()
            {
                Name = "Test",
                Category = "partner.category",
            });
        }

        private string text;
        public string Text
        {
            get => text;
            set
            {
                Set(ref text, value);

            }
        }

        public ObservableCollection<ArticleVM> Articles
        {
            get;
            set;
        } = new ObservableCollection<ArticleVM>();

        public PartnersVM(MainWindowVM mainWindowVm)
        {
            var partners = RequestRouter.getPartners();
            foreach (var partner in partners)
            {
                Articles.Add(new ArticleVM()
                {
                    Image = new BitmapImage(new Uri(partner.icon)),
                    Name = partner.name,
                    Category = partner.category,
                    Url = partner.download
                });
            }
            _mainWindowVm = mainWindowVm;

        }

        private ICommand _InstallAllButtonCommand;

        public ICommand InstallAllButtonCommand => _InstallAllButtonCommand ??=
            new RelayCommand(OnInstallAllButtonCommandExecuted, CanInstallAllButtonCommandExecute);

        private bool CanInstallAllButtonCommandExecute(object p)
        {
            return true;
        }

        public void OnInstallAllButtonCommandExecuted(object p)
        {
            foreach (var item in Articles)
            {
                item.OnInstallButtonCommandExecuted(true);
                if(item.IsInstalled)
                {
                    Text = $"Приложнение {item.Name} уже установлено";
                }
            }

        }


        private ICommand _ContinueButtonCommand;

        public ICommand ContinueButtonCommand => _ContinueButtonCommand ??=
            new RelayCommand(OnContinueButtonCommandExecuted, CanContinueButtonCommandExecute);

        private bool CanContinueButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnContinueButtonCommandExecuted(object p)
        {
            Process.Start("https://wallone.app/?target=app&version=" + Application.ProductVersion +"&times=" + DateTime.Now.Ticks+"&language=" + Application.CurrentCulture.Name);
            Process.Start(Path.Combine(UriService.GetPath(), "Wallone.UI.exe"));
            App.Current.Shutdown();
        }

    }
}