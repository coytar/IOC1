using NLog;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Unity;

namespace IOC1
{
    public class MainWindowModel : IMainWindowModel
    {
        string name;
        ICommand button1PressCommand;

        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowModel()
        {
            name = "Raj";

            // Get the Unity Container from the application resource
            UnityContainer container = (UnityContainer)Application.Current.Resources["IoC"];
            var logger = container.Resolve<ILogger>(); // Creating Main window

            Button1PressCommand = new Button1PressCommand(this, logger);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name { 
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public ICommand Button1PressCommand { get => button1PressCommand; set => button1PressCommand = value; }
    }
}
