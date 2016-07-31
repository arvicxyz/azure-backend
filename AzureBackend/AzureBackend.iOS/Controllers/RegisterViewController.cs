using System;
using UIKit;

namespace AzureBackend.iOS
{
    public partial class RegisterViewController : UIViewController
    {
        public RegisterViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.Title = "Register";

            SubmitButton.TouchUpInside += async (sender, e) =>
            {
                Console.WriteLine("Submit button clicked!");

                string username = UsernameInput.Text;
                string password = PasswordInput.Text;
                string confPassword = ConfPasswordInput.Text;
                string fullname = FullnameInput.Text;
                string occupation = OccupationInput.Text;

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)
                    && !string.IsNullOrEmpty(confPassword) && !string.IsNullOrEmpty(fullname)
                    && !string.IsNullOrEmpty(occupation))
                {
                    if (password.Equals(confPassword))
                    {
                        try
                        {
                            User user = new User();
                            user.Username = username;
                            user.Password = password;
                            user.Fullname = fullname;
                            user.Occupation = occupation;

                            AzureConnection connection = new AzureConnection();
                            await connection.Client.Table<User>().AddAsync(user);

                            Console.WriteLine("Registration success!");
                            UIAlertView errorAlert = new UIAlertView()
                            {
                                Title = "Registration Successful!",
                                Message = "A new user has been created!"
                            };
                            errorAlert.AddButton("Back to Login");
                            errorAlert.Clicked += (senderAlert, eAlert) =>
                            {
                                if (eAlert.ButtonIndex == 0)
                                {
                                    // Back to Login View
                                    NavigationController.PopViewController(true);
                                }
                            };
                            errorAlert.Show();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            RegisterFailed("Server error! Please contact your administrator!");
                        }
                    }
                    else
                    {
                        RegisterFailed("Password and Confirm Password do not match!");
                    }
                }
                else
                {
                    RegisterFailed("All fields are required. Please fill everything.");
                }
            };
        }

        private void RegisterFailed(string message)
        {
            Console.WriteLine("Registration failed!");
            UIAlertView errorAlert = new UIAlertView()
            {
                Title = "Registration Failed!",
                Message = message
            };
            errorAlert.AddButton("Ok");
            errorAlert.Show();
        }
    }
}
