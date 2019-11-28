using DataStructures;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using ViewModelInterfaces;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Diagnostics;

namespace Image_text_extractor
{
    public partial class ReviewWindow : Window
    {
        public event EventHandler MoveToMainPage;

        private IReviewWindowVM _viewModel;

        private bool isBack;

        public ReviewWindow(IReviewWindowVM viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            isBack = false;
        }

        public void SetImagesToReview(List<FieldData> images)
        {
            _viewModel.SetImagesToReview(images);
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            isBack = true;
            MoveToMainPage?.Invoke(this, new EventArgs());
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            _viewModel.NextImage();
        }

        private void PreviousClick(object sender, RoutedEventArgs e)
        {
            _viewModel.PreviousImage();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Are you sure you want to close the form?" +
                "\n\nNote: All changes will be saved.", "Confirm",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (isBack == false)
                {
                    Application.Current.Shutdown();
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
