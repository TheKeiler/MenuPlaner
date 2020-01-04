using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IngredientsRepositoryWeb _ingredientsRepositoryWeb;
        private List<Ingredient> _ingredients;
        private List<Ingredient> _ingredientsFull;

        public IngredientAdapter(IngredientsRepositoryWeb ingredientsRepositoryWeb)
        {
            _ingredientsRepositoryWeb = ingredientsRepositoryWeb;
        }

        public override int ItemCount => _ingredients.Count;

        public event EventHandler<int> ItemClick;

        public async Task LoadData()
        {
            _ingredients = await _ingredientsRepositoryWeb.GetAllIngredients();
            _ingredientsFull = new List<Ingredient>(_ingredients);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is IngredientViewHolder ingredientViewHolder)
                ingredientViewHolder.IngredientNameTextView.Text =
                    SetIngredientsText(position);
        }

        private string SetIngredientsText(int position)
        {
            return string.IsNullOrEmpty(_ingredients[position].Description)
                ? _ingredients[position].Name
                : $"{_ingredients[position].Name}, {_ingredients[position].Description}";
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

        public void Filter(string text)
        {
            _ingredients.Clear();
            if (string.IsNullOrEmpty(text))
            {
                _ingredients.AddRange(_ingredientsFull);
            }
            else
            {
                text = text.ToLower();
                foreach (var item in _ingredientsFull.Where(item => item.Name.ToLower().Contains(text)))
                    _ingredients.Add(item);
            }

            NotifyDataSetChanged();
        }
    }
}