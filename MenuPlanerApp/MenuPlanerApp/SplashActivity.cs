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
            //Display Splash Screen for 2 Sec  
            Thread.Sleep(2000);
            //Start Activity1 Activity  
            StartActivity(typeof(IngredientsActivity));
        }
    }
}