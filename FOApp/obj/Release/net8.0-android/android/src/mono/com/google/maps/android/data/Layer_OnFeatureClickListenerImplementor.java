package mono.com.google.maps.android.data;


public class Layer_OnFeatureClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.maps.android.data.Layer.OnFeatureClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFeatureClick:(Lcom/google/maps/android/data/Feature;)V:GetOnFeatureClick_Lcom_google_maps_android_data_Feature_Handler:Android.Gms.Maps.Utils.Data.Layer/IOnFeatureClickListenerInvoker, Google.Maps.Utils.Android\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Maps.Utils.Data.Layer+IOnFeatureClickListenerImplementor, Google.Maps.Utils.Android", Layer_OnFeatureClickListenerImplementor.class, __md_methods);
	}


	public Layer_OnFeatureClickListenerImplementor ()
	{
		super ();
		if (getClass () == Layer_OnFeatureClickListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Android.Gms.Maps.Utils.Data.Layer+IOnFeatureClickListenerImplementor, Google.Maps.Utils.Android", "", this, new java.lang.Object[] {  });
		}
	}


	public void onFeatureClick (com.google.maps.android.data.Feature p0)
	{
		n_onFeatureClick (p0);
	}

	private native void n_onFeatureClick (com.google.maps.android.data.Feature p0);

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
