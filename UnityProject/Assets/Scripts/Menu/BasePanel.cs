using UnityEngine;

public class BasePanel : MonoBehaviour
{
	[SerializeField]
	protected MenuController menuController;


	protected void OnEnable ()
	{
		OptionsManager.Instance.LanguageChange += OnLanguageChange;
	}

	protected void OnDisable ()
	{
		OptionsManager.Instance.LanguageChange -= OnLanguageChange;
	}

	protected virtual void OnLanguageChange ()
	{

	}
}
