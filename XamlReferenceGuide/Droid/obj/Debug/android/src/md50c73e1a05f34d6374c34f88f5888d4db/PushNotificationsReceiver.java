package md50c73e1a05f34d6374c34f88f5888d4db;


public class PushNotificationsReceiver
	extends com.google.android.gms.gcm.GcmReceiver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("PushNotification.Plugin.PushNotificationsReceiver, CommonCore.XamlReferenceGuide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PushNotificationsReceiver.class, __md_methods);
	}


	public PushNotificationsReceiver ()
	{
		super ();
		if (getClass () == PushNotificationsReceiver.class)
			mono.android.TypeManager.Activate ("PushNotification.Plugin.PushNotificationsReceiver, CommonCore.XamlReferenceGuide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
