using UnityEngine;

public class MenuController : MonoBehaviour {

	[SerializeField]
	private RectTransform[] menuPanels;

	private int activePanel = 0;


	void Start ()
	{
		for (int i = 1; i < menuPanels.Length; ++i)
		{
			menuPanels[i].gameObject.SetActive(true);
			menuPanels[i].anchoredPosition = new Vector2(5000, 0);
		}
	}


	public void SwitchPanel (int n)
	{
		menuPanels[activePanel].anchoredPosition =  new Vector2(5000, 0);
		menuPanels[n].anchoredPosition = Vector2.zero;
		activePanel = n;
	}
	
}
