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
        private List<Ingredient> _ingredients;
        public event EventHandler<int> ItemClick;

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
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.ingredient_viewholder, parent, false);

            IngredientViewHolder ingredientViewHolder = new IngredientViewHolder(itemView, OnClick);
            return ingredientViewHolder;
        }

        private void OnClick(int position)
        {
            var ingredientId = _ingredients[position].IngrediantId;
            ItemClick?.Invoke(this, ingredientId);
        }
    }
}