using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Input;
using Aspose.Zip;
using WalloneInstaller.Commands;
using WalloneInstaller.Services;
using WalloneInstaller.ViewModels.Base;

namespace WalloneInstaller.ViewModels
{
    public class InstallerVM : ViewModel
    {
        private readonly MainWindowVM _mainWindowVm;
        private string path = UriService.GetPath() + @"\app.zip";

        public InstallerVM()
        {

        }
        public InstallerVM(MainWindowVM mainWindowVm)
        {
            _mainWindowVm = mainWindowVm;
            ValueProcess = 0;
            IsEnabled = false;

            Thread thread =  new Thread(() =>
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Text = "Скачивание...";
                try
                {
                    using var wb = new WebClient();
                    
                    _= wb.DownloadFileTaskAsync(new Uri("https://dev.w2me.ru/app/wallone/download"), path);
                    wb.DownloadProgressChanged += Wb_DownloadProgressChanged;
                    wb.DownloadFileCompleted += Wb_DownloadFileCompleted;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });
            thread.Start();
        }

        private void Wb_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Text = "Идет распаковка архива...";

            if (File.Exists(path))
            {
                Console.WriteLine(path);
                try
                {
                    using (FileStream zipFile = File.Open(path, FileMode.Open))
                    {
                        using (var archive = new Archive(zipFile))
                        {
                            archive.ExtractToDirectory(UriService.GetPath());
                        }
                    }

                    Text = "Готово! Нажмите продолжить";
                    IsEnabled = true;
                }
                catch (Exception ex)
                {
                    Text = "Ошибка: " + ex.Message;
                    IsEnabled = true;
                }
                File.Delete(path);
            }
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