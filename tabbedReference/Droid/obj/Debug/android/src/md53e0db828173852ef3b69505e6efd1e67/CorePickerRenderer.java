package md53e0db828173852ef3b69505e6efd1e67;


public class CorePickerRenderer
	extends md5b60ffeb829f638581ab2bb9b1a7f4f3f.ViewRenderer_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFocusChanged:(ZILandroid/graphics/Rect;)V:GetOnFocusChanged_ZILandroid_graphics_Rect_Handler\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Forms.CommonCore.CorePickerRenderer, tabbedReference.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CorePickerRenderer.class, __md_methods);
	}


	public CorePickerRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CorePickerRenderer.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.CommonCore.CorePickerRenderer, tabbedReference.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public CorePickerRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CorePickerRenderer.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.CommonCore.CorePickerRenderer, tabbedReference.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public CorePickerRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CorePickerRenderer.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.CommonCore.CorePickerRenderer, tabbedReference.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onFocusChanged (boolean p0, int p1, android.graphics.Rect p2)
	{
		n_onFocusChanged (p0, p1, p2);
	}

	private native void n_onFocusChanged (boolean p0, int p1, android.graphics.Rect p2);

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
