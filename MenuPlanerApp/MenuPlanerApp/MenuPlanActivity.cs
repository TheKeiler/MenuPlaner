using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using MenuPlanerApp.Core.Model;
using MenuPlanerApp.Core.Repository;
using MenuPlanerApp.Core.Utility;
using MenuPlanerApp.Fragments;
using Xamarin.Essentials;

namespace MenuPlanerApp
{
    [Activity(Label = "@string/app_name")]
    public class MenuPlanActivity : AppCompatActivity
    {
        private const int DayOneLunchRequestCode = 3000;
        private const int DayOneDinnerRequestCode = 3001;
        private const int DayTwoLunchRequestCode = 3002;
        private const int DayTwoDinnerRequestCode = 3003;
        private const int DayThreeLunchRequestCode = 3004;
        private const int DayThreeDinnerRequestCode = 3005;
        private const int DayFourLunchRequestCode = 3006;
        private const int DayFourDinnerRequestCode = 3007;
        private const int DayFiveLunchRequestCode = 3008;
        private const int DayFiveDinnerRequestCode = 3009;
        private const int DaySixLunchRequestCode = 3010;
        private const int DaySixDinnerRequestCode = 3011;
        private const int DaySevenLunchRequestCode = 3012;
        private const int DaySevenDinnerRequestCode = 3013;
        private const int MenuPlanSearchRequestCode = 3050;
        private const string PutExtraStringMenuPlanId = "selectedMenuPlanId";
        private const string MenuPlanHasFirstToBeSavedMessage = "Der Menüplan muss zuerst gespeichert werden";
        private const string MenuAlreadyOpenedMessage = "Menüs bereits geöffnet";
        private const string NoDateSelectedMessage = "Kein Datum gewählt";
        private const string ActionAbortedMessage = "Vorgang abgebrochen";
        private const string FillNeededDataMessage = "Bitte füllen Sie alle Pflichtfelder aus";
        private const string SavedUpdatedDataMessage = "Änderungen gespeichert";
        private const string DefaultTextMealCount = "";
        private const string DefaultTextLunch = "Mittag";
        private const string DefaultTextDinner = "Abend";

        private Button _abortButton;
        private TextView _dateDisplay;
        private Button _dateSelectButton;
        private Button _dayFiveDinnerButton;
        private TextInputEditText _dayFiveDinnerEditText;
        private Button _dayFiveLunchButton;
        private TextInputEditText _dayFiveLunchEditText;
        private TextView _dayFiveTextView;
        private Button _dayFourDinnerButton;
        private TextInputEditText _dayFourDinnerEditText;
        private Button _dayFourLunchButton;
        private TextInputEditText _dayFourLunchEditText;
        private TextView _dayFourTextView;
        private Button _dayOneDinnerButton;
        private TextInputEditText _dayOneDinnerEditText;
        private Button _dayOneLunchButton;
        private TextInputEditText _dayOneLunchEditText;
        private TextView _dayOneTextView;
        private Button _daySevenDinnerButton;
        private TextInputEditText _daySevenDinnerEditText;
        private Button _daySevenLunchButton;
        private TextInputEditText _daySevenLunchEditText;
        private TextView _daySevenTextView;
        private Button _daySixDinnerButton;
        private TextInputEditText _daySixDinnerEditText;
        private Button _daySixLunchButton;
        private TextInputEditText _daySixLunchEditText;
        private TextView _daySixTextView;
        private Button _dayThreeDinnerButton;
        private TextInputEditText _dayThreeDinnerEditText;
        private Button _dayThreeLunchButton;
        private TextInputEditText _dayThreeLunchEditText;
        private TextView _dayThreeTextView;
        private Button _dayTwoDinnerButton;
        private TextInputEditText _dayTwoDinnerEditText;
        private Button _dayTwoLunchButton;
        private TextInputEditText _dayTwoLunchEditText;
        private TextView _dayTwoTextView;
        private Button _deleteButton;
        private Button _ingredientButton;
        private List<MenuPlan> _menuPlanList;
        private MenuPlanRepositoryWeb _menuPlanRepositoryWeb;
        private Button _menuPlanSearchButton;
        private Button _menusButton;
        private Button _newMenuPlanButton;
        private Button _optionsButton;
        private Button _recipeButton;
        private RecipeRepositoryWeb _recipeRepositoryWeb;
        private List<Recipe> _recipesList;
        private Button _saveButton;
        private MenuPlan _selectedMenuPlan;
        private Button _shoppingButton;
        private UserOptionsRepositoryWeb _userOptionsRepositoryWeb;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.menuPlan);
            InitialReferencingObjects();
            await LoadMenuPlanData(_menuPlanRepositoryWeb);
            await LoadRecipeData(_recipeRepositoryWeb);
            await FilterRecipes(_userOptionsRepositoryWeb);
            SetSelectedMenuPlan();
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
        }

        private void InitialReferencingObjects()
        {
            _recipesList = new List<Recipe>();
            _menuPlanList = new List<MenuPlan>();
            _selectedMenuPlan = new MenuPlan();
            _menuPlanRepositoryWeb = new MenuPlanRepositoryWeb();
            _userOptionsRepositoryWeb = new UserOptionsRepositoryWeb();
            _recipeRepositoryWeb = new RecipeRepositoryWeb();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            var selectedItem = new Recipe();
            if (requestCode >= DayOneLunchRequestCode && requestCode <= DaySevenDinnerRequestCode)
                selectedItem = FindSelectedRecipeInList(requestCode, resultCode, data);

            switch (requestCode)
            {
                case DayOneLunchRequestCode:
                    InsertSelectedItemFirstDayLunch(selectedItem);
                    break;
                case DayOneDinnerRequestCode:
                    InsertSelectedItemFirstDayDinner(selectedItem);
                    break;
                case DayTwoLunchRequestCode:
                    InsertSelectedItemSecondDayLunch(selectedItem);
                    break;
                case DayTwoDinnerRequestCode:
                    InsertSelectedItemSecondDayDinner(selectedItem);
                    break;
                case DayThreeLunchRequestCode:
                    InsertSelectedItemThirdDayLunch(selectedItem);
                    break;
                case DayThreeDinnerRequestCode:
                    InsertSelectedItemThirdDayDinner(selectedItem);
                    break;
                case DayFourLunchRequestCode:
                    InsertSelectedItemFourthDayLunch(selectedItem);
                    break;
                case DayFourDinnerRequestCode:
                    InsertSelectedItemFourthDayDinner(selectedItem);
                    break;
                case DayFiveLunchRequestCode:
                    InsertSelectedItemFifthDayLunch(selectedItem);
                    break;
                case DayFiveDinnerRequestCode:
                    InsertSelectedItemFifthDayDinner(selectedItem);
                    break;
                case DaySixLunchRequestCode:
                    InsertSelectedItemSixthDayLunch(selectedItem);
                    break;
                case DaySixDinnerRequestCode:
                    InsertSelectedItemSixthDayDinner(selectedItem);
                    break;
                case DaySevenLunchRequestCode:
                    InsertSelectedItemSeventhDayLunch(selectedItem);
                    break;
                case DaySevenDinnerRequestCode:
                    InsertSelectedItemSeventhDayDinner(selectedItem);
                    break;
                case MenuPlanSearchRequestCode:
                    SetSelectedMenuPlan(requestCode, resultCode, data);
                    break;
                default:
                    return;
            }
        }

        private Recipe FindSelectedRecipeInList(in int requestCode, Result resultCode, Intent data)
        {
            if (data == null || !data.HasExtra("selectedRecipeId")) return null;

            base.OnActivityResult(requestCode, resultCode, data);
            var recipeId = data.Extras.GetInt("selectedRecipeId");
            var recipe = _recipesList.Find(r => r.Id == recipeId);
            return recipe;
        }

        private void SetSelectedMenuPlan(int requestCode, Result resultCode, Intent data)
        {
            if (data == null || !data.HasExtra(PutExtraStringMenuPlanId)) return;

            base.OnActivityResult(requestCode, resultCode, data);

            if (data.Extras == null || data.Extras.GetInt(PutExtraStringMenuPlanId) == 0)
            {
                _selectedMenuPlan = _menuPlanList.Count > 0 ? _menuPlanList.First() : new MenuPlan();
            }
            else
            {
                var selectedId = data.Extras.GetInt(PutExtraStringMenuPlanId);
                SetSelectedMenuPlanResultOrFirstInList(selectedId);
            }

            BindDataFromDataToView();
        }

        private async Task LoadMenuPlanData(MenuPlanRepositoryWeb menuPlanRepositoryWeb)
        {
            var menus = await menuPlanRepositoryWeb.GetAllMenuPlan();
            if (menus != null) _menuPlanList = menus;
        }

        private async Task LoadRecipeData(RecipeRepositoryWeb recipeRepositoryWeb)
        {
            _recipesList = await recipeRepositoryWeb.GetAllRecipes();
        }

        private async Task FilterRecipes(UserOptionsRepositoryWeb userOptionsRepositoryWeb)
        {
            var recipeFilter = new RecipeFilter(userOptionsRepositoryWeb);
            _recipesList = await recipeFilter.FilterRecipes(_recipesList);
        }

        private void SetSelectedMenuPlan()
        {
            if (Intent.Extras == null || Intent.Extras.GetInt(PutExtraStringMenuPlanId) == 0)
            {
                _selectedMenuPlan = _menuPlanList.Count > 0 ? _menuPlanList.FirstOrDefault() : new MenuPlan();
            }
            else
            {
                var selectedId = Intent.Extras.GetInt(PutExtraStringMenuPlanId);
                SetSelectedMenuPlanResultOrFirstInList(selectedId);
            }
        }

        private void SetSelectedMenuPlanResultOrFirstInList(int selectedId)
        {
            var result = _menuPlanList.Find(e => e.Id == selectedId);
            _selectedMenuPlan = result ?? _menuPlanList.First();
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
            _dateSelectButton = FindViewById<Button>(Resource.Id.menuPlanDateButton);
            _dateDisplay = FindViewById<TextView>(Resource.Id.menuPlanDateDisplay);
            _dayOneTextView = FindViewById<TextView>(Resource.Id.dayOneTextView);
            _dayTwoTextView = FindViewById<TextView>(Resource.Id.dayTwoTextView);
            _dayThreeTextView = FindViewById<TextView>(Resource.Id.dayThreeTextView);
            _dayFourTextView = FindViewById<TextView>(Resource.Id.dayFourTextView);
            _dayFiveTextView = FindViewById<TextView>(Resource.Id.dayFiveTextView);
            _daySixTextView = FindViewById<TextView>(Resource.Id.daySixTextView);
            _daySevenTextView = FindViewById<TextView>(Resource.Id.daySevenTextView);
            _dayOneLunchEditText = FindViewById<TextInputEditText>(Resource.Id.dayOneLunchEditText);
            _dayTwoLunchEditText = FindViewById<TextInputEditText>(Resource.Id.dayTwoLunchEditText);
            _dayThreeLunchEditText = FindViewById<TextInputEditText>(Resource.Id.dayThreeLunchEditText);
            _dayFourLunchEditText = FindViewById<TextInputEditText>(Resource.Id.dayFourLunchEditText);
            _dayFiveLunchEditText = FindViewById<TextInputEditText>(Resource.Id.dayFiveLunchEditText);
            _daySixLunchEditText = FindViewById<TextInputEditText>(Resource.Id.daySixLunchEditText);
            _daySevenLunchEditText = FindViewById<TextInputEditText>(Resource.Id.daySevenLunchEditText);
            _dayOneLunchButton = FindViewById<Button>(Resource.Id.dayOneLunchButton);
            _dayTwoLunchButton = FindViewById<Button>(Resource.Id.dayTwoLunchButton);
            _dayThreeLunchButton = FindViewById<Button>(Resource.Id.dayThreeLunchButton);
            _dayFourLunchButton = FindViewById<Button>(Resource.Id.dayFourLunchButton);
            _dayFiveLunchButton = FindViewById<Button>(Resource.Id.dayFiveLunchButton);
            _daySixLunchButton = FindViewById<Button>(Resource.Id.daySixLunchButton);
            _daySevenLunchButton = FindViewById<Button>(Resource.Id.daySevenLunchButton);
            _dayOneDinnerEditText = FindViewById<TextInputEditText>(Resource.Id.dayOneDinnerEditText);
            _dayTwoDinnerEditText = FindViewById<TextInputEditText>(Resource.Id.dayTwoDinnerEditText);
            _dayThreeDinnerEditText = FindViewById<TextInputEditText>(Resource.Id.dayThreeDinnerEditText);
            _dayFourDinnerEditText = FindViewById<TextInputEditText>(Resource.Id.dayFourDinnerEditText);
            _dayFiveDinnerEditText = FindViewById<TextInputEditText>(Resource.Id.dayFiveDinnerEditText);
            _daySixDinnerEditText = FindViewById<TextInputEditText>(Resource.Id.daySixDinnerEditText);
            _daySevenDinnerEditText = FindViewById<TextInputEditText>(Resource.Id.daySevenDinnerEditText);
            _dayOneDinnerButton = FindViewById<Button>(Resource.Id.dayOneDinnerButton);
            _dayTwoDinnerButton = FindViewById<Button>(Resource.Id.dayTwoDinnerButton);
            _dayThreeDinnerButton = FindViewById<Button>(Resource.Id.dayThreeDinnerButton);
            _dayFourDinnerButton = FindViewById<Button>(Resource.Id.dayFourDinnerButton);
            _dayFiveDinnerButton = FindViewById<Button>(Resource.Id.dayFiveDinnerButton);
            _daySixDinnerButton = FindViewById<Button>(Resource.Id.daySixDinnerButton);
            _daySevenDinnerButton = FindViewById<Button>(Resource.Id.daySevenDinnerButton);
            _shoppingButton = FindViewById<Button>(Resource.Id.menuPlanShoppingButton);
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
            BindDateData();
            BindRecipeWithAmountData();
        }

        private void BindRecipeWithAmountData()
        {
            BindRecipeWithAmountDataFirstDayLunch();
            BindRecipeWithAmountDataFirstDayDinner();
            BindRecipeWithAmountDataSecondDayLunch();
            BindRecipeWithAmountDataSecondDayDinner();
            BindRecipeWithAmountDataThirdDayLunch();
            BindRecipeWithAmountDataThirdDayDinner();
            BindRecipeWithAmountDataFourthDayLunch();
            BindRecipeWithAmountDataFourthDayDinner();
            BindRecipeWithAmountDataFifthDayLunch();
            BindRecipeWithAmountDataFifthDayDinner();
            BindRecipeWithAmountDataSixthDayLunch();
            BindRecipeWithAmountDataSixthDayDinner();
            BindRecipeWithAmountDataSeventhDayLunch();
            BindRecipeWithAmountDataSeventhDayDinner();
        }

        private void BindRecipeWithAmountDataFirstDayLunch()
        {
            var recipeWithAmount = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Monday && r.MealDayTime == MealDayTimeEnum.Lunch);
            if (recipeWithAmount == null)
            {
                _dayOneLunchEditText.Text = DefaultTextMealCount;
                _dayOneLunchButton.Text = DefaultTextLunch;
            }
            else
            {
                _dayOneLunchEditText.Text = recipeWithAmount.NumbersOfMeals.ToString();
                _dayOneLunchButton.Text = recipeWithAmount.Recipe.Name;
            }
        }

        private void BindRecipeWithAmountDataFirstDayDinner()
        {
            var recipeFirstDayDinner = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Monday && r.MealDayTime == MealDayTimeEnum.Dinner);
            if (recipeFirstDayDinner == null)
            {
                _dayOneDinnerEditText.Text = DefaultTextMealCount;
                _dayOneDinnerButton.Text = DefaultTextDinner;
            }
            else
            {
                _dayOneDinnerEditText.Text = recipeFirstDayDinner.NumbersOfMeals.ToString();
                _dayOneDinnerButton.Text = recipeFirstDayDinner.Recipe.Name;
            }
        }

        private void BindRecipeWithAmountDataSecondDayLunch()
        {
            var recipeWithAmount = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Tuesday && r.MealDayTime == MealDayTimeEnum.Lunch);
            if (recipeWithAmount == null)
            {
                _dayTwoLunchEditText.Text = DefaultTextMealCount;
                _dayTwoLunchButton.Text = DefaultTextLunch;
            }
            else
            {
                _dayTwoLunchEditText.Text = recipeWithAmount.NumbersOfMeals.ToString();
                _dayTwoLunchButton.Text = recipeWithAmount.Recipe.Name;
            }
        }

        private void BindRecipeWithAmountDataSecondDayDinner()
        {
            var recipeWithAmount = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Tuesday && r.MealDayTime == MealDayTimeEnum.Dinner);
            if (recipeWithAmount == null)
            {
                _dayTwoDinnerEditText.Text = DefaultTextMealCount;
                _dayTwoDinnerButton.Text = DefaultTextDinner;
            }
            else
            {
                _dayTwoDinnerEditText.Text = recipeWithAmount.NumbersOfMeals.ToString();
                _dayTwoDinnerButton.Text = recipeWithAmount.Recipe.Name;
            }
        }

        private void BindRecipeWithAmountDataThirdDayLunch()
        {
            var recipeWithAmount = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Wednesday && r.MealDayTime == MealDayTimeEnum.Lunch);
            if (recipeWithAmount == null)
            {
                _dayThreeLunchEditText.Text = DefaultTextMealCount;
                _dayThreeLunchButton.Text = DefaultTextLunch;
            }
            else
            {
                _dayThreeLunchEditText.Text = recipeWithAmount.NumbersOfMeals.ToString();
                _dayThreeLunchButton.Text = recipeWithAmount.Recipe.Name;
            }
        }

        private void BindRecipeWithAmountDataThirdDayDinner()
        {
            var recipeWithAmount = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Wednesday && r.MealDayTime == MealDayTimeEnum.Dinner);
            if (recipeWithAmount == null)
            {
                _dayThreeDinnerEditText.Text = DefaultTextMealCount;
                _dayThreeDinnerButton.Text = DefaultTextDinner;
            }
            else
            {
                _dayThreeDinnerEditText.Text = recipeWithAmount.NumbersOfMeals.ToString();
                _dayThreeDinnerButton.Text = recipeWithAmount.Recipe.Name;
            }
        }

        private void BindRecipeWithAmountDataFourthDayLunch()
        {
            var recipeWithAmount = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Thursday && r.MealDayTime == MealDayTimeEnum.Lunch);
            if (recipeWithAmount == null)
            {
                _dayFourLunchEditText.Text = DefaultTextMealCount;
                _dayFourLunchButton.Text = DefaultTextLunch;
            }
            else
            {
                _dayFourLunchEditText.Text = recipeWithAmount.NumbersOfMeals.ToString();
                _dayFourLunchButton.Text = recipeWithAmount.Recipe.Name;
            }
        }

        private void BindRecipeWithAmountDataFourthDayDinner()
        {
            var recipeWithAmount = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Thursday && r.MealDayTime == MealDayTimeEnum.Dinner);
            if (recipeWithAmount == null)
            {
                _dayFourDinnerEditText.Text = DefaultTextMealCount;
                _dayFourDinnerButton.Text = DefaultTextDinner;
            }
            else
            {
                _dayFourDinnerEditText.Text = recipeWithAmount.NumbersOfMeals.ToString();
                _dayFourDinnerButton.Text = recipeWithAmount.Recipe.Name;
            }
        }

        private void BindRecipeWithAmountDataFifthDayLunch()
        {
            var recipeWithAmount = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Friday && r.MealDayTime == MealDayTimeEnum.Lunch);
            if (recipeWithAmount == null)
            {
                _dayFiveLunchEditText.Text = DefaultTextMealCount;
                _dayFiveLunchButton.Text = DefaultTextLunch;
            }
            else
            {
                _dayFiveLunchEditText.Text = recipeWithAmount.NumbersOfMeals.ToString();
                _dayFiveLunchButton.Text = recipeWithAmount.Recipe.Name;
            }
        }

        private void BindRecipeWithAmountDataFifthDayDinner()
        {
            var recipeWithAmount = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Friday && r.MealDayTime == MealDayTimeEnum.Dinner);
            if (recipeWithAmount == null)
            {
                _dayFiveDinnerEditText.Text = DefaultTextMealCount;
                _dayFiveDinnerButton.Text = DefaultTextDinner;
            }
            else
            {
                _dayFiveDinnerEditText.Text = recipeWithAmount.NumbersOfMeals.ToString();
                _dayFiveDinnerButton.Text = recipeWithAmount.Recipe.Name;
            }
        }

        private void BindRecipeWithAmountDataSixthDayLunch()
        {
            var recipeWithAmount = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Saturday && r.MealDayTime == MealDayTimeEnum.Lunch);
            if (recipeWithAmount == null)
            {
                _daySixLunchEditText.Text = DefaultTextMealCount;
                _daySixLunchButton.Text = DefaultTextLunch;
            }
            else
            {
                _daySixLunchEditText.Text = recipeWithAmount.NumbersOfMeals.ToString();
                _daySixLunchButton.Text = recipeWithAmount.Recipe.Name;
            }
        }

        private void BindRecipeWithAmountDataSixthDayDinner()
        {
            var recipeWithAmount = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Saturday && r.MealDayTime == MealDayTimeEnum.Dinner);
            if (recipeWithAmount == null)
            {
                _daySixDinnerEditText.Text = DefaultTextMealCount;
                _daySixDinnerButton.Text = DefaultTextDinner;
            }
            else
            {
                _daySixDinnerEditText.Text = recipeWithAmount.NumbersOfMeals.ToString();
                _daySixDinnerButton.Text = recipeWithAmount.Recipe.Name;
            }
        }

        private void BindRecipeWithAmountDataSeventhDayLunch()
        {
            var recipeWithAmount = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Sunday && r.MealDayTime == MealDayTimeEnum.Lunch);
            if (recipeWithAmount == null)
            {
                _daySevenLunchEditText.Text = DefaultTextMealCount;
                _daySevenLunchButton.Text = DefaultTextLunch;
            }
            else
            {
                _daySevenLunchEditText.Text = recipeWithAmount.NumbersOfMeals.ToString();
                _daySevenLunchButton.Text = recipeWithAmount.Recipe.Name;
            }
        }

        private void BindRecipeWithAmountDataSeventhDayDinner()
        {
            var recipeWithAmount = _selectedMenuPlan.RecipesWithAmounts
                .Find(r => r.DayOfWeek == DayOfWeek.Sunday && r.MealDayTime == MealDayTimeEnum.Dinner);
            if (recipeWithAmount == null)
            {
                _daySevenDinnerEditText.Text = DefaultTextMealCount;
                _daySevenDinnerButton.Text = DefaultTextDinner;
            }
            else
            {
                _daySevenDinnerEditText.Text = recipeWithAmount.NumbersOfMeals.ToString();
                _daySevenDinnerButton.Text = recipeWithAmount.Recipe.Name;
            }
        }

        private void BindDateData()
        {
            var dt = _selectedMenuPlan.StartDate;

            if (dt.Equals(new DateTime()))
            {
                _dateDisplay.Text = NoDateSelectedMessage;
            }
            else
            {
                _dateDisplay.Text = $"{dt:ddd, d/M/yyyy}";
                _dayOneTextView.Text = $"{dt:d/M}";
                _dayTwoTextView.Text = $"{dt.AddDays(1):d/M}";
                _dayThreeTextView.Text = $"{dt.AddDays(2):d/M}";
                _dayFourTextView.Text = $"{dt.AddDays(3):d/M}";
                _dayFiveTextView.Text = $"{dt.AddDays(4):d/M}";
                _daySixTextView.Text = $"{dt.AddDays(5):d/M}";
                _daySevenTextView.Text = $"{dt.AddDays(6):d/M}";
            }
        }

        private void LinkEventHandlers()
        {
            //Navigation
            _optionsButton.Click += OptionsButton_Click;
            _menusButton.Click += MenusButton_Click;
            _ingredientButton.Click += IngredientsButton_Click;
            _recipeButton.Click += RecipeButton_Click;

            //MenuPlan
            _menuPlanSearchButton.Click += MenuPlanSearchButton_Click;
            _dateSelectButton.Click += DateSelect_Click;
            _dayOneLunchButton.Click += DayOneLunchButton_Click;
            _dayOneDinnerButton.Click += DayOneDinnerButton_Click;
            _dayTwoLunchButton.Click += DayTwoLunchButton_Click;
            _dayTwoDinnerButton.Click += DayTwoDinnerButton_Click;
            _dayThreeLunchButton.Click += DayThreeLunchButton_Click;
            _dayThreeDinnerButton.Click += DayThreeDinnerButton_Click;
            _dayFourLunchButton.Click += DayFourLunchButton_Click;
            _dayFourDinnerButton.Click += DayFourDinnerButton_Click;
            _dayFiveLunchButton.Click += DayFiveLunchButton_Click;
            _dayFiveDinnerButton.Click += DayFiveDinnerButton_Click;
            _daySixLunchButton.Click += DaySixLunchButton_Click;
            _daySixDinnerButton.Click += DaySixDinnerButton_Click;
            _daySevenLunchButton.Click += DaySevenLunchButton_Click;
            _daySevenDinnerButton.Click += DaySevenDinnerButton_Click;
            _shoppingButton.Click += ShoppingButton_Click;

            //Operations
            _saveButton.Click += SaveButton_Click;
            _abortButton.Click += AbortButton_Click;
            _deleteButton.Click += DeleteButton_Click;
            _newMenuPlanButton.Click += NewMenuButton_Click;
        }


        private void OptionsButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(OptionsActivity));
            StartActivity(intent);
        }

        private void MenusButton_Click(object sender, EventArgs e)
        {
            ShowToastMessage(MenuAlreadyOpenedMessage);
        }

        private void IngredientsButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(IngredientsActivity));
            StartActivity(intent);
        }

        private void RecipeButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeActivity));
            StartActivity(intent);
        }

        private void ShowToastMessage(string text)
        {
            var toastMessage = text;
            const ToastLength duration = ToastLength.Long;
            Toast.MakeText(this, toastMessage, duration).Show();
        }

        private void MenuPlanSearchButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MenuPlanSearchActivity));
            StartActivityForResult(intent, MenuPlanSearchRequestCode);
        }

        private void DateSelect_Click(object sender, EventArgs eventArgs)
        {
            var frag = DatePickerFragment.NewInstance(delegate(DateTime time)
            {
                _selectedMenuPlan.StartDate = time;
                BindDateData();
            });
            frag.Show(SupportFragmentManager, DatePickerFragment.TAG);
        }

        private void DayOneLunchButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DayOneLunchRequestCode);
        }

        private void DayOneDinnerButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DayOneDinnerRequestCode);
        }

        private void DayTwoLunchButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DayTwoLunchRequestCode);
        }

        private void DayTwoDinnerButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DayTwoDinnerRequestCode);
        }

        private void DayThreeLunchButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DayThreeLunchRequestCode);
        }

        private void DayThreeDinnerButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DayThreeDinnerRequestCode);
        }

        private void DayFourLunchButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DayFourLunchRequestCode);
        }

        private void DayFourDinnerButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DayFourDinnerRequestCode);
        }

        private void DayFiveLunchButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DayFiveLunchRequestCode);
        }

        private void DayFiveDinnerButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DayFiveDinnerRequestCode);
        }

        private void DaySixLunchButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DaySixLunchRequestCode);
        }

        private void DaySixDinnerButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DaySixDinnerRequestCode);
        }

        private void DaySevenLunchButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DaySevenLunchRequestCode);
        }

        private void DaySevenDinnerButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeSearchActivity));
            StartActivityForResult(intent, DaySevenDinnerRequestCode);
        }

        private void ShoppingButton_Click(object sender, EventArgs e)
        {
            if (_selectedMenuPlan.Id == 0)
            {
                ShowToastMessage(MenuPlanHasFirstToBeSavedMessage);
                return;
            }

            var intent = new Intent(this, typeof(ShoppingListActivity));
            intent.PutExtra(PutExtraStringMenuPlanId, _selectedMenuPlan.Id);
            StartActivityForResult(intent, DaySevenDinnerRequestCode);
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_selectedMenuPlan.Id == 0) return;
            var toDeletingDate = _selectedMenuPlan.StartDate;
            await _menuPlanRepositoryWeb.DeleteMenuPlanById(_selectedMenuPlan.Id);
            Recreate();
            ShowToastMessage($"Das Menu vom {toDeletingDate} wurde gelöscht");
        }

        private void AbortButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.menuPlan);
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
            ShowToastMessage(ActionAbortedMessage);
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            if (VerifyUserEntries.IsMenuPlanComplete(_selectedMenuPlan))
            {
                await SaveOrUpdateMenuPlan(_menuPlanRepositoryWeb);
                await LoadMenuPlanData(_menuPlanRepositoryWeb);
                ShowToastMessage(SavedUpdatedDataMessage);
            }
            else
            {
                ShowToastMessage(FillNeededDataMessage);
            }
        }

        private async Task SaveOrUpdateMenuPlan(MenuPlanRepositoryWeb menuPlanRepositoryWeb)
        {
            if (_selectedMenuPlan.Id != 0)
                await menuPlanRepositoryWeb.UpdateMenuPlan(_selectedMenuPlan);

            else
                _selectedMenuPlan = await menuPlanRepositoryWeb.PostMenuPlan(_selectedMenuPlan);
        }


        private void NewMenuButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.menuPlan);
            _selectedMenuPlan = new MenuPlan();
            FindViews();
            BindDataFromDataToView();
            LinkEventHandlers();
        }

        private void InsertSelectedItemFirstDayLunch(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Monday && r.MealDayTime == MealDayTimeEnum.Lunch);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe, DayOfWeek = DayOfWeek.Monday, MealDayTime = MealDayTimeEnum.Lunch
                };
                if (!int.TryParse(_dayOneLunchEditText.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataFirstDayLunch();
        }

        private void InsertSelectedItemFirstDayDinner(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Monday && r.MealDayTime == MealDayTimeEnum.Dinner);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe, DayOfWeek = DayOfWeek.Monday, MealDayTime = MealDayTimeEnum.Dinner
                };
                if (!int.TryParse(_dayOneDinnerEditText.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataFirstDayDinner();
        }

        private void InsertSelectedItemSecondDayLunch(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Tuesday && r.MealDayTime == MealDayTimeEnum.Lunch);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe, DayOfWeek = DayOfWeek.Tuesday, MealDayTime = MealDayTimeEnum.Lunch
                };
                if (!int.TryParse(_dayTwoLunchEditText.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataSecondDayLunch();
        }

        private void InsertSelectedItemSecondDayDinner(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Tuesday && r.MealDayTime == MealDayTimeEnum.Dinner);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe, DayOfWeek = DayOfWeek.Tuesday, MealDayTime = MealDayTimeEnum.Dinner
                };
                if (!int.TryParse(_dayTwoDinnerButton.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataSecondDayDinner();
        }

        private void InsertSelectedItemThirdDayLunch(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Wednesday && r.MealDayTime == MealDayTimeEnum.Lunch);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe, DayOfWeek = DayOfWeek.Wednesday, MealDayTime = MealDayTimeEnum.Lunch
                };
                if (!int.TryParse(_dayThreeLunchEditText.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataThirdDayLunch();
        }

        private void InsertSelectedItemThirdDayDinner(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Wednesday && r.MealDayTime == MealDayTimeEnum.Dinner);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe,
                    DayOfWeek = DayOfWeek.Wednesday,
                    MealDayTime = MealDayTimeEnum.Dinner
                };
                if (!int.TryParse(_dayThreeDinnerEditText.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataThirdDayDinner();
        }

        private void InsertSelectedItemFourthDayLunch(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Thursday && r.MealDayTime == MealDayTimeEnum.Lunch);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe, DayOfWeek = DayOfWeek.Thursday, MealDayTime = MealDayTimeEnum.Lunch
                };
                if (!int.TryParse(_dayFourLunchEditText.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataFourthDayLunch();
        }

        private void InsertSelectedItemFourthDayDinner(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Thursday && r.MealDayTime == MealDayTimeEnum.Dinner);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe,
                    DayOfWeek = DayOfWeek.Thursday,
                    MealDayTime = MealDayTimeEnum.Dinner
                };
                if (!int.TryParse(_dayFourDinnerEditText.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataFourthDayDinner();
        }

        private void InsertSelectedItemFifthDayLunch(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Friday && r.MealDayTime == MealDayTimeEnum.Lunch);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe,
                    DayOfWeek = DayOfWeek.Friday,
                    MealDayTime = MealDayTimeEnum.Lunch
                };
                if (!int.TryParse(_dayFiveLunchEditText.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataFifthDayLunch();
        }

        private void InsertSelectedItemFifthDayDinner(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Friday && r.MealDayTime == MealDayTimeEnum.Dinner);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe,
                    DayOfWeek = DayOfWeek.Friday,
                    MealDayTime = MealDayTimeEnum.Dinner
                };
                if (!int.TryParse(_dayFiveDinnerEditText.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataFifthDayDinner();
        }

        private void InsertSelectedItemSixthDayLunch(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Saturday && r.MealDayTime == MealDayTimeEnum.Lunch);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe,
                    DayOfWeek = DayOfWeek.Saturday,
                    MealDayTime = MealDayTimeEnum.Lunch
                };
                if (!int.TryParse(_daySixLunchEditText.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataSixthDayLunch();
        }

        private void InsertSelectedItemSixthDayDinner(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Saturday && r.MealDayTime == MealDayTimeEnum.Dinner);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe,
                    DayOfWeek = DayOfWeek.Saturday,
                    MealDayTime = MealDayTimeEnum.Dinner
                };
                if (!int.TryParse(_daySixDinnerEditText.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataSixthDayDinner();
        }

        private void InsertSelectedItemSeventhDayLunch(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Sunday && r.MealDayTime == MealDayTimeEnum.Lunch);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe,
                    DayOfWeek = DayOfWeek.Sunday,
                    MealDayTime = MealDayTimeEnum.Lunch
                };
                if (!int.TryParse(_daySevenLunchEditText.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataSeventhDayLunch();
        }

        private void InsertSelectedItemSeventhDayDinner(Recipe selectedRecipe)
        {
            if (selectedRecipe == null) return;

            var itemAlreadyInMenuPlan = _selectedMenuPlan.RecipesWithAmounts.Find(r =>
                r.DayOfWeek == DayOfWeek.Sunday && r.MealDayTime == MealDayTimeEnum.Dinner);

            if (itemAlreadyInMenuPlan != null)
            {
                itemAlreadyInMenuPlan.Recipe = selectedRecipe;
            }

            else
            {
                var newRecipe = new RecipeWithAmount
                {
                    Recipe = selectedRecipe,
                    DayOfWeek = DayOfWeek.Sunday,
                    MealDayTime = MealDayTimeEnum.Dinner
                };
                if (!int.TryParse(_daySevenDinnerEditText.Text, out var i)) i = 0;
                newRecipe.NumbersOfMeals = i;
                _selectedMenuPlan.RecipesWithAmounts.Add(newRecipe);
            }

            BindRecipeWithAmountDataSeventhDayDinner();
        }
    }
}