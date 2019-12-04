﻿using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Java.Util;
using MenuPlanerApp.Adapters;
using MenuPlanerApp.Core.Model;

namespace MenuPlanerApp
{
    [Activity(Label = "IngredientsMenuActivity")]
    public class IngredientsMenuActivity : Activity
    {
        private IngredientAdapter _ingredientAdapter;
        private SearchView _searchView;
        private RecyclerView _ingredientRecyclerView;
        private RecyclerView.LayoutManager _ingredientsLayoutManager;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ingredients_Menu);
            _searchView = FindViewById<SearchView>(Resource.Id.searchView);
            _ingredientRecyclerView = FindViewById<RecyclerView>(Resource.Id.IngredientsMenuRecyclerView);

            _ingredientsLayoutManager = new LinearLayoutManager(this);
            _ingredientRecyclerView.SetLayoutManager(_ingredientsLayoutManager);
            _ingredientAdapter = new IngredientAdapter();
            await _ingredientAdapter.LoadData();
            _ingredientAdapter.ItemClick += IngredientAdapter_ItemClick;
            _ingredientRecyclerView.SetAdapter(_ingredientAdapter);

        }

        private void IngredientAdapter_ItemClick(object sender, int e)
        {
            var intent = new Intent();
            intent.SetClass(this, typeof(IngredientsActivity));
            intent.PutExtra("selectedIngredientId", e);
            StartActivity(intent);
        }

        
       
    }
}