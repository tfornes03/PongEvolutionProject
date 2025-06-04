using UnityEngine;

public class ShrinkOpponentPaddlePowerUp : MonoBehaviour
{
    public float factor = 0.5f;  // Less than 1 to shrink
    public float duration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Ball3DMovement ball = other.GetComponent<Ball3DMovement>();

            if (ball.lastPlayerBounced != null)
            {
                string lastPlayerName = ball.lastPlayerBounced.name;
                string opponentName = (lastPlayerName == "Player1") ? "Player2" : "Player1";

                GameObject opponent = GameObject.Find(opponentName);
                if (opponent != null)
                {
                    PlayerPaddle opponentPaddle = opponent.GetComponent<PlayerPaddle>();
                    if (opponentPaddle != null)
                    {
                        opponentPaddle.Shrink(factor, duration);
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
