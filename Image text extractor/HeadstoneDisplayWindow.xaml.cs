using Common;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using ViewModelInterfaces;

namespace Image_text_extractor
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
            string sectionFilePath = _viewModel.SectionFilePath;
            
            string image1FullFilePath = sectionFilePath + "\\" + Constants.REFERENCED_IMAGE_FOLDER_NAME + "\\" + _viewModel.CurrentPageData.Image1FileName;
            string image2FullFilePath;


            frontFaceImage.Source = new BitmapImage(new Uri(image1FullFilePath));

            if(!string.IsNullOrWhiteSpace(_viewModel.CurrentPageData.Image2FileName))
            {
                image2FullFilePath = sectionFilePath + "\\" + Constants.REFERENCED_IMAGE_FOLDER_NAME + "\\" + _viewModel.CurrentPageData.Image2FileName;
                backFaceImage.Source = new BitmapImage(new Uri(image2FullFilePath));
            }

            this.Show();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);

            this.Hide();
        }

    }
}
