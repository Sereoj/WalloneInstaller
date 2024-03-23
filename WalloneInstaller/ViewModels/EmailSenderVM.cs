using Newtonsoft.Json;
using System;
using System.Net.Mail;
using System.Windows.Forms;
using System.Windows.Input;
using WalloneInstaller.Commands;
using WalloneInstaller.Services;
using WalloneInstaller.ViewModels.Base;

namespace WalloneInstaller.ViewModels
{
    public class EmailSenserVM : ViewModel
    {
        private readonly MainWindowVM _mainWindowVm;

        public EmailSenserVM()
        {

        }
        public EmailSenserVM(MainWindowVM mainWindowVm)
        {
            _mainWindowVm = mainWindowVm;

        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                Set(ref email, value);
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
            _mainWindowVm.OnPageButtonCommandExecuted("partners");
        }

        private ICommand _SubButtonCommand;

        public ICommand SubButtonCommand => _SubButtonCommand ??=
            new RelayCommand(OnSubButtonCommandExecuted, CanSubButtonCommandExecute);

        private bool CanSubButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnSubButtonCommandExecuted(object p)
        {

            if (!string.IsNullOrEmpty(Email) && checkEmail(Email))
            {
                var test = RequestRouter.EmailRequest(Email);
                Console.WriteLine(test);
                _mainWindowVm.OnPageButtonCommandExecuted("partners");
            }
            else
            {
                MessageBox.Show("Укажите действующий реальный Email",
                    "Wallone Installer",
                    MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Warning);
            }
        }

        private bool checkEmail(string email)
        {
            try
            {
                Console.WriteLine(new MailAddress(email));
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}