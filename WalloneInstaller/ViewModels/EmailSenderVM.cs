using Newtonsoft.Json;
using System;
using System.Net.Mail;
using System.Threading;
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
        private Thread myThread;

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

        private double opacity;
        public double Opacity
        {
            get => opacity;
            set
            {
                Set(ref opacity, value);
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
            Opacity = 0;
            if (!string.IsNullOrEmpty(Email) && checkEmail(Email))
            {
                var test = RequestRouter.EmailRequest(Email);
                Console.WriteLine(test);
                _mainWindowVm.OnPageButtonCommandExecuted("partners");
            }
            else
            {
                myThread = new Thread(message);
                myThread.Start();
            }
        }
        private void message()
        {
            for (int i = 0; i <= 10; i++)
            {
                Opacity += 0.2;
                Thread.Sleep(10);
            }
            myThread.Abort();
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