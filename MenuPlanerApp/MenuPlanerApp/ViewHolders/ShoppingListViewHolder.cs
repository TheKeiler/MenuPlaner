using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace MenuPlanerApp.ViewHolders
{
    public class ShoppingListViewHolder : RecyclerView.ViewHolder
    {
        public ShoppingListViewHolder(View itemView) : base(itemView)
        {
            IngredientWithAmountNameTextView = itemView.FindViewById<TextView>(Resource.Id.ingredientWithAmountNameTextView);
        }
        public TextView IngredientWithAmountNameTextView { get; }
    }
}