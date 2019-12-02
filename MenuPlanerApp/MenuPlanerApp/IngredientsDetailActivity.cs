using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private IngredientsRepositoryWeb _ingredientsRepository;
        private TextView _ingredientsTextView;
        private Ingredient _selectedIngredient;
        private List<Ingredient> _ingredientsList;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ingredient_detail);
            // Create your application here
            _ingredientsRepository = new IngredientsRepositoryWeb();
            LoadData();
            SetFirstElementInRepoAsSelectedIngredient();
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

        public async Task LoadData()
        {
            _ingredientsList = await _ingredientsRepository.GetAllIngredients();
        }

        public async Task LoadDataById(int id)
        {
            _selectedIngredient = await _ingredientsRepository.GetIngredientById(id);
        }

        private async void SetFirstElementInRepoAsSelectedIngredient()
        {
            if (this.Intent.Extras == null)
            {
                _selectedIngredient = _ingredientsList.First();
            }
            else
            {
                var selectedId = Intent.Extras.GetInt("selectedIngredientId");
                _selectedIngredient = await _ingredientsRepository.GetIngredientById(selectedId);
            }
        }
    }
}