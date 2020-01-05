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
    public class MenuPlanSearchActivity : AppCompatActivity
    {
        private MenuPlanAdapter _menuPlanAdapter;
        private RecyclerView.LayoutManager _menuPlanLayoutManager;
        private RecyclerView _menuPlanRecyclerView;
        private MenuPlanRepositoryWeb _menuPlanRepositoryWeb;
        private SearchView _searchView;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout
                .menuPlans_search);
            _searchView = FindViewById<SearchView>(Resource.Id.searchViewMenuPlans);
            _menuPlanRecyclerView = FindViewById<RecyclerView>(Resource.Id.menuPlanSearchRecyclerView);
            _menuPlanRepositoryWeb = new MenuPlanRepositoryWeb();
            _menuPlanLayoutManager = new LinearLayoutManager(this);
            _menuPlanRecyclerView.SetLayoutManager(_menuPlanLayoutManager);
            _menuPlanAdapter = new MenuPlanAdapter(_menuPlanRepositoryWeb);
            await _menuPlanAdapter.LoadData();
            _menuPlanAdapter.ItemClick += RecipeAdapter_ItemClick;
            _menuPlanRecyclerView.SetAdapter(_menuPlanAdapter);
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
            _menuPlanAdapter.Filter(e.NewText);
        }

        private void RecipeAdapter_ItemClick(object sender, int e)
        {
            var intent = new Intent();
            intent.PutExtra("selectedMenuPlanId", e);

            intent.SetClass(this,
                typeof(MenuPlanActivity));

            SetResult(Result.Ok, intent);
            Finish();
        }
    }
}