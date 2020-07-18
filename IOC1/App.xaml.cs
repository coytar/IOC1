using NLog;
using NLog.Config;
using NLog.Targets;
using System.Windows;
using Unity;
using Unity.Injection;
using Unity.NLog;

namespace IOC1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create Unity Container
            IUnityContainer container = new UnityContainer();
            // Add NLog
            container.AddNewExtension<NLogExtension>();
            container.RegisterType<IMainWindowModel, MainWindowModel>();
            container.RegisterType<ILogger>(new InjectionFactory(l => SetupLogger("Test")));

            // AD the container to the application resource
            Application.Current.Resources.Add("IoC", container);

            var mainWindow = container.Resolve<MainWindow>(); // Creating Main window
            mainWindow.Show();
        }

        public static Logger SetupLogger(string name)
        {
            string layoutDebug = "${longdate} - ${level:uppercase=true}: ${message}. ${exception:format=ToString}";
            string layoutTrace = "${longdate} - ${level:uppercase=true} - thread[${threadid}] ${callsite}, Line ${callsite-linenumber}: ${message}. ${exception:format=ToString}";

            string filenameDebug = "logs/{0}.${{shortdate}}.log";
            string filenameTrace = "logs/{0}.TRACE.${{shortdate}}.log";

            // Create Targets
            FileTarget targetDebug = new FileTarget();
            targetDebug.Name = name;
            targetDebug.FileName = string.Format(filenameDebug, name);
            targetDebug.Layout = layoutDebug;
            targetDebug.ReplaceFileContentsOnEachWrite = false;

            FileTarget targetTrace = new FileTarget();
            targetTrace.Name = name;
            targetTrace.FileName = string.Format(filenameTrace, name);
            targetTrace.Layout = layoutTrace;
            targetTrace.ReplaceFileContentsOnEachWrite = false;

            // Create Rules
            LoggingRule ruleDebug = new LoggingRule("Debug");
            ruleDebug.LoggerNamePattern = "*";
            ruleDebug.SetLoggingLevels(LogLevel.Warn, LogLevel.Fatal);
            ruleDebug.Targets.Add(targetDebug);

            LoggingRule ruleTrace = new LoggingRule("Trace");
            ruleTrace.LoggerNamePattern = "*";
            ruleTrace.SetLoggingLevels(LogLevel.Trace, LogLevel.Info);
            ruleTrace.Targets.Add(targetTrace);

            // Create Configuration
            LoggingConfiguration config = new LoggingConfiguration();
            config.AddTarget(name, targetDebug);
            config.AddTarget(name, targetTrace);
            config.LoggingRules.Add(ruleDebug);
            config.LoggingRules.Add(ruleTrace);

            LogFactory factory = new LogFactory();
            factory.Configuration = config;
            return factory.GetCurrentClassLogger();
        }
    }
}
