using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    private static string StatsFileName = "/gamestats.json";

    private static int MaxHighscoresCount = 10;

    private string m_playerName;

    private List<HighscoreData> m_highscores;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        m_highscores = new List<HighscoreData>();

        LoadGameStats();
    }

    public void LoadGameStats()
    {
        try
        {
            string json = File.ReadAllText(Application.persistentDataPath + StatsFileName);
            GameStats gameStats = JsonUtility.FromJson<GameStats>(json);
            m_playerName = gameStats.lastPlayerName;
            m_highscores = gameStats.highscores.ToList();

            SortAndLimitHighscores();
        }
        catch (FileNotFoundException)
        {
        }
    }

    public void SaveGameStats()
    {
        GameStats gameStats = new GameStats();
        gameStats.lastPlayerName = m_playerName;
        gameStats.highscores = m_highscores.ToArray();

        string json = JsonUtility.ToJson(gameStats);
        File.WriteAllText(Application.persistentDataPath + StatsFileName, json);
    }

    public string GetPlayerName()
    {
        return m_playerName;
    }

    public void SetPlayerName(string playerName)
    {
        m_playerName = playerName;
    }

    public void AddToHighscores(int score)
    {
        HighscoreData highscore = new HighscoreData();
        highscore.playerName = m_playerName;
        highscore.score = score;
        m_highscores.Add(highscore);

        SortAndLimitHighscores();
    }

    public List<HighscoreData> GetHighscores()
    {
        return m_highscores;
    }

    private void SortAndLimitHighscores()
    {
        m_highscores.Sort((a, b) => b.score - a.score);

        while (m_highscores.Count > MaxHighscoresCount)
        {
            m_highscores.RemoveAt(m_highscores.Count - 1);
        }
    }

    [Serializable]
    public struct GameStats
    {
        public string lastPlayerName;
        public HighscoreData[] highscores;
    }

    [Serializable]
    public struct HighscoreData
    {
        public string playerName;
        public int score;
    }
}
