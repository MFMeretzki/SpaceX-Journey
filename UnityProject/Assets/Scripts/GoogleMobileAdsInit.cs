using GoogleMobileAds.Api;
using UnityEngine;

public class GoogleMobileAdsInit : MonoBehaviour {

	
	void Start ()
	{
		#if UNITY_ANDROID
			string appId = "ca-app-pub-6843199568696689~7856945894";
		#elif UNITY_IOS
			string appId = "";
		#else
			string appId = "unexpected-platform;
		#endif

		MobileAds.Initialize(appId);
	}

}
