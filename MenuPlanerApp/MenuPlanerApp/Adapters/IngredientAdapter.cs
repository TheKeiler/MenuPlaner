using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public override int ItemCount => _ingredients.Count;
        public event EventHandler<int> ItemClick;

        public async Task LoadData()
        {
            var ingredientRepository = new IngredientsRepositoryWeb();
            _ingredients = await ingredientRepository.GetAllIngredients();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is IngredientViewHolder ingredientViewHolder)
                ingredientViewHolder.IngredientNameTextView.Text = _ingredients[position].Name;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.ingredient_viewholder, parent, false);

            var ingredientViewHolder = new IngredientViewHolder(itemView, OnClick);
            return ingredientViewHolder;
        }

        private void OnClick(int position)
        {
            var ingredientId = _ingredients[position].Id;
            ItemClick?.Invoke(this, ingredientId);
        }
    }
}