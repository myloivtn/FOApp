package mono.com.google.maps.android.clustering;


public class ClusterManager_OnClusterItemClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.maps.android.clustering.ClusterManager.OnClusterItemClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onClusterItemClick:(Lcom/google/maps/android/clustering/ClusterItem;)Z:GetOnClusterItemClick_Lcom_google_maps_android_clustering_ClusterItem_Handler:Android.Gms.Maps.Utils.Clustering.ClusterManager/IOnClusterItemClickListenerInvoker, Google.Maps.Utils.Android\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Maps.Utils.Clustering.ClusterManager+IOnClusterItemClickListenerImplementor, Google.Maps.Utils.Android", ClusterManager_OnClusterItemClickListenerImplementor.class, __md_methods);
	}


	public ClusterManager_OnClusterItemClickListenerImplementor ()
	{
		super ();
		if (getClass () == ClusterManager_OnClusterItemClickListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Android.Gms.Maps.Utils.Clustering.ClusterManager+IOnClusterItemClickListenerImplementor, Google.Maps.Utils.Android", "", this, new java.lang.Object[] {  });
		}
	}


	public boolean onClusterItemClick (com.google.maps.android.clustering.ClusterItem p0)
	{
		return n_onClusterItemClick (p0);
	}

	private native boolean n_onClusterItemClick (com.google.maps.android.clustering.ClusterItem p0);

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
