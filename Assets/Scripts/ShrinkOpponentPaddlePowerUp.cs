using UnityEngine;

public class ShrinkOpponentPaddlePowerUp : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public float shrinkFactor = 0.5f;
    public float duration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject opponent = (other.gameObject == player1) ? player2 : player1;
            PlayerPaddle paddle = opponent.GetComponent<PlayerPaddle>();
            if (paddle != null)
            {
                paddle.Shrink(shrinkFactor, duration);
            }
            Destroy(gameObject);
        }
    }
}
