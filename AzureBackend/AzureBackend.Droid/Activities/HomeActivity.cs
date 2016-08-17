using Android.App;
using Android.OS;

namespace AzureBackend.Droid
{
    [Activity(
        Label = "Home"
    )]
    public class HomeActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);
        }
    }
}
