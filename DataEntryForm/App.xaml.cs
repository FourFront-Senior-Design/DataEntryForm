using Microsoft.Extensions.DependencyInjection;
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
            MessageBox.Show(e.Exception.Message);
            e.Handled = true;
        }
    }
}
