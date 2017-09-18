package md5a9123579d4730dc6ac74f4706f680d20;


public class AdBuddizHandler_AdBuddizDelegateImpl
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.purplebrain.adbuddiz.sdk.AdBuddizDelegate
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_didCacheAd:()V:GetDidCacheAdHandler:Com.Purplebrain.Adbuddiz.Sdk.IAdBuddizDelegateInvoker, AdBuddiz.Xamarin.Android\n" +
			"n_didClick:()V:GetDidClickHandler:Com.Purplebrain.Adbuddiz.Sdk.IAdBuddizDelegateInvoker, AdBuddiz.Xamarin.Android\n" +
			"n_didFailToShowAd:(Lcom/purplebrain/adbuddiz/sdk/AdBuddizError;)V:GetDidFailToShowAd_Lcom_purplebrain_adbuddiz_sdk_AdBuddizError_Handler:Com.Purplebrain.Adbuddiz.Sdk.IAdBuddizDelegateInvoker, AdBuddiz.Xamarin.Android\n" +
			"n_didHideAd:()V:GetDidHideAdHandler:Com.Purplebrain.Adbuddiz.Sdk.IAdBuddizDelegateInvoker, AdBuddiz.Xamarin.Android\n" +
			"n_didShowAd:()V:GetDidShowAdHandler:Com.Purplebrain.Adbuddiz.Sdk.IAdBuddizDelegateInvoker, AdBuddiz.Xamarin.Android\n" +
			"";
		mono.android.Runtime.register ("AdBuddiz.Xamarin.AdBuddizHandler+AdBuddizDelegateImpl, AdBuddiz.Xamarin.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AdBuddizHandler_AdBuddizDelegateImpl.class, __md_methods);
	}


	public AdBuddizHandler_AdBuddizDelegateImpl ()
	{
		super ();
		if (getClass () == AdBuddizHandler_AdBuddizDelegateImpl.class)
			mono.android.TypeManager.Activate ("AdBuddiz.Xamarin.AdBuddizHandler+AdBuddizDelegateImpl, AdBuddiz.Xamarin.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void didCacheAd ()
	{
		n_didCacheAd ();
	}

	private native void n_didCacheAd ();


	public void didClick ()
	{
		n_didClick ();
	}

	private native void n_didClick ();


	public void didFailToShowAd (com.purplebrain.adbuddiz.sdk.AdBuddizError p0)
	{
		n_didFailToShowAd (p0);
	}

	private native void n_didFailToShowAd (com.purplebrain.adbuddiz.sdk.AdBuddizError p0);


	public void didHideAd ()
	{
		n_didHideAd ();
	}

	private native void n_didHideAd ();


	public void didShowAd ()
	{
		n_didShowAd ();
	}

	private native void n_didShowAd ();

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
