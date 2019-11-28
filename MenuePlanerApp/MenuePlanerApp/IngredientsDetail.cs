using Android;
using Android.App;
using Android.OS;
using Android.Widget;
using MenuPlanerApp.Core.Model;
using MenuPlanerApp.Core.Repository;

namespace MenuPlanerApp
{
    [Activity(Label = "IngredientsDetail", MainLauncher = true)]
    public class IngredientsDetail : Activity
    {
        private ImageView _ingredientsImageView;
        private IngredientsRepository _ingredientsRepository;
        private Ingredient _selectedIngredient;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ingredient_detail);
            // Create your application here
            _ingredientsRepository = new IngredientsRepository();
            _selectedIngredient = _ingredientsRepository.GetIngredientById(1);
            FindViews();
            BindData();
        }

        private void BindData()
        {
        }

        private void FindViews()
        {
            _ingredientsImageView = FindViewById<ImageView>(Resource.Id.ingredientsImageView);
        }
    }
}