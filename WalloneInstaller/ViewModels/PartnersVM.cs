using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WalloneInstaller.Commands;
using WalloneInstaller.ViewModels.Base;

namespace WalloneInstaller.ViewModels
{
    public class PartnersVM : ViewModel
    {
        private readonly MainWindowVM _mainWindowVm;

        public PartnersVM()
        {

        }
        public PartnersVM(MainWindowVM mainWindowVm)
        {
            _mainWindowVm = mainWindowVm;

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
            Process.Start("https://wallone.ru/?target=app&version="+ Application.ProductVersion +"&times=" + DateTime.Now.Ticks+"&language=" + Application.CurrentCulture.Name);
            App.Current.Shutdown(); 
        }

    }
}