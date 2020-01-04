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
    [Activity(Label = "@string/app_name")]
    public class IngredientsSearchActivity : AppCompatActivity
    {
        private IngredientAdapter _ingredientAdapter;
        private RecyclerView _ingredientRecyclerView;
        private RecyclerView.LayoutManager _ingredientsLayoutManager;
        private IngredientsRepositoryWeb _ingredientsRepositoryWeb;
        private SearchView _searchView;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout
                .ingredient_search);
            _searchView = FindViewById<SearchView>(Resource.Id.searchViewIngredient);
            _ingredientRecyclerView = FindViewById<RecyclerView>(Resource.Id.IngredientsSearchRecyclerView);
            _ingredientsRepositoryWeb = new IngredientsRepositoryWeb();
            _ingredientsLayoutManager = new LinearLayoutManager(this);
            _ingredientRecyclerView.SetLayoutManager(_ingredientsLayoutManager);
            _ingredientAdapter = new IngredientAdapter(_ingredientsRepositoryWeb);
            await _ingredientAdapter.LoadData();
            _ingredientAdapter.ItemClick += IngredientAdapter_ItemClick;
            _ingredientRecyclerView.SetAdapter(_ingredientAdapter);
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
            _ingredientAdapter.Filter(e.NewText);
        }

        private void IngredientAdapter_ItemClick(object sender, int e)
        {
            var intent = new Intent();
            intent.PutExtra("selectedIngredientId", e);

            intent.SetClass(this,
                CallingActivity.ClassName.EndsWith("IngredientsActivity")
                    ? typeof(IngredientsActivity)
                    : typeof(RecipeActivity));

            SetResult(Result.Ok, intent);
            Finish();
        }
    }
}