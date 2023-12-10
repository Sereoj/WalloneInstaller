using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WalloneInstaller.Commands;
using WalloneInstaller.Services;
using WalloneInstaller.ViewModels.Base;

namespace WalloneInstaller.ViewModels
{
    public class InstallerVM : ViewModel
    {
        private readonly MainWindowVM _mainWindowVm;

        public InstallerVM()
        {

        }
        public InstallerVM(MainWindowVM mainWindowVm)
        {
            _mainWindowVm = mainWindowVm;
            ValueProcess = 0;
            IsEnabled = false;

            Thread myThread = new Thread(() =>
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Text = "Скачивание...";
                try
                {
                    using var wb = new WebClient();
                    wb.DownloadFile(new Uri("http://dev.w2me.ru/app/wallone/download"), UriService.GetPath() + "//app.rar");
                    wb.DownloadProgressChanged += Wb_DownloadProgressChanged;
                    wb.DownloadFileCompleted += Wb_DownloadFileCompleted;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });
            myThread.Start();

        }

        private void Wb_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Text = "Идет установка...";
            IsEnabled = true;
        }

        private void Wb_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ValueProcess = e.ProgressPercentage;
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
        private int valueProcess;
        public int ValueProcess
        {
            get => valueProcess;
            set
            {
                Set(ref valueProcess, value);
                Text = "Скачано: " + valueProcess + "%";
            }
        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                Set(ref isEnabled, value);
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
            _mainWindowVm.OnPageButtonCommandExecuted("email");
        }

    }
}