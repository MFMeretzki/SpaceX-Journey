using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerAdController : MonoBehaviour {

	[SerializeField]
	private GameController gameController;
	private BannerAdBuilder bannerAdBuilder;
	private bool showBanner = false;
	private bool bannerActive = false;

	void Start ()
	{
		bannerAdBuilder = GetComponent<BannerAdBuilder>();
	}

	void OnEnable ()
	{
		gameController.GamePause += GamePauseBanner;
		gameController.GameOverEvent += GameOverBanner;
		if (showBanner) BuildBannerAd();
	}

	void OnDisable ()
	{
		gameController.GamePause -= GamePauseBanner;
		gameController.GameOverEvent -= GameOverBanner;
		bannerAdBuilder.DestroyBanner();
	}


	private void GamePauseBanner (bool paused)
	{
		if (paused)
		{
			showBanner = true;
			BuildBannerAd();
		}
		else
		{
			showBanner = false;
			DestroyBannerAd();
		}
	}

	private void GameOverBanner ()
	{
		showBanner = true;
		BuildBannerAd();
	}

	private void BuildBannerAd ()
	{
		if (!bannerActive)
		{
			bannerAdBuilder.BuildBanner();
			bannerActive = true;
		}
	}

	private void DestroyBannerAd ()
	{
		if (bannerActive)
		{
			bannerAdBuilder.DestroyBanner();
			bannerActive = false;
		}
	}
}
