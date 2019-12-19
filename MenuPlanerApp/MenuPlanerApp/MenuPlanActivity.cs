using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
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
    public class MenuPlanActivity : AppCompatActivity
    {
        private Button _abortButton;
        private Button _deleteButton;
        private MenuPlanRepositoryWeb _menuPlanRepository;
        private Button _menusButton;
        private Button _newMenuPlanButton;
        private Button _ingredientButton;
        private Button _optionsButton;
        private Button _recipeButton;
        private RecipeRepositoryWeb _recipeRepository;
        private Button _menuPlanSearchButton;
        private List<Recipe> _recipesList;
        private List<MenuPlan> _menuPlanList;
        private Button _saveButton;
        private MenuPlan _selectedMenuPlan;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.menuPlan);
            InitialReferencingObjects();
            await LoadMenuPlanData();
            await LoadRecipeData();
            await FilterRecipes();
            SetSelectedMenuPlan();
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
        }

        private void InitialReferencingObjects()
        {
            _menuPlanRepository = new MenuPlanRepositoryWeb();
            _recipeRepository = new RecipeRepositoryWeb();
            _recipesList = new List<Recipe>();
            _menuPlanList = new List<MenuPlan>();
            _selectedMenuPlan = new MenuPlan();
        }

        private async Task LoadMenuPlanData()
        {
            var menues = await _menuPlanRepository.GetAllMenuPlan();
            if (menues != null)
            {
                _menuPlanList = menues;
            }
        }

        private async Task LoadRecipeData()
        {
            _recipesList = await _recipeRepository.GetAllRecipes();
        }

        private async Task FilterRecipes()
        {
            var recipeFilter = new RecipeFilter();
            _recipesList = await recipeFilter.FilterRecipes(_recipesList);
        }

        private void SetSelectedMenuPlan()
        {
            if (Intent.Extras == null || Intent.Extras.GetInt("selectedMenuPlanId") == 0)
            {
                _selectedMenuPlan = _menuPlanList.Count > 0 ? _menuPlanList.FirstOrDefault() : new MenuPlan();
            }
            else
            {
                var selectedId = Intent.Extras.GetInt("selectedMenuPlanId");
                SetSelectedMenuPlanResultOrFirstInList(selectedId);
            }
        }

        private void SetSelectedMenuPlanResultOrFirstInList(int selectedId)
        {
            var result = _menuPlanList.Find(e => e.Id == selectedId);
            _selectedMenuPlan = result == null ? _menuPlanList.First() : result;
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
            
        }

        private void FindViewsOperation()
        {
            _menuPlanSearchButton = FindViewById<Button>(Resource.Id.menuPlanSearchButton);
            _newMenuPlanButton = FindViewById<Button>(Resource.Id.newMenuPlanButton);
            _saveButton = FindViewById<Button>(Resource.Id.menuPlanSaveButton);
            _abortButton = FindViewById<Button>(Resource.Id.menuPlanAbortButton);
            _deleteButton = FindViewById<Button>(Resource.Id.menuPlanDeleteButton);
        }

        private void BindDataFromDataToView()
        {
            
        }

        private void LinkEventHandlers()
        {
            
        }
    }
}