package md53e0db828173852ef3b69505e6efd1e67;


public class CorePickerRenderer_PickerListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler:Android.Views.View/IOnClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Forms.CommonCore.CorePickerRenderer+PickerListener, tabbedReference.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CorePickerRenderer_PickerListener.class, __md_methods);
	}


	public CorePickerRenderer_PickerListener ()
	{
		super ();
		if (getClass () == CorePickerRenderer_PickerListener.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.CommonCore.CorePickerRenderer+PickerListener, tabbedReference.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onClick (android.view.View p0)
	{
		n_onClick (p0);
	}

	private native void n_onClick (android.view.View p0);

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
