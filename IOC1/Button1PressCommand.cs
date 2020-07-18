using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IOC1
{
    public class Button1PressCommand : ICommand
    {
        ILogger logger;

        public Button1PressCommand(ILogger logger)
        {
            this.logger = logger;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            logger.Info("Button1 clicked");
        }
    }
}
