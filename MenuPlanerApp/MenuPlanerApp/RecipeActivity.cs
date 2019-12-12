using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using MenuPlanerApp.Adapters;
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
        private Button _cameraButton;
        private Button _deleteButton;
        private TextInputEditText _ingredientAmounEditText;
        private Button _ingredientButton;
        private List<IngredientWithAmount> _ingredientsList;
        private ListView _ingredientsListView;
        private IngredientsRepositoryWeb _ingredientsRepository;
        private IngredientsWithAmountListViewAdapter _ingredientsWithAmountListViewAdapter;
        private Button _insertIngredientButton;
        private Button _menusButton;
        private Button _newRecipeButton;
        private Button _optionsButton;
        private Button _recipeButton;
        private TextInputEditText _recipeDescriptionEditText;
        private ImageView _recipeImageView;
        private TextInputEditText _recipeNameEditText;
        private RecipeRepositoryWeb _recipeRepository;
        private Button _recipeSearchButton;
        private List<Recipe> _recipesList;
        private Button _removeIngredientButton;
        private Button _saveButton;
        private Ingredient _selectedIngredient;
        private Recipe _selectedRecipe;
        private Button _selectIngredientButton;
        private VerifyUserEntries _verifyUserEntries;


        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.recipes);
            InitialReferencingObjects();
            await LoadData();
            SetSelectedRecipe();
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
        }

        private void InitialReferencingObjects()
        {
            _recipesList = new List<Recipe>();
            _ingredientsRepository = new IngredientsRepositoryWeb();
            _recipeRepository = new RecipeRepositoryWeb();
            _selectedRecipe = new Recipe();
            _verifyUserEntries = new VerifyUserEntries();
            _selectedIngredient = new Ingredient();
            _ingredientsList = new List<IngredientWithAmount>();
        }

        private async Task LoadData()
        {
            _recipesList = await _recipeRepository.GetAllRecipes();
        }

        private void SetSelectedRecipe()
        {
            if (Intent.Extras == null)
            {
                if (_recipesList.Count > 0)
                    _selectedRecipe = _recipesList.First();
                else
                    _selectedRecipe = new Recipe();
            }
            else
            {
                var selectedId = Intent.Extras.GetInt("selectedRecipeId");
                SetSelectedRecipeResultOrFirstInList(selectedId);
            }
        }

        private void SetSelectedRecipeResultOrFirstInList(int selectedId)
        {
            var result = _recipesList.Find(e => e.Id == selectedId);
            _selectedRecipe = result == null ? _recipesList.First() : result;
        }

        private void FindViews()
        {
            FindViewsNavigation();
            FindViewsRecipe();
            FindViewsOperation();
        }

        private void FindViewsNavigation()
        {
            _optionsButton = FindViewById<Button>(Resource.Id.optionsButton);
            _menusButton = FindViewById<Button>(Resource.Id.menusButton);
            _ingredientButton = FindViewById<Button>(Resource.Id.ingredientsButton);
            _recipeButton = FindViewById<Button>(Resource.Id.recipesButton);
        }

        private void FindViewsRecipe()
        {
            _recipeNameEditText = FindViewById<TextInputEditText>(Resource.Id.recipeNameEditText);
            _recipeDescriptionEditText = FindViewById<TextInputEditText>(Resource.Id.recipeDescriptionEditText);
            _selectIngredientButton = FindViewById<Button>(Resource.Id.recipeSelectIngredientButton);
            _ingredientAmounEditText = FindViewById<TextInputEditText>(Resource.Id.recipeAmountEditText);
            _insertIngredientButton = FindViewById<Button>(Resource.Id.recipeInsertIngredientButton);
            _ingredientsListView = FindViewById<ListView>(Resource.Id.recipeIngredientsListView);
            _removeIngredientButton = FindViewById<Button>(Resource.Id.recipeRemoveIngredientButton);
            _cameraButton = FindViewById<Button>(Resource.Id.recipeCameraButton);
            _recipeImageView = FindViewById<ImageView>(Resource.Id.recipeImageView);
        }

        private void FindViewsOperation()
        {
            _recipeSearchButton = FindViewById<Button>(Resource.Id.recipeSearchButton);
            _newRecipeButton = FindViewById<Button>(Resource.Id.newRecipeButton);
            _saveButton = FindViewById<Button>(Resource.Id.recipeSaveButton);
            _abortButton = FindViewById<Button>(Resource.Id.recipeAbortButton);
            _deleteButton = FindViewById<Button>(Resource.Id.recipeDeleteButton);
        }

        private void BindDataFromDataToView()
        {
            _recipeNameEditText.Text = _selectedRecipe.Name;
            _recipeDescriptionEditText.Text = _selectedRecipe.Description;
            BindTextOnIngredientsButton();
            BindHintOnAmountEditText();
            SetUpListView();

        }

        private void BindTextOnIngredientsButton()
        {
            _selectIngredientButton.Text = _selectedIngredient.Id == 0 ? "Zutat" : _selectedIngredient.Name;
        }

        private void BindHintOnAmountEditText()
        {
            _ingredientAmounEditText.Hint =
                _selectedIngredient.Id == 0 ? "Mengenangabe" : _selectedIngredient.ReferenceUnit;
        }

        private void SetUpListView()
        {
            _ingredientsWithAmountListViewAdapter = new IngredientsWithAmountListViewAdapter(this, _ingredientsList);
            _ingredientsListView.Adapter = _ingredientsWithAmountListViewAdapter;
        }

        private void BindDataFromViewToData()
        {
            _selectedRecipe.Name = _recipeNameEditText.Text;
            _selectedRecipe.Description = _recipeDescriptionEditText.Text;
        }

        private void LinkEventHandlers()
        {
            _optionsButton.Click += OptionsButton_Click;
            _menusButton.Click += MenusButton_Click;
            _ingredientButton.Click += IngredientsButton_Click;
            _recipeButton.Click += RecipeButton_Click;
            _selectIngredientButton.Click += SelectIngredientButtonClick;
            _newRecipeButton.Click += NewButton_Click;
            _saveButton.Click += SaveButton_Click;
            _abortButton.Click += AbortButton_Click;
            _deleteButton.Click += DeleteButton_Click;
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(OptionsActivity));
            StartActivity(intent);
        }

        private void MenusButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void IngredientsButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(IngredientsActivity));
            StartActivity(intent);
        }

        private void RecipeButton_Click(object sender, EventArgs e)
        {
            ShowToastMessage("Rezepte bereits geöffnet");
        }

        private void SelectIngredientButtonClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivity(intent);
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.recipes);
            _selectedRecipe = new Recipe();
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            BindDataFromViewToData();
            if (_verifyUserEntries.IsRecipeComplete(_selectedRecipe))
            {
                await SaveOrUpdateRecipe();
                ShowToastMessage("Änderungen gespeichert");
            }
            else
            {
                ShowToastMessage("Bitte füllen Sie alle Pflichtfelder aus");
            }
        }

        private void AbortButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.recipes);
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
            ShowToastMessage("Vorgang abgebrochen");
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            var toDeletingName = _selectedRecipe.Name;
            await _ingredientsRepository.DeleteIngredientById(_selectedRecipe.Id);
            Recreate();
            ShowToastMessage($"Das Rezept {toDeletingName} wurde gelöscht");
        }

        private async Task SaveOrUpdateRecipe()
        {
            if (_selectedRecipe.Id != 0)
                await _recipeRepository.UpdateRecipe(_selectedRecipe);
            else
                await _recipeRepository.PostRecipe(_selectedRecipe);
        }

        private void ShowToastMessage(string text)
        {
            var toastMessage = text;
            const ToastLength duration = ToastLength.Long;
            Toast.MakeText(this, toastMessage, duration).Show();
        }
    }
}