﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WalloneInstaller.Commands;
using WalloneInstaller.Services;
using WalloneInstaller.ViewModels.Base;

namespace WalloneInstaller.ViewModels
{
    public class ArticleVM : ViewModel
    {

        public string Filename { get; set; } = Path.GetRandomFileName();
        public string Url { get; set; }

        public bool IsInstalled { get; set; } = false;
        public ArticleVM()
        {
            ProcessVisibility = Visibility.Hidden;
            TextDoneVisibility = Visibility.Hidden;
        }

        public BitmapImage _image;
        public BitmapImage Image
        {
            get => _image;
            set
            {
                Set(ref _image, value);
            }
        }

        private string _name;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _category;

        public string Category
        {
            get => _category;
            set => Set(ref _category, value);
        }

        private int _ValueProcess;

        public int ValueProcess
        {
            get => _ValueProcess;
            set => Set(ref _ValueProcess, value);
        }

        private Visibility _ButtonVisibility = Visibility.Visible;

        public Visibility ButtonVisibility
        {
            get => _ButtonVisibility;
            set => Set(ref _ButtonVisibility, value);
        }

        private Visibility _ProcessVisibility = Visibility.Hidden;

        public Visibility ProcessVisibility
        {
            get => _ProcessVisibility;
            set => Set(ref _ProcessVisibility, value);
        }

        private Visibility _TextDoneVisibility = Visibility.Hidden;

        public Visibility TextDoneVisibility
        {
            get => _TextDoneVisibility;
            set => Set(ref _TextDoneVisibility, value);
        }

        private ICommand _InstallButtonCommand;

        public ICommand InstallButtonCommand => _InstallButtonCommand ??=
            new RelayCommand(OnInstallButtonCommandExecuted, CanInstallButtonCommandExecute);

        private bool CanInstallButtonCommandExecute(object p)
        {
            return true;
        }

        public void OnInstallButtonCommandExecuted(object p)
        {
            if(!IsInstalled)
            {
                Thread thread = new Thread(() =>
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    try
                    {
                        var directoryPath = Path.Combine(UriService.GetPath(), "files");
                        using var wb = new WebClient();

                        _ = wb.DownloadFileTaskAsync(new Uri(Url), Path.Combine(directoryPath, Filename + ".exe"));
                        wb.DownloadProgressChanged += WbOnDownloadProgressChanged;
                        wb.DownloadFileCompleted += WbOnDownloadFileCompleted;
                        ButtonVisibility = Visibility.Hidden;
                        ProcessVisibility = Visibility.Visible;
                        IsInstalled = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                });
                thread.Start();
            }
        }

        private void WbOnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var directoryPath = Path.Combine(UriService.GetPath(), "files");
            var file = Path.Combine(directoryPath, Filename + ".exe");

            ProcessVisibility = Visibility.Hidden;
            TextDoneVisibility = Visibility.Visible;

            try
            {
                if (File.Exists(file))
                {
                    Process.Start($"{file}");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void WbOnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ValueProcess = e.ProgressPercentage;
        }
    }
}