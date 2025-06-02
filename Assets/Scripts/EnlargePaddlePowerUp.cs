using UnityEngine;

public class EnlargePaddlePowerUp : MonoBehaviour
{
    public float factor = 1.5f;
    public float duration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPaddle paddle = other.GetComponent<PlayerPaddle>();
            if (paddle != null)
            {
                paddle.Enlarge(factor, duration);
            }
            Destroy(gameObject);
        }
    }
}
