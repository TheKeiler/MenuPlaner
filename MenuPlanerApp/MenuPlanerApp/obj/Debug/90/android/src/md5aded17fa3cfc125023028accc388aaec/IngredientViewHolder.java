package md5aded17fa3cfc125023028accc388aaec;


public class IngredientViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MenuPlanerApp.ViewHolders.IngredientViewHolder, MenuPlanerApp", IngredientViewHolder.class, __md_methods);
	}


	public IngredientViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == IngredientViewHolder.class)
			mono.android.TypeManager.Activate ("MenuPlanerApp.ViewHolders.IngredientViewHolder, MenuPlanerApp", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
