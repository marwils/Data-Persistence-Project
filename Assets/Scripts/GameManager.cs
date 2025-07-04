using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    private Brick m_brickPrefab;

    [SerializeField]
    private int m_lineCount = 6;

    [SerializeField]
    private Rigidbody m_ball;

    [Header("UI Elements")]
    [SerializeField]
    private Text m_scoreText;

    [SerializeField]
    private GameObject m_gameOverText;

    [SerializeField]
    private Button m_backButton;

    private bool m_Started = false;
    private int m_points;
    private bool m_GameOver = false;

    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < m_lineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(m_brickPrefab, position, Quaternion.identity);
                brick.SetPointValue(pointCountArray[i]);
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    void OnEnable()
    {
        m_backButton.onClick.AddListener(BackToMenu);
    }

    void OnDisable()
    {
        m_backButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                m_ball.transform.SetParent(null);
                m_ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void UpdateScoreText()
    {
        List<MainManager.HighscoreData> highscores = MainManager.Instance.GetHighscores();
        if (highscores.Count == 0)
        {
            m_scoreText.text = "";
        }
        else
        {
            m_scoreText.text = $"Best Score : {highscores[0].playerName} : {highscores[0].score}";
        }
    }

    void AddPoint(int point)
    {
        m_points += point;
        m_scoreText.text = $"Score : {m_points}";
    }

    public void GameOver()
    {
        MainManager.Instance.AddToHighscores(m_points);
        m_GameOver = true;
        m_gameOverText.SetActive(true);
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
