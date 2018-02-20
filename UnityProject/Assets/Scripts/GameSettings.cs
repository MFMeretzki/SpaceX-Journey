using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

	private const string PLAYER_KEY = "PLAYER_";
	private const string POINTS_KEY = "POINTS_";
	private const int MAX_SCORES = 10;

	private List<ScoreData> scoresList;

	private static GameSettings instance;
	public static GameSettings Instance
	{
		get { return instance; }
	}

	void Awake ()
	{

		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		scoresList = new List<ScoreData>();
		for (int i = 0; i < MAX_SCORES; ++i)
		{
			int points = PlayerPrefs.GetInt(POINTS_KEY + i, -1);
			if (points >= 0)
			{
				string name = PlayerPrefs.GetString(PLAYER_KEY + i, "");
				ScoreData sd = new ScoreData(name, points, i);
				scoresList.Add(sd);
				Debug.LogFormat("<color=green> INFO: player {0} with score {1}</color>", name, points);
			}
		}
		SortScoresList();
	}


	public int IsNewRecord (int points)
	{
		if (scoresList.Count < MAX_SCORES || scoresList[scoresList.Count - 1].points < points)
		{
			for (int i = scoresList.Count - 1; i <= 0; --i)
			{
				if (scoresList[i].points >= points) return i + 2;
			}
		}
		return -1;
	}

	public void AddRecord (string name, int points)
	{
		int slot = -1;
		if (scoresList.Count < MAX_SCORES)
		{
			slot = scoresList.Count;
		}
		else if (scoresList[scoresList.Count - 1].points < points)
		{
			slot = scoresList[scoresList.Count - 1].slot;
		}

		if (slot >= 0)
		{
			ScoreData sd = new ScoreData(name, points, slot);
			scoresList.RemoveAt(scoresList.Count - 1);
			scoresList.Add(sd);
			SortScoresList();
			PlayerPrefs.SetString(PLAYER_KEY + sd.slot, name);
			PlayerPrefs.SetInt(POINTS_KEY + sd.slot, points);
		}
	}

	public List<Tuple<string, int>> GetScores ()
	{
		List<Tuple<string, int>> l = new List<Tuple<string, int>>();
		foreach (ScoreData sd in scoresList)
		{
			l.Add(new Tuple<string, int>(sd.name, sd.points));
		}

		return l;
	}


	private void SortScoresList ()
	{
		scoresList.Sort((x, y) => y.points.CompareTo(x.points));
	}
}
