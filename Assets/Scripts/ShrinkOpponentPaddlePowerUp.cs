using UnityEngine;

public class ShrinkOpponentPaddlePowerUp : MonoBehaviour
{
    public float factor = 1.5f;
    public float duration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Ball3DMovement ball = other.GetComponent<Ball3DMovement>();
            if (ball.lastPlayerBounced != null)
            {
                ball.lastPlayerBounced.GetComponent<PlayerPaddle>().Shrink(factor, duration);
            }
            Destroy(gameObject);
        }
    }
}
