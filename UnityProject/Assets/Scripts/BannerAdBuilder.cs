using GoogleMobileAds.Api;
using UnityEngine;

public class BannerAdBuilder : MonoBehaviour {

	private BannerView banner;
	

	void Start ()
	{
		BuildBanner();
		banner.OnAdFailedToLoad += OnFailedToLoad;
	}

	void OnDestroy ()
	{
		banner.Destroy();
	}


	private void BuildBanner ()
	{
		#if UNITY_ANDROID
			//TODO change from test ads to production
			//string adUnitId = "ca-app-pub-6843199568696689/9023160669";
			string adUnitId = "ca-app-pub-3940256099942544/6300978111";
		#elif UNITY_IOS
			string adUnitId = "";
		#else
			string adUnitId = "unexpected-platform;
		#endif

		banner = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
		AdRequest request = new AdRequest.Builder().Build();
		banner.LoadAd(request);
	}

	private void OnFailedToLoad (object sender, AdFailedToLoadEventArgs args)
	{
		Debug.LogFormat("Banner ad failed to load: {0}", args.Message);
	}
}
