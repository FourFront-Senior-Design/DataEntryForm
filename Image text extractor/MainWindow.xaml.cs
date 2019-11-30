using System;
using System.Windows;
using ViewModelInterfaces;
using System.Diagnostics;

namespace Image_text_extractor
{
    public partial class MainWindow : Window
    {
        public event EventHandler MoveToReviewPage;

        IMainWindowVM _viewModel;

        public MainWindow(IMainWindowVM viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        public void ResetMainWindow()
        {
            _viewModel.EnableExtract = false;
            _viewModel.FileLocation = "";
        }

        private void LoadDataClick(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();

             _viewModel.FileLocation = dialog.SelectedPath;

            Trace.WriteLine("Printing...");
            Trace.WriteLine(_viewModel.FileLocation);

            int countData = _viewModel.LoadData();

            if(countData == -1)
            {
                System.Windows.MessageBox.Show("Invalid Data Path. Try Again", "Alert",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (countData == 0)  
            {
                System.Windows.MessageBox.Show("No database found", "Alert",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string display = "Successfully loaded " + countData.ToString() + " records";
                System.Windows.MessageBox.Show(display, "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                _viewModel.EnableExtract = true;
            }
        }

        private void ReviewClick(object sender, RoutedEventArgs e)
        {
            MoveToReviewPage?.Invoke(this, new EventArgs());
        }


        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }
    }
}
