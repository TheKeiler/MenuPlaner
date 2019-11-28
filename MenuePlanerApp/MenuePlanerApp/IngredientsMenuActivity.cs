using Android;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;

namespace MenuPlanerApp
{
    [Activity(Label = "IngredientsMenuActivity", MainLauncher = false)]
    public class IngredientsMenuActivity : Activity
    {
        private RecyclerView _ingredientRecyclerView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ingredients_Menu);
            _ingredientRecyclerView = FindViewById<RecyclerView>(Resource.Id.IngredientsMenuRecyclerView);
            // Pie adapter

            // Layout Manager
            // view holder
        }
    }
}