using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Support.V7.Widget;
using Android.Views;
using MenuPlanerApp.Core.Model;
using MenuPlanerApp.Core.Repository;
using MenuPlanerApp.Core.Utility;
using MenuPlanerApp.ViewHolders;

namespace MenuPlanerApp.Adapters
{
    internal class ShoppingListAdapter : RecyclerView.Adapter
    {
        private readonly int _selectedMenuPlanId;
        private List<IngredientWithAmount> _ingredientsList;
        private MenuPlan _menuPlan;

        public ShoppingListAdapter(int selectedMenuPlanId)
        {
            _selectedMenuPlanId = selectedMenuPlanId;
        }

        public override int ItemCount => _ingredientsList.Count;

        public async Task LoadData()
        {
            var menuPlanRepositoryWeb = new MenuPlanRepositoryWeb();
            _menuPlan = await menuPlanRepositoryWeb.GetMenuPlanById(_selectedMenuPlanId);
            if (_menuPlan != null)
            {
                var shoppingListCreator = new ShoppingListCreator();
                _ingredientsList = shoppingListCreator.GenerateShoppingListFromMenuPlan(_menuPlan);
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is ShoppingListViewHolder shoppingListViewHolder)
                shoppingListViewHolder.IngredientWithAmountNameTextView.Text =
                    SetShoppingListText(position);
        }

        private string SetShoppingListText(int position)
        {
            return !string.IsNullOrEmpty(_ingredientsList[position].Ingredient.Description)
                ? $"{_ingredientsList[position].Amount}{_ingredientsList[position].Ingredient.ReferenceUnit} {_ingredientsList[position].Ingredient.Name}, {_ingredientsList[position].Ingredient.Description}"
                : $"{_ingredientsList[position].Amount}{_ingredientsList[position].Ingredient.ReferenceUnit} {_ingredientsList[position].Ingredient.Name}";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.menuPlan_shoppingList_Viewholder, parent, false);

            var shoppingListViewHolder = new ShoppingListViewHolder(itemView);
            return shoppingListViewHolder;
        }
    }
}