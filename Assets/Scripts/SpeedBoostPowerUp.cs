using UnityEngine;

public class SpeedBoostPowerUp : MonoBehaviour
{
    public float boostAmount = 10f;
    public float duration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Ball3DMovement ball = other.GetComponent<Ball3DMovement>();
            if (ball != null)
            {
                ball.ApplySpeedBoost(boostAmount, duration);
            }
            Destroy(gameObject);
        }
    }
}
