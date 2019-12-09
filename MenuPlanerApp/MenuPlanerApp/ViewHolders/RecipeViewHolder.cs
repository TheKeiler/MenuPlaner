using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace MenuPlanerApp.ViewHolders
{
    public class RecipeViewHolder : RecyclerView.ViewHolder
    {
        public RecipeViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            RecipeNameTextView = itemView.FindViewById<TextView>(Resource.Id.recipeNameTextView);

            itemView.Click += (sender, e) => listener(LayoutPosition);
        }

        public TextView RecipeNameTextView { get; }
    }
}