using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using MenuPlanerApp.Adapters;
using MenuPlanerApp.Core.Repository;
using SearchView = Android.Widget.SearchView;

namespace MenuPlanerApp
{
    [Activity(Label = "RecipeSearchActivity")]
    public class RecipeSearchActivity : AppCompatActivity
    {
        private RecipeAdapter _recipeAdapter;
        private RecyclerView.LayoutManager _recipeLayoutManager;
        private RecyclerView _recipeRecyclerView;
        private SearchView _searchView;
        private RecipeRepositoryWeb _recipeRepositoryWeb;
        private UserOptionsRepositoryWeb _userOptionsRepositoryWeb;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout
                .recipes_search);
            _searchView = FindViewById<SearchView>(Resource.Id.searchViewRecipe);
            _recipeRecyclerView = FindViewById<RecyclerView>(Resource.Id.recipeSearchRecyclerView);
            _recipeRepositoryWeb = new RecipeRepositoryWeb();
            _userOptionsRepositoryWeb = new UserOptionsRepositoryWeb();
            _recipeLayoutManager = new LinearLayoutManager(this);
            _recipeRecyclerView.SetLayoutManager(_recipeLayoutManager);
            _recipeAdapter = new RecipeAdapter(_recipeRepositoryWeb , _userOptionsRepositoryWeb);
            await _recipeAdapter.LoadData();
            _recipeAdapter.ItemClick += RecipeAdapter_ItemClick;
            _recipeRecyclerView.SetAdapter(_recipeAdapter);
            SetupSearchView();
        }

        private void SetupSearchView()
        {
            _searchView.SetIconifiedByDefault(false);
            _searchView.SubmitButtonEnabled = true;
            _searchView.QueryTextChange += SearchViewOnQueryTextChange;
        }

        private void SearchViewOnQueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            _recipeAdapter.Filter(e.NewText);
        }

        private void RecipeAdapter_ItemClick(object sender, int e)
        {
            var intent = new Intent();
            intent.PutExtra("selectedRecipeId", e);

            intent.SetClass(this,
                CallingActivity.ClassName.EndsWith("RecipeActivity")
                    ? typeof(RecipeActivity)
                    : typeof(MenuPlanActivity));

            SetResult(Result.Ok, intent);
            Finish();
        }
    }
}