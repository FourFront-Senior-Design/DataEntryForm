using System;
using System.Collections.Generic;
using System.Windows;
using ViewModelInterfaces;
using DataStructures;

namespace Image_text_extractor
{
    public partial class ResultsWindow : Window
    {
        public event EventHandler MoveToMainWindow;
        public event EventHandler ReviewHighConfImages;
        public event EventHandler ReviewLowConfImages;

        private IResultWindowVM _viewModel;

        public ResultsWindow(IResultWindowVM viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        public void SetResultsPageData()
        {
            _viewModel.SetResultsPageData();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            MoveToMainWindow?.Invoke(this, new EventArgs());
        }

        private void ReviewHighClick(object sender, RoutedEventArgs e)
        {
            ReviewHighConfImages?.Invoke(this, new EventArgs());
        }

        private void ReviewLowClick(object sender, RoutedEventArgs e)
        {
            ReviewLowConfImages?.Invoke(this, new EventArgs());
        }

        private void SaveToDatabaseClick(object sender, RoutedEventArgs e)
        {
            List<FieldData> highConfImagesData = _viewModel.GetHighConfImages();
            string message, type;
            if (_viewModel.SaveToDatabase(highConfImagesData))
            {
                message = "Successfully inserted high confidence images in database";
                type = "Success";
            }
            else
            {
                message = "Failed to insert data in the database";
                type = "Alert";
            }
            System.Windows.MessageBox.Show(message, type,
                    MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
