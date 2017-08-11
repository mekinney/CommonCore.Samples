package md592432b11e548b3b476288f0cf9b4e62c;


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
		mono.android.Runtime.register ("PushNotification.Plugin.PushNotificationsReceiver, sqliteDemo.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PushNotificationsReceiver.class, __md_methods);
	}


	public PushNotificationsReceiver () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PushNotificationsReceiver.class)
			mono.android.TypeManager.Activate ("PushNotification.Plugin.PushNotificationsReceiver, sqliteDemo.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
