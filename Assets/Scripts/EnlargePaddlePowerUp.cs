using UnityEngine;

public class EnlargePaddlePowerUp : MonoBehaviour
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
                ball.lastPlayerBounced.GetComponent<PlayerPaddle>().Enlarge(factor, duration);
            }
            Destroy(gameObject);
        }
    }
}
