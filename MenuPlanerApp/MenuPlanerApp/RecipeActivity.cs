using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using MenuPlanerApp.Core.Model;
using MenuPlanerApp.Core.Repository;
using MenuPlanerApp.Core.VerifyData;
using Xamarin.Essentials;

namespace MenuPlanerApp
{
    [Activity(Label = "@string/app_name")]
    public class RecipeActivity : AppCompatActivity
    {
        private Button _abortButton;
        private Button _deleteButton;
        private Button _ingredientButton;
        private TextInputEditText _recipeNameEditText;
        private List<IngredientWithAmount> _ingredientsList;
        private IngredientsRepositoryWeb _ingredientsRepository;
        private Button _menusButton;
        private Button _newButton;
        private Button _optionsButton;
        private Button _recipeButton;
        private Button _saveButton;
        private Button _searchButton;
        private Ingredient _selectedMenu;
        private VerifyUserEntries _verifyUserEntries;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.recipes);
        }
    }
}