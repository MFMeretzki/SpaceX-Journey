using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BasePanel
{

	private const string PLAY_ID = "main_menu_play";
	private const string HIGHSCORES_ID = "main_menu_highscores";
	private const string OPTIONS_ID = "main_menu_options";

	[SerializeField]
	private Text playText;
	[SerializeField]
	private Text highscoresText;
	[SerializeField]
	private Text optionsText;


	void Start ()
	{
		OnLanguageChange();
	}

	
	protected override void OnLanguageChange ()
	{
		playText.text = Localization.Instance.GetText(PLAY_ID);
		highscoresText.text = Localization.Instance.GetText(HIGHSCORES_ID);
		optionsText.text = Localization.Instance.GetText(OPTIONS_ID);
	}
}