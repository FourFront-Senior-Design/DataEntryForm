﻿using System;
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
            _viewModel.EnableExtract = false;
            _viewModel.Message = "";
            _viewModel.FileLocation = Properties.Settings.Default.databaseFilePath;
            sectionPath.Focus();
            sectionPath.Select(_viewModel.FileLocation.Length, 0);
            Trace.WriteLine(_viewModel.FileLocation);
            Trace.WriteLine(_viewModel.FileLocation.Length);
        }

        private void BrowseClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Message = "";
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();

            string selectedPath = dialog.SelectedPath;
            if(selectedPath != string.Empty)
            {
                _viewModel.FileLocation = selectedPath;
            }
            sectionPath.Focus();
            sectionPath.Select(_viewModel.FileLocation.Length, 0);
            Trace.WriteLine(sectionPath.CaretIndex);
        }

        private void LoadDataClick(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("Printing...");
            Trace.WriteLine(_viewModel.FileLocation);

            int countData = _viewModel.LoadData();

            if(countData == -1)
            {
                _viewModel.Message = "Invalid Path. Try Again.";
            }
            else if (countData == 0)  
            {
                _viewModel.Message = "No database found. Try Again.";
            }
            else
            {
                Properties.Settings.Default.databaseFilePath = _viewModel.FileLocation;
                Properties.Settings.Default.Save();
                _viewModel.Message = "Successfully uploaded " + countData.ToString() +
                                 " records from the Database";
                _viewModel.EnableExtract = true;
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
            _viewModel.EnableExtract = false;
            _viewModel.Message = "";
        }
        
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }
    }
}
