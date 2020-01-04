using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Android.Support.V7.Widget;
using Android.Views;
using MenuPlanerApp.Core.Model;
using MenuPlanerApp.Core.Repository;
using MenuPlanerApp.ViewHolders;

namespace MenuPlanerApp.Adapters
{
    internal class MenuPlanAdapter : RecyclerView.Adapter
    {
        private readonly MenuPlanRepositoryWeb _menuPlanRepositoryWeb;
        private List<MenuPlan> _menuPlans;
        private List<MenuPlan> _menuPlansFull;

        public override int ItemCount => _menuPlans.Count;

        public event EventHandler<int> ItemClick;

        public MenuPlanAdapter(MenuPlanRepositoryWeb menuPlanRepositoryWeb)
        {
            _menuPlanRepositoryWeb = menuPlanRepositoryWeb;
        }

        public async Task LoadData()
        {
            _menuPlans = await _menuPlanRepositoryWeb.GetAllMenuPlan();
            _menuPlansFull = new List<MenuPlan>(_menuPlans);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is MenuPlanViewHolder menuPlanViewHolder)
                menuPlanViewHolder.MenuPlanDateTextView.Text =
                    SetMenuPlanText(position);
        }

        private string SetMenuPlanText(int position)
        {
            return $"Menüplan {_menuPlans[position].Id}, ab {_menuPlans[position].StartDate:dddd, dd MMMM yyyy}";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.menuPlan_viewHolder, parent, false);

            var menuPlanViewHolder = new MenuPlanViewHolder(itemView, OnClick);
            return menuPlanViewHolder;
        }

        private void OnClick(int position)
        {
            var menuPlanId = _menuPlans[position].Id;
            ItemClick?.Invoke(this, menuPlanId);
        }

        public void Filter(string text)
        {
            _menuPlans.Clear();
            if (string.IsNullOrEmpty(text))
            {
                _menuPlans.AddRange(_menuPlansFull);
            }
            else
            {
                text = text.ToLower();
                foreach (var item in _menuPlansFull.Where(item =>
                    item.StartDate.ToString(CultureInfo.InvariantCulture).ToLower().Contains(text)))
                    _menuPlans.Add(item);
            }

            NotifyDataSetChanged();
        }
    }
}