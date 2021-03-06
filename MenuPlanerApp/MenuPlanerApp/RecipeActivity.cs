﻿using System;
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
using Android.Views;
using Android.Widget;
using MenuPlanerApp.Adapters;
using MenuPlanerApp.Core.Model;
using MenuPlanerApp.Core.Repository;
using MenuPlanerApp.Core.Utility;
using Xamarin.Essentials;

namespace MenuPlanerApp
{
    [Activity(Label = "@string/app_name")]
    public class RecipeActivity : AppCompatActivity
    {
        private const int RequestCamera = 1000;
        private const int IngredientsSearchRequestCode = 2000;
        private const int CameraRequestCode = 2001;
        private const int RecipeSearchRequestCode = 2002;
        private const string ActionAbortedMessage = "Vorgang abgebrochen";
        private const string FillOutNeededDataMessage = "Bitte füllen Sie alle Pflichtfelder aus";
        private const string ChangesSavedMessage = "Änderungen gespeichert";
        private const string RecipeAlreadyOpenedMessage = "Rezepte bereits geöffnet";
        private const string IngredientDataNotCompletelyMessage = "Zutatenangaben nicht vollständig";
        private const string ExtraCameraDataString = "data";
        private const string ExtraIngredientString = "selectedIngredientId";
        private const string NoIngredientSelectedMessage = "Keine Zutat gewählt";
        private const string IngredientUnitText = "Mengenangabe";
        private const string SelectIngredientString = "Zutat wählen";
        private const string ExtraRecipeString = "selectedRecipeId";
        private Button _abortButton;
        private Button _cameraButton;
        private Button _deleteButton;
        private TextInputEditText _ingredientAmountEditText;
        private Button _ingredientButton;
        private ListView _ingredientsListView;
        private IngredientsRepositoryWeb _ingredientsRepositoryWeb;
        private IngredientsWithAmountListViewAdapter _ingredientsWithAmountListViewAdapter;
        private Button _insertIngredientButton;
        private Bitmap _instructionsBitmap;
        private Button _menusButton;
        private Button _newRecipeButton;
        private Button _optionsButton;
        private int _positionSelectedListViewItem;
        private Button _recipeButton;
        private TextInputEditText _recipeDescriptionEditText;
        private ImageView _recipeImageView;
        private TextInputEditText _recipeNameEditText;
        private RecipeRepositoryWeb _recipeRepositoryWeb;
        private Button _recipeSearchButton;
        private List<Recipe> _recipesList;
        private Button _removeIngredientButton;
        private Button _saveButton;
        private Ingredient _selectedIngredient;
        private Recipe _selectedRecipe;
        private Button _selectIngredientButton;
        private UserOptionsRepositoryWeb _userOptionsRepositoryWeb;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.recipes);
            CheckPermissions();
            InitialReferencingObjects();
            await LoadRecipeData(_recipeRepositoryWeb);
            await FilterRecipes();
            SetSelectedRecipe();
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
        }

        private async Task FilterRecipes()
        {
            var recipeFilter = new RecipeFilter(_userOptionsRepositoryWeb);
            _recipesList = await recipeFilter.FilterRecipes(_recipesList);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            switch (requestCode)
            {
                case CameraRequestCode:
                    SetBitmapFromCameraToImageView(requestCode, resultCode, data);
                    break;
                case IngredientsSearchRequestCode:
                    BindDataFromIngredientSearchResultToView(requestCode, resultCode, data);
                    break;
                case RecipeSearchRequestCode:
                    SetSelectedRecipe(requestCode, resultCode, data);
                    break;
                default: return;
            }
        }

        private void InitialReferencingObjects()
        {
            _recipesList = new List<Recipe>();
            _selectedRecipe = new Recipe();
            _selectedIngredient = new Ingredient();
            _recipeRepositoryWeb = new RecipeRepositoryWeb();
            _userOptionsRepositoryWeb = new UserOptionsRepositoryWeb();
            _ingredientsRepositoryWeb = new IngredientsRepositoryWeb();
        }

        private async Task LoadRecipeData(RecipeRepositoryWeb recipeRepositoryWeb)
        {
            _recipesList = await recipeRepositoryWeb.GetAllRecipes();
        }


        private void SetSelectedRecipe(int requestCode, Result resultCode, Intent data)
        {
            if (data == null || !data.HasExtra(ExtraRecipeString)) return;

            base.OnActivityResult(requestCode, resultCode, data);

            if (data.Extras == null || data.Extras.GetInt(ExtraRecipeString) == 0)
            {
                _selectedRecipe = _recipesList.Count > 0 ? _recipesList.First() : new Recipe();
            }
            else
            {
                var selectedId = data.Extras.GetInt(ExtraRecipeString);
                SetSelectedRecipeResultOrFirstInList(selectedId);
            }

            BindDataFromDataToView();
        }

        private void SetSelectedRecipe()
        {
            if (Intent.Extras == null || Intent.Extras.GetInt(ExtraRecipeString) == 0)
            {
                _selectedRecipe = _recipesList.Count > 0 ? _recipesList.First() : new Recipe();
            }
            else
            {
                var selectedId = Intent.Extras.GetInt(ExtraRecipeString);
                SetSelectedRecipeResultOrFirstInList(selectedId);
            }
        }

        private void SetSelectedRecipeResultOrFirstInList(int selectedId)
        {
            var result = _recipesList.Find(e => e.Id == selectedId);
            _selectedRecipe = result ?? _recipesList.First();
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
            _ingredientAmountEditText = FindViewById<TextInputEditText>(Resource.Id.recipeAmountEditText);
            _insertIngredientButton = FindViewById<Button>(Resource.Id.recipeInsertIngredientButton);
            _ingredientsListView = FindViewById<ListView>(Resource.Id.recipeIngredientsListView);
            _removeIngredientButton = FindViewById<Button>(Resource.Id.recipeRemoveIngredientButton);
            _cameraButton = FindViewById<Button>(Resource.Id.recipeCameraButton);
            _recipeImageView = FindViewById<ImageView>(Resource.Id.recipeImageView);
            _recipeSearchButton = FindViewById<Button>(Resource.Id.recipeSearchButton);
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
            SetListViewHeightBasedOnChildren(_ingredientsListView);
            SetUpImageView();
        }

        private void SetUpImageView()
        {
            if (string.IsNullOrEmpty(_selectedRecipe.DirectionPictures)) return;
            _instructionsBitmap = ImageHelper.ConvertBase64StringToBitmap(_selectedRecipe.DirectionPictures);
            _recipeImageView.SetImageBitmap(_instructionsBitmap);
        }

        private void BindTextOnIngredientsButton()
        {
            _selectIngredientButton.Text =
                _selectedIngredient.Id == 0 ? SelectIngredientString : _selectedIngredient.Name;
        }

        private void BindHintOnAmountEditText()
        {
            _ingredientAmountEditText.Hint =
                _selectedIngredient.Id == 0 ? IngredientUnitText : _selectedIngredient.ReferenceUnit;
        }

        private void SetUpListView()
        {
            _ingredientsWithAmountListViewAdapter =
                new IngredientsWithAmountListViewAdapter(this, _selectedRecipe.Ingredients);
            _ingredientsListView.Adapter = _ingredientsWithAmountListViewAdapter;
        }

        private void BindDataFromViewToData()
        {
            _selectedRecipe.Name = _recipeNameEditText.Text;
            _selectedRecipe.Description = _recipeDescriptionEditText.Text;
            _selectedRecipe.DirectionPictures = ImageHelper.ConvertBitmapToBase64String(_instructionsBitmap);
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
            _recipeSearchButton.Click += SearchButton_Click;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, RecipeSearchRequestCode);
        }

        private void RemoveSelectedIngredientFromList_Click(object sender, EventArgs e)
        {
            if (_positionSelectedListViewItem == -1) ShowToastMessage(NoIngredientSelectedMessage);

            _selectedRecipe.Ingredients.RemoveAt(_positionSelectedListViewItem);
            _positionSelectedListViewItem = -1;
            SetUpListView();
        }

        private void ChangeSelectedItemField_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _positionSelectedListViewItem = e.Position;
        }

        private void SetBitmapFromCameraToImageView(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (data == null) return;

            _instructionsBitmap = (Bitmap) data.Extras.Get(ExtraCameraDataString);
            _recipeImageView.SetImageBitmap(_instructionsBitmap);
        }

        private async void BindDataFromIngredientSearchResultToView(int requestCode, Result resultCode, Intent data)
        {
            if (data == null || !data.HasExtra(ExtraIngredientString)) return;

            base.OnActivityResult(requestCode, resultCode, data);
            var ingredientId = data.Extras.GetInt(ExtraIngredientString);
            var ingredient = await _ingredientsRepositoryWeb.GetIngredientById(ingredientId);
            _selectedIngredient = ingredient;
            _selectIngredientButton.Text = _selectedIngredient.Name;
            _ingredientAmountEditText.Hint = _selectedIngredient.ReferenceUnit;
            _ingredientAmountEditText.Text = "";
        }

        private void CameraButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, CameraRequestCode);
        }

        private void InsertIngredientButton_Click(object sender, EventArgs e)
        {
            var newIngredientWithAmount = new IngredientWithAmount {Ingredient = _selectedIngredient};
            var decAsText = _ingredientAmountEditText.Text;
            newIngredientWithAmount.Amount = decimal.Parse(decAsText);

            if (VerifyUserEntries.IsIngredientWithAmountComplete(newIngredientWithAmount))
            {
                _selectedRecipe.Ingredients.Add(newIngredientWithAmount);
                SetUpListView();
                SetListViewHeightBasedOnChildren(_ingredientsListView);
                _selectedIngredient = new Ingredient();
                _selectIngredientButton.Text = SelectIngredientString;
                _ingredientAmountEditText.Text = "";
                _ingredientAmountEditText.Hint = IngredientUnitText;
            }
            else
            {
                ShowToastMessage(IngredientDataNotCompletelyMessage);
            }
        }


        private void OptionsButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(OptionsActivity));
            StartActivity(intent);
        }

        private void MenusButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MenuPlanActivity));
            StartActivity(intent);
        }

        private void IngredientsButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(IngredientsActivity));
            StartActivity(intent);
        }

        private void RecipeButton_Click(object sender, EventArgs e)
        {
            ShowToastMessage(RecipeAlreadyOpenedMessage);
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
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
        }


        private async void SaveButton_Click(object sender, EventArgs e)
        {
            BindDataFromViewToData();
            if (VerifyUserEntries.IsRecipeComplete(_selectedRecipe))
            {
                await SaveOrUpdateRecipe(_recipeRepositoryWeb);
                ShowToastMessage(ChangesSavedMessage);
            }
            else
            {
                ShowToastMessage(FillOutNeededDataMessage);
            }

            await LoadRecipeData(_recipeRepositoryWeb);
            await FilterRecipes();
        }

        private void AbortButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.recipes);
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
            ShowToastMessage(ActionAbortedMessage);
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            var toDeletingName = _selectedRecipe.Name;
            await _recipeRepositoryWeb.DeleteRecipeById(_selectedRecipe.Id);
            Recreate();
            ShowToastMessage($"Das Rezept {toDeletingName} wurde gelöscht");
        }

        private async Task SaveOrUpdateRecipe(RecipeRepositoryWeb recipeRepositoryWeb)
        {
            if (_selectedRecipe.Id != 0)
                await recipeRepositoryWeb.UpdateRecipe(_selectedRecipe);

            else
                _selectedRecipe = await recipeRepositoryWeb.PostRecipe(_selectedRecipe);
        }

        private void ShowToastMessage(string text)
        {
            var toastMessage = text;
            const ToastLength duration = ToastLength.Long;
            Toast.MakeText(this, toastMessage, duration).Show();
        }

        private void CheckPermissions()
        {
            ActivityCompat.RequestPermissions(this, new[] {Manifest.Permission.Camera}, RequestCamera);
        }

        private static void SetListViewHeightBasedOnChildren(ListView listView)
        {
            var listAdapter = listView.Adapter;
            if (listAdapter == null)
                return;

            var totalHeight = 0;
            var desiredWidth = View.MeasureSpec.MakeMeasureSpec(listView.Width, MeasureSpecMode.AtMost);
            for (var i = 0; i < listAdapter.Count; i++)
            {
                var listItem = listAdapter.GetView(i, null, listView);
                listItem.Measure(desiredWidth, 0);
                totalHeight += listItem.MeasuredHeight;
            }

            var parameters = listView.LayoutParameters;
            parameters.Height = totalHeight + listView.DividerHeight * (listAdapter.Count - 1);
            listView.LayoutParameters = parameters;
            listView.RequestLayout();
        }
    }
}