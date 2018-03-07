using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class InterstitialBuilder : MonoBehaviour {

	public InterstitialAd interAd;

	private static InterstitialBuilder instance;
	public static InterstitialBuilder Instance { get { return instance; } }

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}
	}

	void Start ()
	{
		
	}

	void OnDestroy ()
	{
		if (interAd != null) interAd.Destroy();
	}

	void OnEnable ()
	{
		BuildInterstitial();
	}

	void OnDisable ()
	{
		if (interAd != null) interAd.Destroy();
	}


	public bool ShowInterstitial ()
	{
		if (interAd.IsLoaded())
		{
			interAd.Show();
			return true;
		}
		else return false;
	}

	public void BuildNewInterstitial ()
	{
		DestroyInterstitial(this, null);
	}


	private void BuildInterstitial ()
	{
		#if UNITY_ANDROID
			//TODO change from test ads to production
			//string adUnitId = "ca-app-pub-6843199568696689/8160607342";
			string adUnitId = "ca-app-pub-3940256099942544/1033173712";
		#elif UNITY_IOS
			string adUnitId = "";
		#else
			string adUnitId = "unexpected-platform;
		#endif

		interAd = new InterstitialAd(adUnitId);
		interAd.OnAdClosed += DestroyInterstitial;
		interAd.OnAdFailedToLoad += OnFailedToLoad;
		AdRequest request = new AdRequest.Builder().Build();
		interAd.LoadAd(request);
	}

	private void DestroyInterstitial (object sender, EventArgs args)
	{
		interAd.Destroy();
		BuildInterstitial();
	}

	private void OnFailedToLoad (object sender, AdFailedToLoadEventArgs args)
	{
		Debug.LogFormat("Interstitial ad failed to load: {0}", args.Message);
	}
}
