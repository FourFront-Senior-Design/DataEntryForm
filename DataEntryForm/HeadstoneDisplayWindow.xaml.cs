using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
using ViewModelInterfaces;

namespace Data_Entry_Form
{
    /// <summary>
    /// Interaction logic for HeadstoneDisplayWindow.xaml
    /// </summary>
    public partial class HeadstoneDisplayWindow : Window
    {
        IReviewWindowVM _viewModel;


        public HeadstoneDisplayWindow(IReviewWindowVM viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _viewModel.HeadstoneChanged += viewModel_HeadstoneChanged;
        }

        private void viewModel_HeadstoneChanged(object sender, EventArgs e)
        {
            frontFaceImage.Source = new BitmapImage(new Uri(_viewModel.ImageSource1));

            if (!string.IsNullOrWhiteSpace(_viewModel.CurrentPageData.Image2FileName))
            {
                backFaceImage.Source = new BitmapImage(new Uri(_viewModel.ImageSource2));
            }
            else
            {
                backFaceImage.Source = null;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            this.Hide();
        }

    }
}
