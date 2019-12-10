using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using MenuPlanerApp.Core.Model;
using Object = Java.Lang.Object;

namespace MenuPlanerApp.Adapters
{
    public class IngredientsWithAmountListViewAdapter : BaseAdapter
    {
        private readonly Activity _activity;
        private readonly List<IngredientWithAmount> _ingredients;

        public IngredientsWithAmountListViewAdapter(Activity activity, List<IngredientWithAmount> ingredients)
        {
            this._activity = activity;
            this._ingredients = ingredients;
        }

        public override int Count => _ingredients.Count;

        public override Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return _ingredients[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.ingredientsWithAmount_viewholder, parent, false);

            var ingredientNameTextView = view.FindViewById<TextView>(Resource.Id.ingredientWithAmountTextView);
            SetTextInTextViewViewHolder(position, ingredientNameTextView);
            return view;
        }

        private void SetTextInTextViewViewHolder(int position, TextView ingredientNameTextView)
        {
            ingredientNameTextView.Text = string.IsNullOrEmpty(_ingredients[position].Ingredient.Description) ? $"{_ingredients[position].Ingredient.Name}, {_ingredients[position].Amount}{_ingredients[position].Ingredient.ReferenceUnit}" : $"{_ingredients[position].Ingredient.Name}, {_ingredients[position].Ingredient.Description}, {_ingredients[position].Amount}{_ingredients[position].Ingredient.ReferenceUnit}";
        }
    }
}