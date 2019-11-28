using Android;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace MenuPlanerApp.ViewHolders
{
    public class IngredientViewHolder : RecyclerView.ViewHolder
    {
        public ImageView IngredientImageView { get; set; }
        public TextView IngredientNameTextView { get; set; }

        public IngredientViewHolder(View itemView) : base(itemView)
        {
            IngredientImageView = itemView.FindViewById<ImageView>(Resource.Id.ingredientImageView);
            IngredientNameTextView = itemView.FindViewById<TextView>(Resource.Id.ingredientNameTextView);
        }

    }
}