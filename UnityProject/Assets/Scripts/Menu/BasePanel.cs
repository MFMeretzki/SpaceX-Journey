using UnityEngine;

public class BasePanel : MonoBehaviour
{
	[SerializeField]
	protected MenuController menuController;


	protected void OnEnable ()
	{
		OptionsManager.Instance.OnLanguageChange += OnLanguageChange;
	}

	protected void OnDisable ()
	{
		OptionsManager.Instance.OnLanguageChange -= OnLanguageChange;
	}

	protected virtual void OnLanguageChange ()
	{

	}
}
