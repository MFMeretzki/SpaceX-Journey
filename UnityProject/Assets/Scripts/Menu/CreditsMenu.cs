using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : BasePanel
{

	private const string TITLE_ID = "credits";
	private const string CREDITS_ID = "credits_text";
	private const string RETURN_ID = "options_menu_return";

	[SerializeField]
	private Text titleText;
	[SerializeField]
	private Text creditsText;
	[SerializeField]
	private Text returnText;

	void Start () {
		OnLanguageChange();
	}


	public void ReturnButtonClick ()
	{
		menuController.SwitchPanel(0);
	}


	protected override void OnLanguageChange ()
	{
		titleText.text = Localization.Instance.GetText(TITLE_ID);
		creditsText.text = Localization.Instance.GetText(CREDITS_ID);
		returnText.text = Localization.Instance.GetText(RETURN_ID);
	}

}
