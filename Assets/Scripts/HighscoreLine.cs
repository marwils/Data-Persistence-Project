using TMPro;
using UnityEngine;

public class HighscoreLine : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerNameText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void SetPlayerName(string playerName)
    {
        playerNameText.text = playerName;
    }
    
    public void SetScore(int score)
    {
        scoreText.text = $"{score}";
    }
}
