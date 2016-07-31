using System;
using UIKit;

namespace AzureBackend.iOS
{
    public partial class HomeViewController : UIViewController
    {
        public User LoggedUser { get; set; }

        public HomeViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.Title = "Home";
            NavigationItem.HidesBackButton = true;

            if (LoggedUser != null)
            {
                WelcomeText.Text = string.Format(
                "Welcome, {0} who works as {1}! This is a simple Login/Register " +
                "project example using Xamarin and with Azure as its backend " +
                "technology. Xamarin rocks! Happy coding!", LoggedUser.Fullname, LoggedUser.Occupation);
            }

            LogoutButton.TouchUpInside += (sender, e) =>
            {
                Console.WriteLine("Logout button clicked!");
                UIAlertView warningAlert = new UIAlertView()
                {
                    Title = "Logout?",
                    Message = string.Format("Are you sure you want to logout user, \"{0}\"?", LoggedUser.Username)
                };
                warningAlert.AddButton("Yes");
                warningAlert.AddButton("No");
                warningAlert.Clicked += (senderAlert, eAlert) =>
                {
                    if (eAlert.ButtonIndex == 0)
                    {
                        // Back to Login View
                        NavigationController.PopViewController(true);
                    }
                };
                warningAlert.Show();
            };
        }
    }
}
