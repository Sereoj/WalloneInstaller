using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalloneInstaller.Commands
{
    internal class RelayCommand : Command
    {
        private readonly Func<object, bool> _CanExecute;
        private readonly Action<object> _Execute;

        public RelayCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        override public bool CanExecute(object parameter)
        {
            return _CanExecute?.Invoke(parameter) ?? true;
        }

        override public void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            _Execute(parameter);
        }
    }
}
