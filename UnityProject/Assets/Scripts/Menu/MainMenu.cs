using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : BasePanel
{

	private const string PLAY_ID = "main_menu_play";
	private const string HIGHSCORES_ID = "main_menu_highscores";
	private const string OPTIONS_ID = "main_menu_options";
    private const string INFO_ID = "main_menu_info";
    private const string CREDITS_ID = "main_menu_credits";
	private const string EXIT_ID = "main_menu_exit";

	[SerializeField]
	private Text playText;
	[SerializeField]
	private Text highscoresText;
	[SerializeField]
	private Text optionsText;
    [SerializeField]
    private Text infoText;
    [SerializeField]
	private Text creditsText;
	[SerializeField]
	private Text exitText;


	void Start ()
	{
		OnLanguageChange();
		BannerAdBuilder bannerAdBuilder = GameObject.Find("BannerAd").GetComponent<BannerAdBuilder>();
		bannerAdBuilder.BuildBanner();
	}


	public void PlayButtonClick ()
	{
		SceneManager.LoadScene("InterstitialAd");
	}

	public void ScoresButtonClick ()
	{
		menuController.SwitchPanel(2);
	}

	public void OptionsButtonClick ()
	{
		menuController.SwitchPanel(1);
    }

    public void InfoButtonClick ()
    {
        menuController.SwitchPanel(4);
    }

    public void CreditsButtonClick ()
	{
		menuController.SwitchPanel(3);
	}

	public void ExitButtonClick ()
	{
		Application.Quit();
	}

	protected override void OnLanguageChange ()
	{
		playText.text = Localization.Instance.GetText(PLAY_ID);
		highscoresText.text = Localization.Instance.GetText(HIGHSCORES_ID);
		optionsText.text = Localization.Instance.GetText(OPTIONS_ID);
        infoText.text = Localization.Instance.GetText(INFO_ID);
        creditsText.text = Localization.Instance.GetText(CREDITS_ID);
		exitText.text = Localization.Instance.GetText(EXIT_ID);
	}

}
