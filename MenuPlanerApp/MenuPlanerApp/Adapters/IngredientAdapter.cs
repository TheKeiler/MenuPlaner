using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using MenuPlanerApp.Core.Model;
using MenuPlanerApp.Core.Repository;
using MenuPlanerApp.ViewHolders;

namespace MenuPlanerApp.Adapters
{
    public class IngredientAdapter : RecyclerView.Adapter
    {
        private readonly List<Ingredient> _ingredients;

        public IngredientAdapter()
        {
            var ingredientRepository = new IngredientsRepository();
            _ingredients = ingredientRepository.GetAllIngredients();
        }

        public override int ItemCount => _ingredients.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is IngredientViewHolder ingredientViewHolder)
            {
                ingredientViewHolder.IngredientNameTextView.Text = _ingredients[position].Name;

                /*var imageBitmap = ImageHelper.GetImageBitmapFromUrl(_ingredients[position].ImageThumbnalUrl);
                ingredientViewHolder.IngredientImageView.SetImageBitmap(imageBitmap);*/
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.ingredient_viewholder, parent, false);

            IngredientViewHolder ingredientViewHolder = new IngredientViewHolder(itemView);
            return ingredientViewHolder;
        }
    }
}