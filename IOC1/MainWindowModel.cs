using NLog;
using System;
using System.Windows;
using System.Windows.Input;
using Unity;

namespace IOC1
{
    public class MainWindowModel : IMainWindowModel
    {
        string name;
        ILogger logger;
        ICommand button1PressCommand;

        public event EventHandler CanExecuteChanged;

        public MainWindowModel()
        {
            name = "Raj";
            UnityContainer container = (UnityContainer)Application.Current.Resources["IoC"];
            var logger = container.Resolve<ILogger>(); // Creating Main window

            Button1PressCommand = new Button1PressCommand(logger);
        }

        public string Name { get => name; set => name = value; }
        public ICommand Button1PressCommand { get => button1PressCommand; set => button1PressCommand = value; }
    }
}
