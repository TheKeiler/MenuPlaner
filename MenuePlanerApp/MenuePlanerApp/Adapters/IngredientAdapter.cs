using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using MenuPlanerApp.Core.Model;
using MenuPlanerApp.Core.Repository;

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
            throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }
    }
}