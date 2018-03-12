using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour {

	void Start ()
	{
		StartCoroutine(LoadGame());
	}


	private IEnumerator LoadGame ()
	{
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene("MainMenu");
	}

}
