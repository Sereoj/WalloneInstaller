using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using WalloneInstaller.Commands;
using WalloneInstaller.ViewModels.Base;

namespace WalloneInstaller.ViewModels
{
    public class SelectDirectoryVM : ViewModel
    {
        private readonly MainWindowVM _mainWindowVm;
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
            Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
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
                Text = dialog.SelectedPath;
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
            _mainWindowVm.OnPageButtonCommandExecuted("installer");
        }
    }
}