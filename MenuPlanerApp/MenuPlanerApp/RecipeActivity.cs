using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Widget;
using MenuPlanerApp.Adapters;
using MenuPlanerApp.Core.Model;
using MenuPlanerApp.Core.Repository;
using MenuPlanerApp.Core.Utility;
using MenuPlanerApp.Core.VerifyData;
using Xamarin.Essentials;

namespace MenuPlanerApp
{
    [Activity(Label = "@string/app_name")]
    public class RecipeActivity : AppCompatActivity
    {
        private const int IngredientsSearchRequestCode = 2000;
        private const int CameraRequestCode = 2001;
        private readonly int REQUEST_CAMERA = 1000;
        private Button _abortButton;
        private Button _cameraButton;
        private Button _deleteButton;
        private Bitmap _instructionsBitmap;
        private TextInputEditText _ingredientAmounEditText;
        private Button _ingredientButton;
        private List<IngredientWithAmount> _ingredientsList;
        private ListView _ingredientsListView;
        private IngredientsRepositoryWeb _ingredientsRepository;
        private IngredientWithAmountRepositoryWeb _ingredientWithAmountRepositoryWeb;
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
        private ImageHelper _imageHelper;
        private Button _saveButton;
        private Ingredient _selectedIngredient;
        private Recipe _selectedRecipe;
        private Button _selectIngredientButton;
        private VerifyUserEntries _verifyUserEntries;
        private int PositionSelectedListViewItem;


        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.recipes);
            CheckPermissions();
            InitialReferencingObjects();
            await LoadRecipeData();
            SetSelectedRecipe();
            await LoadIngredientsWithAmountData();
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == CameraRequestCode) SetBitmapFromCameraToImageView(requestCode, resultCode, data);

            if (requestCode == IngredientsSearchRequestCode)
                BindDataFromIngredientSearchResultToView(requestCode, resultCode, data);
        }

        private void InitialReferencingObjects()
        {
            _recipesList = new List<Recipe>();
            _ingredientsRepository = new IngredientsRepositoryWeb();
            _ingredientWithAmountRepositoryWeb = new IngredientWithAmountRepositoryWeb();
            _recipeRepository = new RecipeRepositoryWeb();
            _selectedRecipe = new Recipe();
            _verifyUserEntries = new VerifyUserEntries();
            _selectedIngredient = new Ingredient();
            _ingredientsList = new List<IngredientWithAmount>();
            _imageHelper = new ImageHelper();
        }

        private async Task LoadRecipeData()
        {
            _recipesList = await _recipeRepository.GetAllRecipes();
        }

        private async Task LoadIngredientsWithAmountData()
        {
            if(_selectedRecipe.Id > 0)
            {
                _ingredientsList = await _ingredientWithAmountRepositoryWeb.GetIngredientWithAmountForRecipeId(_selectedRecipe.Id);
            }
        }
        

        private void SetSelectedRecipe()
        {
            if (Intent.Extras == null || Intent.Extras.GetInt("selectedRecipeId") == 0)
            {
                _selectedRecipe = _recipesList.Count > 0 ? _recipesList.First() : new Recipe();
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
            SetUpImageView();
        }

        private void SetUpImageView()
        {
            if (string.IsNullOrEmpty(_selectedRecipe.DirectionPictures)) return;
            _instructionsBitmap = _imageHelper.ConvertBase64StringToBitmap(_selectedRecipe.DirectionPictures);
            _recipeImageView.SetImageBitmap(_instructionsBitmap);
        }

        private void BindTextOnIngredientsButton()
        {
            _selectIngredientButton.Text = _selectedIngredient.Id == 0 ? "Zutat wählen" : _selectedIngredient.Name;
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
            _selectedRecipe.Ingredients = _ingredientsList;
            _selectedRecipe.DirectionPictures = _imageHelper.ConvertBitmapToBase64String(_instructionsBitmap);
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
            _cameraButton.Click += CameraButton_Click;
            _insertIngredientButton.Click += InsertIngredientButton_Click;
            _ingredientsListView.ItemClick += ChangeSelectedItemField_ItemClick;
            _removeIngredientButton.Click += RemoveSelectedIngredientFromList_Click;
        }


        private void RemoveSelectedIngredientFromList_Click(object sender, EventArgs e)
        {
            if (PositionSelectedListViewItem == -1) ShowToastMessage("Keine Zutat gewählt");
            _ingredientsList.RemoveAt(PositionSelectedListViewItem);
            PositionSelectedListViewItem = -1;
            SetUpListView();
        }

        private void ChangeSelectedItemField_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            PositionSelectedListViewItem = e.Position;
        }

        private void SetBitmapFromCameraToImageView(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(data == null) return;

            _instructionsBitmap = (Bitmap) data.Extras.Get("data");
            _recipeImageView.SetImageBitmap(_instructionsBitmap);
        }

        private async void BindDataFromIngredientSearchResultToView(int requestCode, Result resultCode, Intent data)
        {
            
            if (data == null || !data.HasExtra("selectedIngredientId")) return;

            base.OnActivityResult(requestCode, resultCode, data);
            var ingredientId = data.Extras.GetInt("selectedIngredientId");
            var ingredient = await _ingredientsRepository.GetIngredientById(ingredientId);
            _selectedIngredient = ingredient;
            _selectIngredientButton.Text = _selectedIngredient.Name;
            _ingredientAmounEditText.Hint = _selectedIngredient.ReferenceUnit;
            _ingredientAmounEditText.Text = "";
        }

        private void CameraButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, CameraRequestCode);
        }

        private void InsertIngredientButton_Click(object sender, EventArgs e)
        {
            var newIngredientWithAmount = new IngredientWithAmount {Ingredient = _selectedIngredient};
            var decAsText = _ingredientAmounEditText.Text;
            newIngredientWithAmount.Amount = decimal.Parse(decAsText);

            if (VerifyUserEntries.IsIngredientWithAmountComplete(newIngredientWithAmount))
            {
                _ingredientsList.Add(newIngredientWithAmount);
                SetUpListView();
                _selectedIngredient = new Ingredient();
                _selectIngredientButton.Text = "Zutat wählen";
                _ingredientAmounEditText.Text = "";
                _ingredientAmounEditText.Hint = "Mengenangabe";
            }
            else
            {
                ShowToastMessage("Zutatenangaben nicht vollständig");
            }
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
            var intent = new Intent(this, typeof(IngredientsSearchActivity));
            StartActivityForResult(intent, IngredientsSearchRequestCode);
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.recipes);
            _selectedRecipe = new Recipe();
            _ingredientsList = new List<IngredientWithAmount>();
            BindDataFromDataToView();
            LinkEventHandlers();
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            BindDataFromViewToData();
            if (VerifyUserEntries.IsRecipeComplete(_selectedRecipe))
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
            await _recipeRepository.DeleteRecipeById(_selectedRecipe.Id);
            Recreate();
            ShowToastMessage($"Das Rezept {toDeletingName} wurde gelöscht");
        }

        private async Task SaveOrUpdateRecipe()
        {
            if (_selectedRecipe.Id != 0)
            {
                await _recipeRepository.UpdateRecipe(_selectedRecipe);
                foreach (var ingr in _ingredientsList)
                {
                    await _ingredientWithAmountRepositoryWeb.UpdateIngredient(ingr);
                }
            }

            else
            {
                var newRecipe = await _recipeRepository.PostRecipe(_selectedRecipe);
                foreach (var ingr in _ingredientsList)
                {
                    ingr.RecipeId = newRecipe.Id;
                    await _ingredientWithAmountRepositoryWeb.UpdateIngredient(ingr);
                }
            }
        }

        private void ShowToastMessage(string text)
        {
            var toastMessage = text;
            const ToastLength duration = ToastLength.Long;
            Toast.MakeText(this, toastMessage, duration).Show();
        }

        private void CheckPermissions()
        {
            ActivityCompat.RequestPermissions(this, new[] {Manifest.Permission.Camera}, REQUEST_CAMERA);
        }
    }
}