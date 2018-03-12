using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterstitialAdController : MonoBehaviour {

	private InterstitialBuilder interstitialBuilder;
	private bool showed = false;
	private float startTime;


	void Awake ()
	{
		interstitialBuilder = InterstitialBuilder.Instance;
	}

	void Start () {
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

	void OnEnable ()
	{
		interstitialBuilder.interAd.OnAdClosed += OnAdClosed;
	}

	void OnDisable ()
	{
		interstitialBuilder.interAd.OnAdClosed -= OnAdClosed;
	}


	private void OnAdClosed (object sender, EventArgs args)
	{
		SceneManager.LoadScene("Game");
	}
}
