using UnityEngine;
using UnityEngine.UI;

public class VersionDisplay : MonoBehaviour {

	public Text versionText;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
		versionText.text = "Developement version " + Application.version;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
