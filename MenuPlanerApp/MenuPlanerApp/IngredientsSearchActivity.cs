using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Widget;
using MenuPlanerApp.Adapters;
using SearchView = Android.Widget.SearchView;

namespace MenuPlanerApp
{
    [Activity(Label = "IngredientsSearchActivity")]
    public class IngredientsSearchActivity : Activity
    {
        private IngredientAdapter _ingredientAdapter;
        private RecyclerView _ingredientRecyclerView;
        private RecyclerView.LayoutManager _ingredientsLayoutManager;
        private SearchView _searchView;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout
                .ingredient_search);
            _searchView = FindViewById<Android.Widget.SearchView>(Resource.Id.searchView);
            _ingredientRecyclerView = FindViewById<RecyclerView>(Resource.Id.IngredientsSearchRecyclerView);

            _ingredientsLayoutManager = new LinearLayoutManager(this);
            _ingredientRecyclerView.SetLayoutManager(_ingredientsLayoutManager);
            _ingredientAdapter = new IngredientAdapter();
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
            intent.SetClass(this, typeof(IngredientsActivity));
            intent.PutExtra("selectedIngredientId", e);
            StartActivity(intent);
        }
    }
}