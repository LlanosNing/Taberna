using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;

        scoreText.text = score.ToString();
    }
}
