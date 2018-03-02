using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterstitialAdController : MonoBehaviour {

	private InterstitialBuilder interstitialBuilder;
	private bool showed = false;
	private float startTime;

	void Start () {
		interstitialBuilder = GameObject.Find("InterstitialAd").GetComponent<InterstitialBuilder>();
		interstitialBuilder.interAd.OnAdClosed += OnAdClosed;

		startTime = Time.time;
	}

	void Update ()
	{
		#if UNITY_EDITOR
			OnAdClosed(this, null);
		#else
		if (!showed)
		{
			showed = interstitialBuilder.ShowInterstitial();
		}
		if (!showed && startTime < Time.time)
		{
			OnAdClosed(this, null);
		}
		#endif
	}

	void OnDisable ()
	{
		interstitialBuilder.interAd.OnAdClosed -= OnAdClosed;
	}


	private void OnAdClosed (object sender, EventArgs args)
	{
		if (!showed) interstitialBuilder.BuildNewInterstitial();
		SceneManager.LoadScene("Game");
	}
}
