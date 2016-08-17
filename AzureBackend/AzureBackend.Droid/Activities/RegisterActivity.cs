using Android.App;
using Android.OS;

namespace AzureBackend.Droid
{
    [Activity(
        Label = "Register"
    )]
    public class RegisterActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);
        }
    }
}
