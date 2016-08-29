using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace AzureBackend.Droid
{
    [Activity(
        Label = "Azure Backend",
        MainLauncher = true,
        Icon = "@mipmap/icon",
        Theme = "@android:style/Theme.NoTitleBar"
    )]
    public class SplashActivity : Activity
    {
        public const int ProgressDuration = 1500;
        public const int TickTime = 100;
        public const int LoadingPause = 500;

        public ProgressBar progressBar;
        public static bool isBackPressed;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Splash);

            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            progressBar.Visibility = ViewStates.Visible;
            progressBar.Progress = 0;

            new SplashTimer(this, progressBar, ProgressDuration, TickTime).Start();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            isBackPressed = true;
        }

        public class SplashTimer : CountDownTimer
        {
            public Activity activity;
            public ProgressBar progressBar;
            public int progressStatus = 0;
            public float timePassed = 0;

            public SplashTimer(Activity activity, ProgressBar progressBar,
                               long millisInFuture, long countDownInterval)
                : base(millisInFuture, countDownInterval)
            {
                this.activity = activity;
                this.progressBar = progressBar;
            }

            public override void OnTick(long millisUntilFinished)
            {
                timePassed += TickTime;
                progressStatus = (int)((timePassed / millisUntilFinished) * 100);
                progressBar.Progress = progressStatus;
            }

            public override void OnFinish()
            {
                if (!isBackPressed)
                {
                    progressBar.Progress = progressBar.Max;
                    var runnable = new Runnable(delegate
                    {
                        activity.Finish();

                        // Proceed to Login screen
                        Intent intent = new Intent(activity, typeof(LoginActivity));
                        activity.StartActivity(intent);
                        activity.OverridePendingTransition(
                            Android.Resource.Animation.FadeIn,
                            Android.Resource.Animation.FadeOut);
                    });
                    new Handler().PostDelayed(runnable, LoadingPause);
                }
            }
        }
    }
}
