package md59dbfbaa2fc42f1e13e1af93045bf62b3;


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
		mono.android.Runtime.register ("PushNotification.Plugin.PushNotificationsReceiver, realmDemo.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PushNotificationsReceiver.class, __md_methods);
	}


	public PushNotificationsReceiver () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PushNotificationsReceiver.class)
			mono.android.TypeManager.Activate ("PushNotification.Plugin.PushNotificationsReceiver, realmDemo.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
