using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using MenuPlanerApp.Adapters;
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

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout
                .recipes_search);
            _searchView = FindViewById<SearchView>(Resource.Id.searchViewRecipe);
            _recipeRecyclerView = FindViewById<RecyclerView>(Resource.Id.recipeSearchRecyclerView);

            _recipeLayoutManager = new LinearLayoutManager(this);
            _recipeRecyclerView.SetLayoutManager(_recipeLayoutManager);
            _recipeAdapter = new RecipeAdapter();
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
            intent.SetClass(this, typeof(RecipeActivity));
            intent.PutExtra("selectedRecipeId", e);
            StartActivity(intent);
        }
    }
}