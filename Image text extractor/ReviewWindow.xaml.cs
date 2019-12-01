using DataStructures;
using System;
using System.Windows;
using System.Windows.Input;
using ViewModelInterfaces;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Controls;
using System.Reflection;

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
            AddHandler(KeyDownEvent, new KeyEventHandler((ss, ee) =>
            {
                if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.PageUp))
                {
                    _viewModel.PreviousRecordMacro.Execute(null);
                }

                if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.PageDown))
                {
                    _viewModel.NextRecordMacro.Execute(null);
                }
                
            }), true);
            isBack = false;
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

        private void GoToRecordClick(object sender, RoutedEventArgs e)
        {
            _viewModel.PageIndex = System.Convert.ToInt32(GoToRecordTextBox.Text);
            GoToRecordTextBox.Text = "";
        }

        private void FirstRecordClick(object sender, RoutedEventArgs e)
        {
            _viewModel.PageIndex = 1;
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            isBack = true;
            MoveToMainPage?.Invoke(this, new EventArgs());
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            _viewModel.NextRecord();
        }

        private void PreviousClick(object sender, RoutedEventArgs e)
        {
            _viewModel.PreviousRecord();
        }

        public void SetImagesToReview()
        {
            _viewModel.SetRecordsToReview();
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
                MessageBox.Show("Invalid text");
            }
        }

        private void EmblemCombox_LostFocus(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            if (string.IsNullOrEmpty(cb.Text))
            {
                return;
            }

            int input = Convert.ToInt32(cb.Text);

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
            MessageBox.Show("Invalid text");
        }

    }
}
