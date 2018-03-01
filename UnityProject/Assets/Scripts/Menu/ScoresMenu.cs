using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoresMenu : BasePanel
{
	private const string TITLE_ID = "scores_highscores";
    private const string RETURN_ID = "scores_menu_return";


    [SerializeField]
	private Text titleText;
	[SerializeField]
	private Text[] playersText;
	[SerializeField]
	private Text[] pointsText;
    [SerializeField]
    private Text returnText;


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
        returnText.text = Localization.Instance.GetText(RETURN_ID);
    }
}
