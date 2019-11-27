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

        private void LoadDataClick(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();

            _viewModel.FileLocation = dialog.SelectedPath;

            Trace.WriteLine("Printing...");
            Trace.WriteLine(_viewModel.FileLocation);

            int countData = _viewModel.LoadData();

            if (countData != 0)  //Make this 0
            {
                System.Windows.MessageBox.Show("No database found", "Alert",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                string display = "Successfully loaded " + countData.ToString() + " records";
                System.Windows.MessageBox.Show(display, "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
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

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
    }
}
