using Android.App;
using Android.OS;

namespace AzureBackend.Droid
{
    [Activity(
        Label = "Login"
    )]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
        }
    }
}
