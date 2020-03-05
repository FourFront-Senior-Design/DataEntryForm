using DataStructures;
using System;
using System.Windows;
using System.Windows.Input;
using ViewModelInterfaces;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using MessageBox = System.Windows.MessageBox;
using Xceed.Wpf.Toolkit;

namespace Data_Entry_Form
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

            isBack = false;

            _displayWindow = new HeadstoneDisplayWindow(_viewModel);

            _viewModel.HeadstoneChanged += viewModel_HeadstoneChanged;

            this.PreviewKeyDown += ReviewWindow_KeyDown;
        }

        private void ReviewWindow_KeyDown(object sender, KeyEventArgs ee)
        {
            //Previous 
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.PageUp))
            {
                updateFocusField(ee);
                _viewModel.PreviousRecord();
                pageReset();
            }

            //Next
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.PageDown))
            {
                updateFocusField(ee);
                if (!_validateMandatoryInfoExists())
                {
                    return;
                }

                _viewModel.NextRecord();
                pageReset();
            }

            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.H))
            {
                Console.WriteLine("Detected Ctrl + H");
                HelpMenu.IsOpen = !HelpMenu.IsOpen;
            }

            //Skip mandatory info
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) &&
            Keyboard.IsKeyDown(Key.J) && Keyboard.IsKeyDown(Key.K) & Keyboard.IsKeyDown(Key.L))
            {
                Console.WriteLine("Detected Alt + JKL");
                _viewModel.NextRecord();
                BurialSectionField.Focus();
            }

            else if (ee.Key == Key.Enter)
            {
                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }

                ee.Handled = true;
            }
        }

        private void updateFocusField(KeyEventArgs ee)
        {
            if(ee.Source is MaskedTextBox)
            {
                MaskedTextBox mtb = (MaskedTextBox)ee.Source;
                mtb.GetBindingExpression(MaskedTextBox.ValueProperty).UpdateSource();
                mtb.Focus();
            }
            else if (ee.Source is TextBox)
            {
                TextBox tb = (TextBox)ee.Source;
                tb.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                tb.Focus();
            }
            else if (ee.Source is ComboBox)
            {
                ComboBox cb = (ComboBox)ee.Source;
                cb.GetBindingExpression(ComboBox.TextProperty).UpdateSource();
                cb.Focus();
            }
        }

        private void viewModel_HeadstoneChanged(object sender, EventArgs e)
        {
            frontFaceImage.Source = new BitmapImage(new Uri(_viewModel.ImageSource1));

            if (!string.IsNullOrWhiteSpace(_viewModel.CurrentPageData.Image2FileName))
            {
                backFaceImage.Source = new BitmapImage(new Uri(_viewModel.ImageSource2));
                frontFaceImage.SetValue(Grid.ColumnSpanProperty, 1);

            }
            else
            {
                backFaceImage.Source = null;
                frontFaceImage.SetValue(Grid.ColumnSpanProperty, 2);
            }
            BurialSectionField.Focus();
        }

        private bool _validateMandatoryInfoExists()
        {
            bool missing = false;
            List<bool> filledInformation = _viewModel.CheckMandatoryFields();
            List<Border> maskedMandatoryFields = new List<Border>()
            {
                cemeteryName, markerType, emb1Border
            };

            List<TextBox> mandatoryField = new List<TextBox>() { 
                BurialSectionField, wallID, rowNum, 
                gravesiteNum, primaryLastName, secondaryLastName,
                name3LastName, name4LastName, name5LastName, name6LastName,
                name7LastName, };

            for (int i = 0; i < maskedMandatoryFields.Count; i++)
            {
                if (filledInformation[i] == false)
                {
                    maskedMandatoryFields[i].BorderBrush = System.Windows.Media.Brushes.Red;
                    missing = true;
                    Console.WriteLine(maskedMandatoryFields[i].BorderBrush);
                }
                else
                {
                    maskedMandatoryFields[i].ClearValue(Border.BorderBrushProperty);
                }
            }

            for (int i = maskedMandatoryFields.Count; i < filledInformation.Count; i++)
            {
                int index = i - maskedMandatoryFields.Count;
                if (filledInformation[i] == false)
                {
                    mandatoryField[index].BorderBrush = System.Windows.Media.Brushes.Red;
                    missing = true;
                }
                else
                {
                    mandatoryField[index].ClearValue(Border.BorderBrushProperty);
                }
            }
            
            if (missing == true)
            {
                MessageBox.Show("Enter Mandatory Information", "Error", MessageBoxButton.OK);
                return false;
            }
            return true;
        }

        private void clearMandatoryFieldBorders()
        {
            List<Border> maskedMandatoryFields = new List<Border>()
            {
                cemeteryName, markerType, emb1Border
            };

            List<TextBox> mandatoryField = new List<TextBox>() {
                BurialSectionField, wallID, rowNum,
                gravesiteNum, primaryLastName, secondaryLastName,
                name3LastName, name4LastName, name5LastName, name6LastName,
                name7LastName, };

            for (int i = 0; i < maskedMandatoryFields.Count; i++)
            {
                maskedMandatoryFields[i].ClearValue(Border.BorderBrushProperty);
            }

            for (int i = 0; i < mandatoryField.Count; i++)
            {
                mandatoryField[i].ClearValue(Border.BorderBrushProperty);
            }
        }

        private void GoToRecordClick(object sender, RoutedEventArgs e)
        {
            string input = goToRecordTb.Text;
            goToRecordTb.Text = "";

            try
            {
                int page = Convert.ToInt32(input);

                if (page >= _viewModel.PageIndex && !_validateMandatoryInfoExists())
                {
                    return;
                }

                if(_viewModel.GoToRecord(input) == false)
                {
                    MessageBox.Show("Invalid Record Number", "Error", MessageBoxButton.OK);
                }
                pageReset();
            }
            catch
            {
                MessageBox.Show("Invalid Record Number", "Error", MessageBoxButton.OK);
                return;
            }
        }

        private void FirstRecordClick(object sender, RoutedEventArgs e)
        {
            _viewModel.FirstRecord();
            pageReset();
        }

        private void LastRecordClick(object sender, RoutedEventArgs e)
        {
            if (!_validateMandatoryInfoExists())
            {
                return;
            }

            _viewModel.LastRecord();
            pageReset();
        }

        private void ReturnToMainWindow(object sender, RoutedEventArgs e)
        {
            if (!_validateMandatoryInfoExists())
            {
                return;
            }
            isBack = true;
            _viewModel.SaveRecord();
            _viewModel.CloseDatabase();
            MoveToMainPage?.Invoke(this, new EventArgs());
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            if (!_validateMandatoryInfoExists())
            {
                return;
            }

            _viewModel.NextRecord();
            pageReset();
        }

        private void PreviousClick(object sender, RoutedEventArgs e)
        {
            _viewModel.PreviousRecord();
            pageReset();
        }

        private void pageReset()
        {
            ScrollBar.ScrollToTop();

            morePrimaryData.IsChecked = _viewModel.CurrentPageData.PrimaryDecedent.containsExtraData();
            moreSecondaryData.IsChecked = _viewModel.CurrentPageData.OthersDecedentList[0].containsExtraData();
            Name3.IsChecked = _viewModel.CurrentPageData.OthersDecedentList[1].containsData();
            Name4.IsChecked = _viewModel.CurrentPageData.OthersDecedentList[2].containsData();
            Name5.IsChecked = _viewModel.CurrentPageData.OthersDecedentList[3].containsData();
            Name6.IsChecked = _viewModel.CurrentPageData.OthersDecedentList[4].containsData();
            Name7.IsChecked = _viewModel.CurrentPageData.OthersDecedentList[5].containsData();

            clearMandatoryFieldBorders();
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            HelpMenu.IsOpen = !HelpMenu.IsOpen;
        }

        public void SetImagesToReview()
        {
            _viewModel.SetRecordsToReview();
            pageReset();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            if(isBack == false)
            {
                if (System.Windows.MessageBox.Show("Are you sure you want to close the form?" +
                "\n\nNote: Your changes are not saved. Click \"Save & Go to Menu\" to save.", "Confirm",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _displayWindow.Close();
                    _viewModel.CloseDatabase();
                    Application.Current.Shutdown();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        
        private void Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if((Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)) && Keyboard.IsKeyDown(Key.U))
            {
                System.Diagnostics.Trace.WriteLine("Detected Alt+U");
                TextBox tb = (TextBox)sender;
                int pos = tb.CaretIndex;
                tb.Text = tb.Text.Insert(pos, "UNKNOWN");
                tb.CaretIndex = pos + "UNKNOWN".Length;
            }
            
            if ((Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)) && Keyboard.IsKeyDown(Key.I))
            {
                System.Diagnostics.Trace.WriteLine("Detected Alt+I");
                TextBox tb = (TextBox)sender;
                int pos = tb.CaretIndex;
                tb.Text = tb.Text.Insert(pos, "ILLEGIBLE");
                tb.CaretIndex = pos + "ILLEGIBLE".Length;
            }

            if ((Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)) && Keyboard.IsKeyDown(Key.B))
            {
                System.Diagnostics.Trace.WriteLine("Detected Alt+B");
                primaryFirstName.Text = "BLANK";
                primaryMiddleName.Text = "BLANK";
                primaryLastName.Text = "BLANK";
                TextBox tb = (TextBox)sender;
                tb.CaretIndex = tb.Text.Length;
            }

            //if ((Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)) && Keyboard.IsKeyDown(Key.X))
            //if (e.Key == Key.X && (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)))
            //{
            //    System.Diagnostics.Trace.WriteLine("Detected ALT+X");
            //    TextBox tb = (TextBox)sender;
            //    tb.SelectAll();
            //    tb.Cut();
            //}
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

        private void EmblemCombox_TextChanged(object sender, RoutedEventArgs e)
        {
            List<int> codes = new List<int>();
            for(int i=0; i<_viewModel.GetEmblemData.Count; i++)
            {
                codes.Add(Convert.ToInt32(_viewModel.GetEmblemData[i].Code));
            }

            try
            {
                int index1 = codes.IndexOf(Convert.ToInt32(emb1.Text));

                if (index1 != -1)
                {
                    String source1 = _viewModel.GetEmblemData[index1].Photo;
                    emb1_selected.Source = new BitmapImage(new Uri(source1, UriKind.Relative));
                    Console.WriteLine(emb1_selected.Source);
                    Console.WriteLine(emb1.SelectedIndex);
                }
            }
            catch
            {
                emb1_selected.Source = new BitmapImage();
            }


            try
            {
                int index2 = codes.IndexOf(Convert.ToInt32(emb2.Text));
                if (index2 != -1)
                {
                    String source2 = _viewModel.GetEmblemData[index2].Photo;
                    emb2_selected.Source = new BitmapImage(new Uri(source2, UriKind.Relative));
                    Console.WriteLine(emb2_selected.Source);
                    Console.WriteLine(emb2.SelectedIndex);
                }
            }
            catch
            {
                emb2_selected.Source = new BitmapImage();
            }
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textbox = (e.Source as TextBox);
            if(textbox != null) textbox.SelectAll();
        }
    }
}
