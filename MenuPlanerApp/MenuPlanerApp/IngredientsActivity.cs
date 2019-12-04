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
using MenuPlanerApp.Core.VerifyData;
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
        private VerifyUserEntries _verifyUserEntries;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ingredients);
            InitialReferencingObjects();
            await LoadData();
            SetSelectedIngredient();
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
        }

        private void InitialReferencingObjects()
        {
            _ingredientsRepository = new IngredientsRepositoryWeb();
            _selectedIngredient = new Ingredient();
            _verifyUserEntries = new VerifyUserEntries();
        }

        private void SetSelectedIngredient()
        {
            if (Intent.Extras == null)
            {
                _selectedIngredient = _ingredientsList.First();
            }
            else
            {
                var selectedId = Intent.Extras.GetInt("selectedIngredientId");
                SetSelectedIngredientResultOrFirstInList(selectedId);
            }
        }

        private void SetSelectedIngredientResultOrFirstInList(int selectedId)
        {
            var result = _ingredientsList.Find(e => e.Id == selectedId);
            _selectedIngredient = result == null ? _ingredientsList.First() : result;
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

        private void BindDataFromDataToView()
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

        private void BindDataFromViewToData()
        {
            //Search TBD
            _selectedIngredient.Name = _ingredientNameEditText.Text;
            _selectedIngredient.Description = _ingredientDescriptionEditText.Text;
            _selectedIngredient.ReferenceUnit = _ingredientsUnitnEditText.Text;
            _selectedIngredient.CompatibleForFructose = _fructoseCheckBox.Checked;
            _selectedIngredient.CompatibleForHistamin = _histaminCheckBox.Checked;
            _selectedIngredient.CompatibleForLactose = _lactoseCheckBox.Checked;
            _selectedIngredient.CompatibleForCeliac = _celiacCheckBox.Checked;
        }

        private void LinkEventHandlers()
        {
            _optionsButton.Click += OptionsButton_Click;
            _menusButton.Click += MenusButton_Click;
            _ingredientButton.Click += IngredientsButton_Click;
            _recipeButton.Click += RecipeButton_Click;
            _searchButton.Click += SearchButton_Click;
            _newButton.Click += NewButton_Click;
            _saveButton.Click += SaveButton_Click;
            _abortButton.Click += AbortButton_Click;
            _deleteButton.Click += DeleteButton_Click;
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MenusButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void IngredientsButton_Click(object sender, EventArgs e)
        {
            ShowToastMessage("Zutaten bereits geöffnet");
        }

        private void RecipeButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(IngredientsMenuActivity));
            StartActivity(intent);
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.ingredients);
            _selectedIngredient = new Ingredient();
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            BindDataFromViewToData();
            if (_verifyUserEntries.IsIngredientComplete(_selectedIngredient))
            {
                await SaveOrUpdateIngredient();
                Recreate();
                ShowToastMessage("Änderungen gespeichert");
            }
            else
            {
                ShowToastMessage("Bitte füllen Sie alle Pflichtfelder aus");
            }
        }

        private void ShowToastMessage(string text)
        {
            var toastMessage = text;
            const ToastLength duration = ToastLength.Long;
            Toast.MakeText(this, toastMessage, duration).Show();
        }

        private async Task SaveOrUpdateIngredient()
        {
            if (_selectedIngredient.Id != 0)
                await _ingredientsRepository.UpdateIngredient(_selectedIngredient);
            else
                await _ingredientsRepository.PostIngredient(_selectedIngredient);
        }

        private void AbortButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.ingredients);
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
            ShowToastMessage("Vorgang abgebrochen");
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            var toDeletingName = _selectedIngredient.Name;
            await _ingredientsRepository.DeleteIngredientById(_selectedIngredient.Id);
            Recreate();
            ShowToastMessage($"Die Zutat {toDeletingName} wurde gelöscht");
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private async Task LoadData()
        {
            _ingredientsList = await _ingredientsRepository.GetAllIngredients();
        }
    }
}