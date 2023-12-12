using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using WalloneInstaller.Commands;
using WalloneInstaller.Services;
using WalloneInstaller.ViewModels.Base;
using WalloneInstaller.Views;

namespace WalloneInstaller.ViewModels
{
    public class SelectDirectoryVM : ViewModel
    {
        private readonly MainWindowVM _mainWindowVm;

        private string path = @"AbsoluteGroup\Wallone";
        private string _text;

        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }
        public SelectDirectoryVM()
        {

        }
        public SelectDirectoryVM(MainWindowVM mainWindowVm)
        {
            _mainWindowVm = mainWindowVm;
            Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), path);
        }

        private ICommand _OpenDialogButtonCommand;

        public ICommand OpenDialogButtonCommand => _OpenDialogButtonCommand ??=
            new RelayCommand(OnOpenDialogButtonCommandExecuted, CanOpenDialogButtonCommandExecute);

        private bool CanOpenDialogButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnOpenDialogButtonCommandExecuted(object p)
        {
            using var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Text = Path.Combine(dialog.SelectedPath, path);
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
            try
            {
                UriService.SetPath(Text);
                Directory.CreateDirectory(Text);

                var directoryPath =  Path.Combine(UriService.GetPath(), "files");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                _mainWindowVm.SelectedView = new Installer();
                _mainWindowVm.SelectedView.DataContext = new InstallerVM(_mainWindowVm);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Недостаточно прав");
            }
        }
    }
}