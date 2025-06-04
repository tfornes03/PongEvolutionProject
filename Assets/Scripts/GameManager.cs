using UnityEngine;
using UnityEngine.UI;
using TMPro; // If using TextMeshPro

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public Ball3DMovement ball;

    private int scorePlayer1 = 0;
    private int scorePlayer2 = 0;
    private int winScore = 3;
    private bool gameEnded = false;

    void Start()
    {
        winText.gameObject.SetActive(false); // Hide win message at start
        UpdateScoreUI();
    }

    public void ScorePlayer1()
    {
        if (gameEnded) return;

        scorePlayer1++;
        UpdateScoreUI();
        ClearAllPowerUps();
        FindObjectOfType<PowerUpSpawner>()?.RestartSpawning();



        if (scorePlayer1 >= winScore)
        {
            EndGame("Player 1 Wins!");
        }
        else
        {
            ball.ResetBall(true); // To Player 2
        }
    }

    public void ScorePlayer2()
    {
        if (gameEnded) return;

        scorePlayer2++;
        UpdateScoreUI();
        ClearAllPowerUps(); // <<< ADD THIS

        if (scorePlayer2 >= winScore)
        {
            EndGame("Player 2 Wins!");
        }
        else
        {
            ball.ResetBall(false); // To Player 1
        }
    }


    void UpdateScoreUI()
    {
        scoreText.text = $"{scorePlayer1} : {scorePlayer2}";
    }

    void EndGame(string message)
    {
        gameEnded = true;
        ball.StopBall();
        winText.text = message;
        winText.gameObject.SetActive(true);
        ball.GetComponent<Ball3DMovement>().PlayGameOverMusic();

    }

    void ClearAllPowerUps()
    {
        GameObject[] powerUpsInScene = GameObject.FindGameObjectsWithTag("PowerUp");

        foreach (GameObject powerUp in powerUpsInScene)
        {
            if (powerUp != null)
            {
                Destroy(powerUp);
            }
        }
    }


}
