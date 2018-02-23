using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{

	[SerializeField]
	private AudioClip sound;

	private Button button;

	void Start ()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(ReproduceAudioClip);
	}


	void ReproduceAudioClip ()
	{
		SoundManager.Instance.PlayEffect(sound);
	}

}
