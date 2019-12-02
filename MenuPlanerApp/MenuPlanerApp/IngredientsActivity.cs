using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
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

        //Ingredientsdetails
        private AutoCompleteTextView _ingredientsSearchAutoCompleteTextView;
        private CheckBox _lactoseCheckBox;

        private Button _menusButton;

        //Navigation
        private Button _optionsButton;
        private Button _recipeButton;

        //Operations
        private Button _saveButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ingredients);

            FindViews();
            LinkEventHandlers();
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
            _ingredientsSearchAutoCompleteTextView =
                FindViewById<AutoCompleteTextView>(Resource.Id.ingredientSearchAutocomplete);
            _ingredientNameEditText = FindViewById<TextInputEditText>(Resource.Id.nameEditText);
            _ingredientDescriptionEditText = FindViewById<TextInputEditText>(Resource.Id.descriptionEditText);
            _fructoseCheckBox = FindViewById<CheckBox>(Resource.Id.fructoseCheckBox);
            _histaminCheckBox = FindViewById<CheckBox>(Resource.Id.histaminCheckBox);
            _lactoseCheckBox = FindViewById<CheckBox>(Resource.Id.lactoseCheckBox);
            _celiacCheckBox = FindViewById<CheckBox>(Resource.Id.celiacCheckBox);
        }

        private void FindViewsOperation()
        {
            _saveButton = FindViewById<Button>(Resource.Id.saveButton);
            _abortButton = FindViewById<Button>(Resource.Id.abortButton);
            _deleteButton = FindViewById<Button>(Resource.Id.deleteButton);
        }

        private void LinkEventHandlers()
        {
            _optionsButton.Click += _optionsButton_Click;
            _menusButton.Click += _menusButton_Click;
            _ingredientButton.Click += _ingredientsButton_Click;
            _recipeButton.Click += _recipeButton_Click;
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
            StartActivity(intent);
        }

        private void _recipeButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}