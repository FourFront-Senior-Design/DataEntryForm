using DataStructures;
using System;
using System.Windows;
using System.Windows.Input;
using ViewModelInterfaces;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Controls;
using System.Reflection;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace Image_text_extractor
{
    public partial class ReviewWindow : Window
    {
        public event EventHandler MoveToMainPage;


        private IReviewWindowVM _viewModel;
        private HeadstoneDisplayWindow _displayWindow;
        private bool isBack;

        public ReviewWindow(IReviewWindowVM viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;

            _displayWindow = new HeadstoneDisplayWindow(_viewModel);

            _viewModel.HeadstoneChanged += viewModel_HeadstoneChanged;

            AddHandler(KeyDownEvent, new KeyEventHandler((ss, ee) =>
            {
                if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.PageUp))
                {
                    if (!_validateMandatoryInfoExists())
                    {
                        return;
                    }
                    _viewModel.PreviousRecord();
                    BurialSectionField.Focus();
                }

                if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.PageDown))
                {
                    if (!_validateMandatoryInfoExists())
                    {
                        return;
                    }
                    _viewModel.NextRecord();
                    BurialSectionField.Focus();
                }
                
            }), true);
            isBack = false;
        }

        private void viewModel_HeadstoneChanged(object sender, EventArgs e)
        {
            frontFaceImage.Source = new BitmapImage(new Uri(_viewModel.ImageSource1));

            if (!string.IsNullOrWhiteSpace(_viewModel.CurrentPageData.Image2FileName))
            {
                backFaceImage.Source = new BitmapImage(new Uri(_viewModel.ImageSource2));
            }
            BurialSectionField.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }

                e.Handled = true;
            }
        }

        private bool _validateMandatoryInfoExists()
        {
            bool missing = false;
            List<bool> filledInformation = _viewModel.CheckMandatoryFields();
            List<TextBox> mandatoryField = new List<TextBox>() {
                gravesiteNum, primaryLastName, secondaryLastName,
                name3LastName, name4LastName, name5LastName, name6LastName,
                name7LastName };

            for(int i = 0; i < mandatoryField.Count; i++)
            {
                if (filledInformation[i] == false)
                {
                    mandatoryField[i].BorderBrush = System.Windows.Media.Brushes.Red;
                    missing = true;
                }
                else
                {
                    mandatoryField[i].ClearValue(Border.BorderBrushProperty);
                }
            }
            
            if (missing == true)
            {
                MessageBox.Show("Enter Mandatory Information", "Error", MessageBoxButton.OK);
                return false;
            }
            return true;
        }

        private void GoToRecordClick(object sender, RoutedEventArgs e)
        {
            string input = goToRecordTb.Text;
            goToRecordTb.Text = "";

            if (!_validateMandatoryInfoExists())
            {
                return;
            }

            if(!_viewModel.GoToRecord(input))
            {
                MessageBox.Show("Invalid Record Number", "Error", MessageBoxButton.OK);
                return;
            }
        }

        private void FirstRecordClick(object sender, RoutedEventArgs e)
        {
            if (!_validateMandatoryInfoExists())
            {
                return;
            }
            _viewModel.FirstRecord();
        }

        private void LastRecordClick(object sender, RoutedEventArgs e)
        {
            if (!_validateMandatoryInfoExists())
            {
                return;
            }
            _viewModel.LastRecord();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            if (!_validateMandatoryInfoExists())
            {
                return;
            }
            isBack = true;
            MoveToMainPage?.Invoke(this, new EventArgs());
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            if (!_validateMandatoryInfoExists())
            {
                return;
            }
            _viewModel.NextRecord();
        }

        private void PreviousClick(object sender, RoutedEventArgs e)
        {
            if (!_validateMandatoryInfoExists())
            {
                return;
            }
            _viewModel.PreviousRecord();
        }

        public void SetImagesToReview()
        {
            _viewModel.SetRecordsToReview();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            _viewModel.SaveRecord();

            if (System.Windows.MessageBox.Show("Are you sure you want to close the form?" +
                "\n\nNote: All changes will be saved.", "Confirm",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _displayWindow.Close();
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

        private void Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if((Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)) && Keyboard.IsKeyDown(Key.U))
            {
                System.Diagnostics.Trace.WriteLine("Detected Alt+U");
                TextBox tb = (TextBox)sender;
                tb.Text = "UNKNOWN";
            }
            
            if ((Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)) && Keyboard.IsKeyDown(Key.I))
            {
                System.Diagnostics.Trace.WriteLine("Detected Alt+I");
                TextBox tb = (TextBox)sender;
                tb.Text = "ILLEGIBLE";
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void MarkerCombox_LostFocus(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string input = cb.Text;
            
            foreach (ComboBoxItem i in cb.Items)
            {
                if (i.Content.ToString().Contains(input))
                {
                    cb.SelectedItem = i;
                    return;
                }
            }

            if (!string.IsNullOrEmpty(input))
            {
                cb.Text = "";
                MessageBox.Show("The text you have entered isn't an item in the list."+
                    "\n\nSelect an item from the list, or enter text that matches one of the listed items.",
                    "VA National Cemetery Inventory");
                cb.IsDropDownOpen = true;
            }
        }

        private void Cemetery_LostFocus(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string input = cb.Text;
            

            foreach (CemeteryNameData i in cb.Items)
            {
                if (i.CemeteryName.Equals(input))
                {
                    cb.SelectedItem = i;
                    return;
                }
            }

            if (!string.IsNullOrEmpty(input))
            {
                cb.Text = "";
                MessageBox.Show("The text you have entered isn't an item in the list." +
                    "\n\nSelect an item from the list, or enter text that matches one of the listed items.",
                    "VA National Cemetery Inventory");
                cb.IsDropDownOpen = true;
            }
        }

        private void Branch_LostFocus(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string input = cb.Text;

            foreach (BranchData i in cb.Items)
            {
                if (i.Code.Equals(input))
                {
                    cb.SelectedItem = i;
                    return;
                }
            }

            if (!string.IsNullOrEmpty(input))
            {
                cb.Text = "";
                MessageBox.Show("The text you have entered isn't an item in the list." +
                    "\n\nSelect an item from the list, or enter text that matches one of the listed items.",
                    "VA National Cemetery Inventory");
                cb.IsDropDownOpen = true;
            }
        }

        private void War_LostFocus(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string input = cb.Text;

            foreach (WarData i in cb.Items)
            {
                if (i.Code.Equals(input))
                {
                    cb.SelectedItem = i;
                    return;
                }
            }

            if (!string.IsNullOrEmpty(input))
            {
                cb.Text = "";
                MessageBox.Show("The text you have entered isn't an item in the list." +
                    "\n\nSelect an item from the list, or enter text that matches one of the listed items.",
                    "VA National Cemetery Inventory");
                cb.IsDropDownOpen = true;
            }
        }

        private void Award_LostFocus(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string input = cb.Text;

            foreach (AwardData i in cb.Items)
            {
                if (i.Code.Equals(input))
                {
                    cb.SelectedItem = i;
                    return;
                }
            }

            if (!string.IsNullOrEmpty(input))
            {
                cb.Text = "";
                MessageBox.Show("The text you have entered isn't an item in the list." +
                    "\n\nSelect an item from the list, or enter text that matches one of the listed items.",
                    "VA National Cemetery Inventory");
                cb.IsDropDownOpen = true;
            }
        }

        private void EmblemCombox_LostFocus(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            int input = 0;
            if(string.IsNullOrEmpty(cb.Text) || string.IsNullOrWhiteSpace(cb.Text))
            {
                return;
            }

            try
            {
                input = Convert.ToInt32(cb.Text);
            }
            catch
            {
                cb.Text = "";
                MessageBox.Show("The text you have entered isn't an item in the list." +
                "\n\nSelect an item from the list, or enter text that matches one of the listed items.",
                "VA National Cemetery Inventory");
                cb.IsDropDownOpen = true;
                return;
            }

            foreach (EmblemData i in cb.Items)
            {
                int currentCode = Convert.ToInt32(i.Code);
                if (currentCode == input)
                {
                    cb.SelectedItem = cb.Text;
                    cb.Text = input.ToString();
                    return;
                }
            }
            cb.Text = "";
            MessageBox.Show("The text you have entered isn't an item in the list." +
                "\n\nSelect an item from the list, or enter text that matches one of the listed items.",
                "VA National Cemetery Inventory");
            cb.IsDropDownOpen = true;
        }
        
        private void OpenImageClick(object sender, RoutedEventArgs e)
        {
            _displayWindow.Show();
        }
    }
}
