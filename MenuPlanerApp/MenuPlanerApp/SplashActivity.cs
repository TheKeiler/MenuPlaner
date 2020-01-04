using System.Threading;
using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace MenuPlanerApp
{
    [Activity(Label = "SplashActivity", Theme = "@style/AppTheme.Splash", MainLauncher = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Thread.Sleep(2000);
            StartActivity(typeof(RecipeActivity));
        }
    }
}