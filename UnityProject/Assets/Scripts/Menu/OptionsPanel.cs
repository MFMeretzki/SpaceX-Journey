using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel: BasePanel
{

	private const string TITLE_ID = "main_menu_options";

	[SerializeField]
	private Text titleText;


	void Start ()
	{
		OnLanguageChange();
	}

	protected override void OnLanguageChange ()
	{
		titleText.text = Localization.Instance.GetText(TITLE_ID);
	}
}
