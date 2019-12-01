using Android.App;
using Android.Content.PM;
using Android.OS;

namespace MenuPlanerApp
{
    [Activity(Label = "LoginActivity", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
    }
}