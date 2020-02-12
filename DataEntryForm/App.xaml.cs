using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Conditions;
using NLog.Targets;
using Services;
using ServicesInterfaces;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using ViewModelInterfaces;
using ViewModels;

namespace Data_Entry_Form
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _mainWindow;
        private ReviewWindow _reviewWindow;
        private IServiceProvider _serviceProvider;


        public App()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
          
            serviceCollection.AddTransient<IReviewWindowVM, ReviewWindowVM>();
            serviceCollection.AddTransient<IMainWindowVM, MainWindowVM>();
            serviceCollection.AddSingleton<IDatabaseService, MicrosoftAccess>();

            serviceCollection.AddTransient<MainWindow, MainWindow>();
            serviceCollection.AddTransient<ReviewWindow, ReviewWindow>();

            _serviceProvider = serviceCollection.BuildServiceProvider();

            _mainWindow = _serviceProvider.GetService<MainWindow>();
            _mainWindow.MoveToReviewPage += MainWindow_MoveToReviewPage;
            DispatcherUnhandledException += App_DispatcherUnhandledException;




            _mainWindow.Show();
        }

        // not used currenlty using app.config
        private void ConfigureNLog()
        {
            // Log Levels
            // Trace - very detailed logs, which may include high - volume information such as protocol payloads.This log level is typically only enabled during development
            // Debug - debugging information, less detailed than trace, typically not enabled in production environment.
            // Info - information messages, which are normally enabled in production environment
            // Warn - warning messages, typically for non - critical issues, which can be recovered or which are temporary failures
            // Error - error messages - most of the time these are Exceptions
            // Fatal - very serious errors!

            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = $"logs/{DateTime.Now.ToShortDateString().Replace('/', '-')}.txt" };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            var consoleTarget = new ColoredConsoleTarget();

            var highlightRule = new ConsoleRowHighlightingRule();
            highlightRule.Condition = ConditionParser.ParseExpression("level == LogLevel.Error");
            highlightRule.ForegroundColor = ConsoleOutputColor.Red;
            consoleTarget.RowHighlightingRules.Add(highlightRule);

            config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget);

            NLog.LogManager.Configuration = config;
        }


        private void MainWindow_MoveToReviewPage(object sender, EventArgs e)
        {
            _mainWindow.Visibility = Visibility.Collapsed;
            if (_reviewWindow == null || !_reviewWindow.IsActive)
            {
                _reviewWindow = _serviceProvider.GetService<ReviewWindow>();
                _reviewWindow.MoveToMainPage += ReviewWindow_MoveToMainPage;
            }
            _reviewWindow.SetImagesToReview();
            _reviewWindow.Show();
        }

        private void ReviewWindow_MoveToMainPage(object sender, EventArgs e)
        {
            _reviewWindow.Close();
            if (_reviewWindow == null || !_reviewWindow.IsActive)
            {
                _mainWindow.ResetMainWindow();
                _mainWindow.Show();
            }
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
