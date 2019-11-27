using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModelInterfaces;

namespace Image_text_extractor
{
    public partial class ReviewWindow : Window
    {
        public event EventHandler MoveToMainPage;

        private IReviewWindowVM _viewModel;

        public ReviewWindow(IReviewWindowVM viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            // this.locationComboBox.ItemsSource = _viewModel.GetLocations();
            DataContext = _viewModel;
        }

        public void SetImagesToReview(List<FieldData> images)
        {
            _viewModel.SetImagesToReview(images);
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Windows.Application.Current.Shutdown();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
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
    }
}
