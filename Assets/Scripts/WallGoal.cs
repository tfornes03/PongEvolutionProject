using UnityEngine;

public class WallGoal : MonoBehaviour
{
    public bool isPlayer1Goal; // True for Wall_4, False for Wall_3

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            FindObjectOfType<GameManager>().SendMessage(
                isPlayer1Goal ? "ScorePlayer2" : "ScorePlayer1"
            );
        }
    }
}
