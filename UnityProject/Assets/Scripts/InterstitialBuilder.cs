﻿using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class InterstitialBuilder : MonoBehaviour {

	public InterstitialAd interAd;

	void Awake ()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Start ()
	{
		BuildInterstitial();
		interAd.OnAdClosed += DestroyInterstitial;
		interAd.OnAdFailedToLoad += OnFailedToLoad;
	}

	void OnDestroy ()
	{
		interAd.Destroy();
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
