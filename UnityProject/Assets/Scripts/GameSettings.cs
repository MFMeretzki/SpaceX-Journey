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
        int pos;

        if (scoresList.Count > 0)
        {
            if (scoresList.Count < MAX_SCORES)
            {
                pos = scoresList.Count;
            }
            else if (scoresList[scoresList.Count -1].points < points)
            {
                int i = scoresList.Count - 2;
                while (i >= 0 && scoresList[i].points < points)
                {
                    i--;
                }
                pos = i + 1;
            }
            else
            {
                pos = -1;
            }
        }
        else
        {
            pos = 0;
        }

        return pos;
	}

	public void AddRecord (string name, int points)
	{
        int slot = IsNewRecord(points);

		if (slot >= 0)
		{
			ScoreData sd = new ScoreData(name, points, slot);
			scoresList.Add(sd);
			SortScoresList();
			PlayerPrefs.SetString(PLAYER_KEY + sd.slot, name);
			PlayerPrefs.SetInt(POINTS_KEY + sd.slot, points);

            if (scoresList.Count > MAX_SCORES)
            {
                scoresList.RemoveAt(scoresList.Count - 1);
            }
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
