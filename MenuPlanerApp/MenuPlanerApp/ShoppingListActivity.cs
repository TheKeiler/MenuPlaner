using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using MenuPlanerApp.Adapters;
using MenuPlanerApp.Core.Repository;

namespace MenuPlanerApp
{
    [Activity(Label = "ShoppingListActivity")]
    public class ShoppingListActivity : AppCompatActivity
    {
        private MenuPlanRepositoryWeb _menuPlanRepositoryWeb;
        private int _selectedMenuPlanId;
        private ShoppingListAdapter _shoppingListAdapter;
        private RecyclerView.LayoutManager _shoppingListLayoutManager;
        private RecyclerView _shoppingListRecyclerView;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _selectedMenuPlanId = Intent.Extras.GetInt("selectedMenuPlanId");
            SetContentView(Resource.Layout
                .menuPlan_shoppingList);
            _shoppingListRecyclerView = FindViewById<RecyclerView>(Resource.Id.shoppingListRecyclerView);
            _menuPlanRepositoryWeb = new MenuPlanRepositoryWeb();

            _shoppingListLayoutManager = new LinearLayoutManager(this);
            _shoppingListRecyclerView.SetLayoutManager(_shoppingListLayoutManager);
            _shoppingListAdapter = new ShoppingListAdapter(_selectedMenuPlanId, _menuPlanRepositoryWeb);
            await _shoppingListAdapter.LoadData();
            _shoppingListRecyclerView.SetAdapter(_shoppingListAdapter);
        }
    }
}