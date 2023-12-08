using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WalloneInstaller.Commands;
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

            Thread myThread = new Thread(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    ValueProcess++;
                    Thread.Sleep(100);
                }

                Text = "Идет установка...";
                Thread.Sleep(2000);
                Text = "Установка завершена!";
            });
            myThread.Start();

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
                Text = $"Скачано: " + valueProcess + "%";
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