#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIControls : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_bestScoreText;

    [SerializeField]
    private TMP_InputField m_nameInput;

    [SerializeField]
    private Button m_startButton;

    [SerializeField]
    private Button m_highscoresButton;

    [SerializeField]
    private Button m_quitButton;

    [SerializeField]
    private Button m_backToMenuButton;

    [SerializeField]
    private GameObject m_highscoresPanel;

    [SerializeField]
    private GameObject m_highscoreLinePrefab;

    [SerializeField]
    private Vector2 m_firstHighscorePosition;

    [SerializeField]
    private float m_highscoresOffset;

    void Start()
    {
        List<MainManager.HighscoreData> highscores = MainManager.Instance.GetHighscores();
        if (highscores.Count == 0)
        {
            m_bestScoreText.text = "No Highscore - until now ;)";
        }
        else
        {
            m_bestScoreText.text = $"Best Score : {highscores[0].playerName} : {highscores[0].score}";
        }
        m_nameInput.text = MainManager.Instance.GetPlayerName();
    }
    void OnEnable()
    {
        m_startButton.onClick.AddListener(StartGame);
        m_highscoresButton.onClick.AddListener(ShowHighscores);
        m_quitButton.onClick.AddListener(QuitGame);
        m_backToMenuButton.onClick.AddListener(HideHighscores);
    }

    void OnDisable()
    {
        m_startButton.onClick.RemoveAllListeners();
        m_highscoresButton.onClick.RemoveAllListeners();
        m_quitButton.onClick.RemoveAllListeners();
        m_backToMenuButton.onClick.RemoveAllListeners();
    }

    private void StartGame()
    {
        MainManager.Instance.SetPlayerName(m_nameInput.text);
        SceneManager.LoadScene(1);
    }

    private void ShowHighscores()
    {
        m_highscoresPanel.SetActive(true);
        float yOffset = 0;
        foreach (MainManager.HighscoreData highscore in MainManager.Instance.GetHighscores())
        {
            GameObject highscoreLineGO = Instantiate(m_highscoreLinePrefab, m_highscoresPanel.transform);
            highscoreLineGO.GetComponent<RectTransform>().anchoredPosition = m_firstHighscorePosition + Vector2.up * yOffset;
            HighscoreLine highscoreLine = highscoreLineGO.GetComponent<HighscoreLine>();
            yOffset += m_highscoresOffset;
            highscoreLine.SetPlayerName(highscore.playerName);
            highscoreLine.SetScore(highscore.score);
        }
    }

    private void HideHighscores()
    {
        HighscoreLine[] highscoreLines = GameObject.FindObjectsByType<HighscoreLine>(FindObjectsSortMode.None);
        foreach (HighscoreLine highscoreLine in highscoreLines)
        {
            Destroy(highscoreLine.gameObject);
        }
        m_highscoresPanel.SetActive(false);
    }

    private void QuitGame()
    {
        MainManager.Instance.SaveGameStats();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
