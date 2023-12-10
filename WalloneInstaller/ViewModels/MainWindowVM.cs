using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WalloneInstaller.Commands;
using WalloneInstaller.ViewModels.Base;
using WalloneInstaller.Views;

namespace WalloneInstaller.ViewModels
{
    public class MainWindowVM : ViewModel
    {
        public Wellcome Wellcome { get; set; }
        public SelectDirectory SelectDirectory { get; set; }
        public EmailSender EmailSender { get; set; }
        public Partners Partners { get; set; }

        private UserControl _SelectedView;

        public UserControl SelectedView
        {
            get => _SelectedView;
            set => Set(ref _SelectedView, value);
        }

        public WellcomeVM WellcomeVM
        {
            get;
            set;
        }

        public SelectDirectoryVM SelectDirectoryVM
        {
            get;
            set;
        }

        public EmailSenserVM EmailSenserVM
        {
            get;
            set;
        }

        public PartnersVM PartnersVM
        {
            get;
            set;
        }

        public MainWindowVM()
        {
            Wellcome = new Wellcome();
            WellcomeVM = new WellcomeVM(this);

            SelectDirectory = new SelectDirectory();
            SelectDirectoryVM = new SelectDirectoryVM(this);

            EmailSender = new EmailSender();
            EmailSenserVM = new EmailSenserVM(this);

            Partners = new Partners();
            PartnersVM = new PartnersVM(this);

            OnPageButtonCommandExecuted("wellcome");
        }

        private ICommand _PageButtonCommand;

        public ICommand PageButtonCommand => _PageButtonCommand ??= _PageButtonCommand =
            new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute);

        private bool CanPageButtonCommandExecute(object p)
        {
            return true;
        }

        public void OnPageButtonCommandExecuted(object p)
        {
            switch (p)
            {
                case "wellcome":
                    SelectedView = Wellcome;
                    SelectedView.DataContext = WellcomeVM;
                break;
                case "select":
                    SelectedView = SelectDirectory;
                    SelectedView.DataContext = SelectDirectoryVM;
                    break;
                case "email":
                    SelectedView = EmailSender;
                    SelectedView.DataContext = EmailSenserVM;
                    break;
                case "partners":
                    SelectedView = Partners;
                    SelectedView.DataContext = PartnersVM;
                    break;
            }
        }

        private ICommand _CloseApplicationCommand;

        public ICommand CloseApplicationCommand => _CloseApplicationCommand ??= _CloseApplicationCommand =
            new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

        private bool CanCloseApplicationCommandExecute(object p)
        {
            return true;
        }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
    }
}
