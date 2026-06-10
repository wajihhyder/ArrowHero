using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public int score = 0;

    // Method to increase score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Method to update the score text
    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void ResetScore(){
        score = 0;
        UpdateScoreText();
    }
}
