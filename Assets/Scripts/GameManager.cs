using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public Ball3DMovement ball;
    public GameObject resetButtonObject; // Assign this in Inspector
    public TextMeshProUGUI restartText; // Assign in Inspector


    private int scorePlayer1 = 0;
    private int scorePlayer2 = 0;
    private int winScore = 3;
    private bool gameEnded = false;

    void Start()
    {
        winText.gameObject.SetActive(false); // Hide win message at start
        if (restartText != null)
            restartText.gameObject.SetActive(false);

        if (resetButtonObject != null)
            resetButtonObject.SetActive(false); // Hide reset button initially

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
        ClearAllPowerUps();
        FindObjectOfType<PowerUpSpawner>()?.RestartSpawning();

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
        restartText.gameObject.SetActive(true); 
        ball.GetComponent<Ball3DMovement>().PlayGameOverMusic();
        FindObjectOfType<PowerUpSpawner>().StopSpawning();

        if (resetButtonObject != null)
            resetButtonObject.SetActive(true); // Show reset button

        if (restartText != null)
        restartText.gameObject.SetActive(true);
    }

    

    public void RestartGame()
    {
        Debug.Log("RestartGame() called");

        scorePlayer1 = 0;
        scorePlayer2 = 0;
        gameEnded = false;

        winText.gameObject.SetActive(false);
        UpdateScoreUI();
        ClearAllPowerUps();
        FindObjectOfType<PowerUpSpawner>()?.RestartSpawning();

        if (resetButtonObject != null)
        {
            resetButtonObject.SetActive(false); // Hide reset button again

            // Reset button's internal state
            ResetButton resetScript = resetButtonObject.GetComponent<ResetButton>();
            if (resetScript != null)
                resetScript.ResetButtonState();
        }

        ball.enabled = true;
        ball.ResetBall(Random.value > 0.5f); // Start in random direction

        if (restartText != null)
            restartText.gameObject.SetActive(false);
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
