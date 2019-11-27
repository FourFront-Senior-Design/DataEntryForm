using Microsoft.Extensions.DependencyInjection;
using Services;
using ServicesInterfaces;
using System;
using System.Diagnostics;
using System.Windows;
using ViewModelInterfaces;
using ViewModels;

namespace Image_text_extractor
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
<<<<<<< HEAD
            serviceCollection.AddTransient<IDatabase, MicrosoftAccess>();
=======
            serviceCollection.AddTransient<IDatabaseService, FileDatabase>();
>>>>>>> IDatabaseService

            serviceCollection.AddTransient<MainWindow, MainWindow>();
            serviceCollection.AddTransient<ReviewWindow, ReviewWindow>();

            _serviceProvider = serviceCollection.BuildServiceProvider();

            _mainWindow = _serviceProvider.GetService<MainWindow>();
            _reviewWindow = _serviceProvider.GetService<ReviewWindow>();

            _mainWindow.MoveToReviewPage += MainWindow_MoveToReviewPage;
            _reviewWindow.MoveToMainPage += ReviewWindow_MoveToMainPage;
            _mainWindow.Show();
        }

        private void MainWindow_MoveToReviewPage(object sender, EventArgs e)
        {
            _mainWindow.Visibility = Visibility.Collapsed;
            _reviewWindow.Show();
        }

        private void ReviewWindow_MoveToMainPage(object sender, EventArgs e)
        {
            _reviewWindow.Visibility = Visibility.Collapsed;
            _mainWindow.Show();
        }
    }
}
