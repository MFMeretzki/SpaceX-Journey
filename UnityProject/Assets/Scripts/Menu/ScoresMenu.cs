﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoresMenu : BasePanel
{
	private const string TITLE_ID = "scores_highscores";
	private const string PLAYER_ID = "scores_player";
	private const string POINTS_ID = "scores_points";


	[SerializeField]
	private Text titleText;
	[SerializeField]
	private Text playerTitleText;
	[SerializeField]
	private Text pointsTitleText;
	[SerializeField]
	private Text[] playersText;
	[SerializeField]
	private Text[] pointsText;
	

	void Start ()
	{
		OnLanguageChange();

		List<Tuple<string, int>> scoresList = GameSettings.Instance.GetScores();
		for (int i = 0; i < scoresList.Count; ++i)
		{
			playersText[i].text = scoresList[i].item1;
			playersText[i].gameObject.SetActive(true);
			pointsText[i].text = scoresList[i].item2.ToString();
			pointsText[i].gameObject.SetActive(true);
		}
	}


	public void ReturnButtonClick ()
	{
		menuController.SwitchPanel(0);
	}


	protected override void OnLanguageChange ()
	{
		titleText.text = Localization.Instance.GetText(TITLE_ID);
		playerTitleText.text = Localization.Instance.GetText(PLAYER_ID);
		pointsTitleText.text = Localization.Instance.GetText(POINTS_ID);
	}
}
