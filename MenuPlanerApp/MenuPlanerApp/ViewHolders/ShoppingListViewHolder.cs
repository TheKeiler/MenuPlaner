using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace MenuPlanerApp.ViewHolders
{
    public class ShoppingListViewHolder : RecyclerView.ViewHolder
    {
        public ShoppingListViewHolder(View itemView) : base(itemView)
        {
            IngredientWithAmountNameTextView =
                itemView.FindViewById<TextView>(Resource.Id.ingredientWithAmountNameTextView);
        }

        public TextView IngredientWithAmountNameTextView { get; }
    }
}