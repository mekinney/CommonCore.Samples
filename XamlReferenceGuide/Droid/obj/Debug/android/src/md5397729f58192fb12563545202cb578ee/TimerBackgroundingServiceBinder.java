package md5397729f58192fb12563545202cb578ee;


public class TimerBackgroundingServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Xamarin.Forms.CommonCore.TimerBackgroundingServiceBinder, CommonCore.XamlReferenceGuide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TimerBackgroundingServiceBinder.class, __md_methods);
	}


	public TimerBackgroundingServiceBinder ()
	{
		super ();
		if (getClass () == TimerBackgroundingServiceBinder.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.CommonCore.TimerBackgroundingServiceBinder, CommonCore.XamlReferenceGuide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public TimerBackgroundingServiceBinder (md5397729f58192fb12563545202cb578ee.TimerBackgroundService p0)
	{
		super ();
		if (getClass () == TimerBackgroundingServiceBinder.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.CommonCore.TimerBackgroundingServiceBinder, CommonCore.XamlReferenceGuide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Xamarin.Forms.CommonCore.TimerBackgroundService, CommonCore.XamlReferenceGuide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
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
