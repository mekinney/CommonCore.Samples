package md592432b11e548b3b476288f0cf9b4e62c;


public class PushNotificationInstanceIDListenerService
	extends com.google.android.gms.iid.InstanceIDListenerService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTokenRefresh:()V:GetOnTokenRefreshHandler\n" +
			"";
		mono.android.Runtime.register ("PushNotification.Plugin.PushNotificationInstanceIDListenerService, sqliteDemo.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PushNotificationInstanceIDListenerService.class, __md_methods);
	}


	public PushNotificationInstanceIDListenerService () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PushNotificationInstanceIDListenerService.class)
			mono.android.TypeManager.Activate ("PushNotification.Plugin.PushNotificationInstanceIDListenerService, sqliteDemo.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onTokenRefresh ()
	{
		n_onTokenRefresh ();
	}

	private native void n_onTokenRefresh ();

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
