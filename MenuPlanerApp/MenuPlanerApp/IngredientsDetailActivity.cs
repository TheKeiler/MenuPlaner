using Android.App;
using Android.OS;
using Android.Widget;
using MenuPlanerApp.Core.Model;
using MenuPlanerApp.Core.Repository;

namespace MenuPlanerApp
{
    [Activity(Label = "IngredientsDetail")]
    public class IngredientsDetailActivity : Activity
    {
        private ImageView _ingredientsImageView;
        private TextView _ingredientsTextView;
        private IngredientsRepository _ingredientsRepository;
        private Ingredient _selectedIngredient;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ingredient_detail);
            // Create your application here
            _ingredientsRepository = new IngredientsRepository();
            var selectedIngredientId = Intent.Extras.GetInt("selectedIngredientId");
            _selectedIngredient = _ingredientsRepository.GetIngredientById(selectedIngredientId);
            FindViews();
            BindData();
        }

        private void FindViews()
        {
            _ingredientsImageView = FindViewById<ImageView>(Resource.Id.ingredientsImageView);
            _ingredientsTextView = FindViewById<TextView>(Resource.Id.ingredientsText);
        }

        private void BindData()
        {
            _ingredientsTextView.Text = _selectedIngredient.Name;
        }
    }
}