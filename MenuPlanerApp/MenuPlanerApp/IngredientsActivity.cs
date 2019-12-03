using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using MenuPlanerApp.Core.Model;
using MenuPlanerApp.Core.Repository;
using Xamarin.Essentials;

namespace MenuPlanerApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class IngredientsActivity : AppCompatActivity
    {
        private Button _abortButton;
        private CheckBox _celiacCheckBox;
        private Button _deleteButton;
        private CheckBox _fructoseCheckBox;
        private CheckBox _histaminCheckBox;
        private Button _ingredientButton;
        private TextInputEditText _ingredientDescriptionEditText;
        private TextInputEditText _ingredientNameEditText;
        private List<Ingredient> _ingredientsList;

        //Data
        private IngredientsRepositoryWeb _ingredientsRepository;
        private TextInputEditText _ingredientsUnitnEditText;
        private CheckBox _lactoseCheckBox;
        private Button _menusButton;

        //Operations
        private Button _newButton;

        //Navigation
        private Button _optionsButton;
        private Button _recipeButton;
        private Button _saveButton;

        //Ingredientsdetails
        private Button _searchButton;
        private Ingredient _selectedIngredient;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ingredients);
            _ingredientsRepository = new IngredientsRepositoryWeb();
            _selectedIngredient = new Ingredient();
            await LoadData();
            SetFirstElementInRepoAsSelectedIngredient();
            FindViews();
            BindData();
            LinkEventHandlers();
        }

        private void SetFirstElementInRepoAsSelectedIngredient()
        {
            if (Intent.Extras == null)
            {
                _selectedIngredient = _ingredientsList.First();
            }
            else
            {
                var selectedId = Intent.Extras.GetInt("selectedIngredientId");
                _selectedIngredient = _ingredientsList.Find(e => e.Id == selectedId);
            }
        }

        private void FindViews()
        {
            FindViewsNavigation();
            FindViewsIngredient();
            FindViewsOperation();
        }

        private void FindViewsNavigation()
        {
            _optionsButton = FindViewById<Button>(Resource.Id.optionsButton);
            _menusButton = FindViewById<Button>(Resource.Id.menusButton);
            _ingredientButton = FindViewById<Button>(Resource.Id.ingredientsButton);
            _recipeButton = FindViewById<Button>(Resource.Id.recipesButton);
        }

        private void FindViewsIngredient()
        {
            _ingredientNameEditText = FindViewById<TextInputEditText>(Resource.Id.nameEditText);
            _ingredientDescriptionEditText = FindViewById<TextInputEditText>(Resource.Id.descriptionEditText);
            _ingredientsUnitnEditText = FindViewById<TextInputEditText>(Resource.Id.unitEditText);
            _fructoseCheckBox = FindViewById<CheckBox>(Resource.Id.fructoseCheckBox);
            _histaminCheckBox = FindViewById<CheckBox>(Resource.Id.histaminCheckBox);
            _lactoseCheckBox = FindViewById<CheckBox>(Resource.Id.lactoseCheckBox);
            _celiacCheckBox = FindViewById<CheckBox>(Resource.Id.celiacCheckBox);
        }

        private void FindViewsOperation()
        {
            _searchButton = FindViewById<Button>(Resource.Id.searchButton);
            _newButton = FindViewById<Button>(Resource.Id.newButton);
            _saveButton = FindViewById<Button>(Resource.Id.saveButton);
            _abortButton = FindViewById<Button>(Resource.Id.abortButton);
            _deleteButton = FindViewById<Button>(Resource.Id.deleteButton);
        }

        private void BindData()
        {
            //Search TBD
            _ingredientNameEditText.Text = _selectedIngredient.Name;
            _ingredientDescriptionEditText.Text = _selectedIngredient.Description;
            _ingredientsUnitnEditText.Text = _selectedIngredient.ReferenceUnit;
            _fructoseCheckBox.Checked = _selectedIngredient.CompatibleForFructose;
            _histaminCheckBox.Checked = _selectedIngredient.CompatibleForHistamin;
            _lactoseCheckBox.Checked = _selectedIngredient.CompatibleForLactose;
            _celiacCheckBox.Checked = _selectedIngredient.CompatibleForCeliac;
        }

        private void LinkEventHandlers()
        {
            _optionsButton.Click += _optionsButton_Click;
            _menusButton.Click += _menusButton_Click;
            _ingredientButton.Click += _ingredientsButton_Click;
            _recipeButton.Click += _recipeButton_Click;
            _searchButton.Click += _searchButton_Click;
            _newButton.Click += _newButton_Click;
            _saveButton.Click += _saveButton_Click;
            _abortButton.Click += _abortButton_Click;
            _deleteButton.Click += _deleteButton_Click;
        }

        private void _optionsButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _menusButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _ingredientsButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(IngredientsMenuActivity));
            Recreate();
        }

        private void _recipeButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _searchButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(IngredientsMenuActivity));
            StartActivity(intent);
        }

        private void _newButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.ingredients);
            _selectedIngredient = new Ingredient();
            FindViews();
            BindData();
            LinkEventHandlers();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _abortButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.ingredients);
            FindViews();
            BindData();
            LinkEventHandlers();
        }

        private void _deleteButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public async Task LoadData()
        {
            _ingredientsList = await _ingredientsRepository.GetAllIngredients();
        }
    }
}