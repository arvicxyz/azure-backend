using System;
using System.Linq;
using AzureBackend.Shared;
using UIKit;

namespace AzureBackend.iOS
{
    public partial class LoginViewController : UIViewController
    {
        public LoginViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            LoginButton.TouchUpInside += async (sender, e) =>
            {
                Console.WriteLine("Login button clicked!");

                string username = UsernameInput.Text;
                string password = PasswordInput.Text;

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    AzureConnection connection = new AzureConnection();
                    var users = await connection.Client.Table<User>().GetItemsAsync();
                    try
                    {
                        var user = users.SingleOrDefault(x => x.Username == username
                                                         && x.Password == password);
                        if (user != null)
                        {
                            Console.WriteLine("Login success!");

                            // Navigate to Home View
                            var homeViewController = Storyboard.InstantiateViewController(
                                "HomeViewController") as HomeViewController;
                            if (homeViewController != null)
                            {
                                homeViewController.LoggedUser = user;
                                NavigationController.PushViewController(homeViewController, true);
                            }
                        }
                        else
                        {
                            LoginFailed("Your Username or Password is incorrect!");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        LoginFailed("Server error! Please contact your administrator!");
                    }
                }
                else
                {
                    LoginFailed("Fill Username and Password field to proceed!");
                }
            };

            RegisterButton.TouchUpInside += (sender, e) =>
            {
                Console.WriteLine("Register button clicked!");

                // Navigate to Register View
                var registerViewController = Storyboard.InstantiateViewController(
                    "RegisterViewController") as RegisterViewController;
                if (registerViewController != null)
                {
                    NavigationController.PushViewController(registerViewController, true);
                }
            };
        }

        private void LoginFailed(string message)
        {
            Console.WriteLine("Login failed!");
            UIAlertView errorAlert = new UIAlertView()
            {
                Title = "Login Failed!",
                Message = message
            };
            errorAlert.AddButton("Ok");
            errorAlert.Show();
        }
    }
}
