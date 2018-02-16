using UnityEngine;

public class BasePanel : MonoBehaviour
{

	void OnEnable ()
	{
		OptionsManager.Instance.OnLanguageChange += OnLanguageChange;
	}

	void OnDisable ()
	{
		OptionsManager.Instance.OnLanguageChange -= OnLanguageChange;
	}

	protected virtual void OnLanguageChange ()
	{

	}
}
