﻿using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WalloneInstaller.Commands;
using WalloneInstaller.ViewModels.Base;

namespace WalloneInstaller.ViewModels
{
    public class WellcomeVM : ViewModel
    {
        private readonly MainWindowVM _mainWindowVm;

        public WellcomeVM()
        {

        }
        public WellcomeVM(MainWindowVM mainWindowVm)
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
            _mainWindowVm.OnPageButtonCommandExecuted("select");
        }

    }
}