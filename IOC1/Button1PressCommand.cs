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
        MainWindowModel model;
        ILogger logger;

        public Button1PressCommand(MainWindowModel model, ILogger logger)
        {
            this.model = model;
            this.logger = logger;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DateTime dt = DateTime.Now;
            model.Name += dt.Second;
            logger.Info("Button1 clicked " + dt.Second);
        }
    }
}
