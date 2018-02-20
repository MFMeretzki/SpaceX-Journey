using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsTest : MonoBehaviour {

	private const string PLAYER_KEY = "PLAYER_";
	private const string POINTS_KEY = "POINTS_";
	private const int MAX_SCORES = 10;

	public InputField nameIF;
	public InputField pointsIF;

	public void AddButtonClick ()
	{
		string name = nameIF.text;
		string points = pointsIF.text;
		if (name != "" && points != "")
		{
			AddScore(name, int.Parse(points));
		}
	}

	public void RemoveButtonClick ()
	{
		string name = nameIF.text;
		string points = pointsIF.text;
		if (name != "" && points != "")
		{
			RemoveScore(name, int.Parse(points));
		}
	}


	private void AddScore (string name, int points)
	{
		for (int i = 0; i < MAX_SCORES; ++i)
		{
			int p = PlayerPrefs.GetInt(POINTS_KEY + i, -1);
			if (p < 0)
			{
				PlayerPrefs.SetString(PLAYER_KEY + i, name);
				PlayerPrefs.SetInt(POINTS_KEY + i, points);
				Debug.LogFormat("Added {0} with score {1} to slot {2}", name, points, i);
				break;
			}
		}
	}

	private void RemoveScore (string name, int points)
	{
		for (int i = 0; i < MAX_SCORES; ++i)
		{
			string s = PlayerPrefs.GetString(PLAYER_KEY + i, "");
			int p = PlayerPrefs.GetInt(POINTS_KEY + i, -1);
			if (s == name && p == points)
			{
				PlayerPrefs.DeleteKey(PLAYER_KEY + i);
				PlayerPrefs.DeleteKey(POINTS_KEY + i);
				break;
			}
		}
	}
}
