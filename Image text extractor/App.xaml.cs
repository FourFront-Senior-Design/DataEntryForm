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
        private ResultsWindow _resultWindow;
        private ReviewWindow _reviewWindow;
        private IServiceProvider _serviceProvider;


        public App()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IImageProcessor, ImageProcessor>();
            serviceCollection.AddSingleton<IImageLoader, ImageLoader>();

            serviceCollection.AddTransient<ITextClassifier, TextClassifier>();
            serviceCollection.AddTransient<ITextExtractor, GoogleVision>();
            serviceCollection.AddTransient<IReviewWindowVM, ReviewWindowVM>();
            serviceCollection.AddTransient<IResultWindowVM, ResultWindowVM>();
            serviceCollection.AddTransient<IMainWindowVM, MainWindowVM>();
            serviceCollection.AddTransient<IDatabaseService, FileDatabase>();

            serviceCollection.AddTransient<MainWindow, MainWindow>();
            serviceCollection.AddTransient<ResultsWindow, ResultsWindow>();
            serviceCollection.AddTransient<ReviewWindow, ReviewWindow>();


            _serviceProvider = serviceCollection.BuildServiceProvider();

            _mainWindow = _serviceProvider.GetService<MainWindow>();
            _resultWindow = _serviceProvider.GetService<ResultsWindow>();
            _reviewWindow = _serviceProvider.GetService<ReviewWindow>();

            _mainWindow.MoveToResultsPage += MainWindow_MoveToResultsPage;
            _resultWindow.MoveToMainWindow += ResultWindow_MoveToMainPage;
            _reviewWindow.MoveToResultsPage += ReviewWindow_MoveToResultsPage;
      
            _resultWindow.ReviewHighConfImages += ResultWindow_ReviewHighConfImages;
            _resultWindow.ReviewLowConfImages += ResultWindow_ReviewLowConfImages;
            _mainWindow.Show();
        }

        private void MainWindow_MoveToResultsPage(object sender, EventArgs e)
        {
            _mainWindow.Visibility = Visibility.Collapsed;
            _resultWindow.SetResultsPageData();
            _resultWindow.Show();
        }

        private void ResultWindow_MoveToMainPage(object sender, EventArgs e)
        {
            _resultWindow.Visibility = Visibility.Collapsed;
            _mainWindow.Show();
        }

        private void ReviewWindow_MoveToResultsPage(object sender, EventArgs e)
        {
            _reviewWindow.Visibility = Visibility.Collapsed;
            _resultWindow.Show();
        }

        private void ResultWindow_ReviewHighConfImages(object sender, EventArgs e)
        {
            IImageProcessor processor = _serviceProvider.GetService<IImageProcessor>();
            Trace.WriteLine("Review High Conf Images");
            Trace.WriteLine(processor.GetHighConfImages().Count.ToString());
            _reviewWindow.SetImagesToReview(processor.GetHighConfImages());
            _resultWindow.Visibility = Visibility.Collapsed;
            _reviewWindow.Show();
        }

        private void ResultWindow_ReviewLowConfImages(object sender, EventArgs e)
        {
            IImageProcessor processor = _serviceProvider.GetService<IImageProcessor>();
            Trace.WriteLine("Review Low Conf Images");
            Trace.WriteLine(processor.GetLowConfImages().Count.ToString());
            _reviewWindow.SetImagesToReview(processor.GetLowConfImages());
            _resultWindow.Visibility = Visibility.Collapsed;
            _reviewWindow.Show();
        }
    }
}
