package md5b62a796723a823b35e1837be7dff9bb0;


public class IngredientsDetailActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("MenuPlanerApp.IngredientsDetailActivity, MenuPlanerApp", IngredientsDetailActivity.class, __md_methods);
	}


	public IngredientsDetailActivity ()
	{
		super ();
		if (getClass () == IngredientsDetailActivity.class)
			mono.android.TypeManager.Activate ("MenuPlanerApp.IngredientsDetailActivity, MenuPlanerApp", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
