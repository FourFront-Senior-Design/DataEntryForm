using System;
using System.Windows;
using ViewModelInterfaces;
using System.Diagnostics;

namespace Data_Entry_Form
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

            ResetMainWindow();
        }

        public void ResetMainWindow()
        {
            _viewModel.ResetWindow();
            _viewModel.SetFilePath(Properties.Settings.Default.databaseFilePath);

            sectionPath.Focus();
            sectionPath.Select(_viewModel.FileLocation.Length, 0);

            Trace.WriteLine(_viewModel.FileLocation);
        }

        private void BrowseClick(object sender, RoutedEventArgs e)
        {   
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            string selectedPath = dialog.SelectedPath;

            _viewModel.SetFilePath(selectedPath);
            
            sectionPath.Focus();
            sectionPath.Select(_viewModel.FileLocation.Length, 0);
        }

        private void LoadDataClick(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine(_viewModel.FileLocation);

            if (_viewModel.LoadData())
            {
                Properties.Settings.Default.databaseFilePath = _viewModel.FileLocation;
                Properties.Settings.Default.Save();
            }
            
            sectionPath.Focus();
            sectionPath.Select(_viewModel.FileLocation.Length, 0);
        }

        private void ReviewClick(object sender, RoutedEventArgs e)
        {
            MoveToReviewPage?.Invoke(this, new EventArgs());
        }

        private void OnTextChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.ResetWindow();
        }
        
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }
    }
}
