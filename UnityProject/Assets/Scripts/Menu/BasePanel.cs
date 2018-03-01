using UnityEngine;

public class BasePanel : MonoBehaviour
{
	[SerializeField]
	protected MenuController menuController;

    protected virtual void Awake ()
    {

    }

	protected virtual void OnEnable ()
	{
		OptionsManager.Instance.LanguageChange += OnLanguageChange;
	}

    protected virtual void OnDisable ()
	{
		OptionsManager.Instance.LanguageChange -= OnLanguageChange;
	}

	protected virtual void OnLanguageChange ()
	{

	}
}
