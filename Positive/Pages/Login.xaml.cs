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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Positive.DataModels;

namespace Positive.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        // Data model to access the UserAccount table.
        private UserAccounts userAccounts;

        public Login()
        {
            InitializeComponent();

            // Create a new UserAccounts object to access the database.
            userAccounts = new UserAccounts();
        }

        private void LoginLoaded(object sender, RoutedEventArgs e)
        {
            // First focus on the ID textbox.
            IDTextbox.Focus();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            int? ID = null;
            int? PINNumber = null;

            // Get the ID and PIN number from the textboxes.
            if (IDTextbox.Text != string.Empty)
                ID = Convert.ToInt32(IDTextbox.Text);
            if (PINNumberTextbox.Password != string.Empty)
                PINNumber = Convert.ToInt32(PINNumberTextbox.Password);

            UserAccount userAccount = userAccounts.GetUserAccountByID(ID);

            // Check is the ID exists.
            if (userAccount != null)
            {
                // Check the PIN number matches the account's PIN.
                if (userAccount.PINNumber == PINNumber)
                {
                    WarningTextBlock.Visibility = Visibility.Hidden;
                    System.Windows.Navigation.NavigationService test = NavigationService;
                    test.Navigate(new MainMenu());
                }
                else
                {
                    WarningTextBlock.Visibility = Visibility.Visible;
                }
            }
            else
            {
                WarningTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void FormTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox s = e.Source as TextBox;
                if (s != null)
                {
                    s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }

                e.Handled = true;
            }
        }

        private void IDTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PINNumberTextbox.Focus();
                e.Handled = true;
            }
        }

        private void PINNumberTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                e.Handled = true;
            }
        }
    }
}
