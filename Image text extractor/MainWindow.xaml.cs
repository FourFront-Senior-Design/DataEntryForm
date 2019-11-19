using System;
using System.Windows;
using ViewModelInterfaces;
using System.Diagnostics;

namespace Image_text_extractor
{
    public partial class MainWindow : Window
    {
        public event EventHandler MoveToResultsPage;

        IMainWindowVM _viewModel;

        public MainWindow(IMainWindowVM viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void LoadImagesClick(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();

            _viewModel.FileLocation = dialog.SelectedPath;

            Trace.WriteLine("Printing...");
            Trace.WriteLine(_viewModel.FileLocation);

            if (_viewModel.LoadImages() == false)  //Make this false 
            {
                System.Windows.MessageBox.Show("No images found", "Alert",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                System.Windows.MessageBox.Show("Successful", "Alert",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void ExtractClick(object sender, RoutedEventArgs e)
        {
            if (_viewModel.ProcessImages())
            {
                MoveToResultsPage?.Invoke(this, new EventArgs());
            }
            else
            {
                System.Windows.MessageBox.Show("Unable to extract images. Try again.", "Alert",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
