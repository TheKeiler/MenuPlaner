using System;
using Android.Drm;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace MenuPlanerApp.ViewHolders
{
    public class IngredientViewHolder : RecyclerView.ViewHolder
    {
        public TextView IngredientNameTextView { get; set; }

        public IngredientViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            IngredientNameTextView = itemView.FindViewById<TextView>(Resource.Id.ingredientNameTextView);

            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }

    }
}