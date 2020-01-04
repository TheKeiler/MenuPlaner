using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Support.V7.Widget;
using Android.Views;
using MenuPlanerApp.Core.Model;
using MenuPlanerApp.Core.Repository;
using MenuPlanerApp.Core.Utility;
using MenuPlanerApp.ViewHolders;

namespace MenuPlanerApp.Adapters
{
    public class RecipeAdapter : RecyclerView.Adapter
    {
        private readonly RecipeRepositoryWeb _recipeRepositoryWeb;
        private readonly UserOptionsRepositoryWeb _userOptionsRepositoryWeb;
        private List<Recipe> _recipes;
        private List<Recipe> _recipesFull;

        public RecipeAdapter(RecipeRepositoryWeb recipeRepositoryWeb, UserOptionsRepositoryWeb userOptionsRepositoryWeb)
        {
            _recipeRepositoryWeb = recipeRepositoryWeb;
            _userOptionsRepositoryWeb = userOptionsRepositoryWeb;
        }

        public override int ItemCount => _recipes.Count;

        public event EventHandler<int> ItemClick;

        public async Task LoadData()
        {
            _recipes = await _recipeRepositoryWeb.GetAllRecipes();
            await FilterRecipesFromOptions();
            _recipesFull = new List<Recipe>(_recipes);
        }

        private async Task FilterRecipesFromOptions()
        {
            var recipeFilter = new RecipeFilter(_userOptionsRepositoryWeb);
            var filteredList = await recipeFilter.FilterRecipes(_recipes);
            _recipes = filteredList;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is RecipeViewHolder recipeViewHolder)
                recipeViewHolder.RecipeNameTextView.Text =
                    SetRecipesText(position);
        }

        private string SetRecipesText(int position)
        {
            return string.IsNullOrEmpty(_recipes[position].Description)
                ? _recipes[position].Name
                : $"{_recipes[position].Name}, {_recipes[position].Description}";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.recipe_viewholder, parent, false);

            var recipesViewHolder = new RecipeViewHolder(itemView, OnClick);
            return recipesViewHolder;
        }

        private void OnClick(int position)
        {
            var recipeId = _recipes[position].Id;
            ItemClick?.Invoke(this, recipeId);
        }

        public void Filter(string text)
        {
            _recipes.Clear();
            if (string.IsNullOrEmpty(text))
            {
                _recipes.AddRange(_recipesFull);
            }
            else
            {
                text = text.ToLower();
                foreach (var item in _recipesFull.Where(item => item.Name.ToLower().Contains(text)))
                    _recipes.Add(item);
            }

            NotifyDataSetChanged();
        }
    }
}