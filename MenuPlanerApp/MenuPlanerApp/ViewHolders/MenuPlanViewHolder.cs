using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace MenuPlanerApp.ViewHolders
{
    public class MenuPlanViewHolder : RecyclerView.ViewHolder
    {
        public MenuPlanViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            MenuPlanDateTextView = itemView.FindViewById<TextView>(Resource.Id.menuPlanDateTextView);

            itemView.Click += (sender, e) => listener(LayoutPosition);
        }

        public TextView MenuPlanDateTextView { get; }
    }
}