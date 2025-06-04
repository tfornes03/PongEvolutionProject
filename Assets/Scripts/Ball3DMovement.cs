using System.Collections;
using UnityEngine;

public class Ball3DMovement : MonoBehaviour
{
    public float initialSpeed = 25f;
    public float speedIncreaseRate = 2f; // increase per second
    private float currentSpeed;
    private Rigidbody rb;
    private Vector3 startPosition;
    private Coroutine speedBoostCoroutine;

    public GameObject lastPlayerBounced;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        ResetBall(true); // Start towards Player 2
    }

    void FixedUpdate()
    {
        // Apply current speed in the moving direction
        rb.velocity = rb.velocity.normalized * currentSpeed;
        currentSpeed += speedIncreaseRate * Time.fixedDeltaTime;
    }

    public void ResetBall(bool toRight)
    {
        transform.position = startPosition;
        rb.velocity = Vector3.zero;
        currentSpeed = initialSpeed;

        // Random angle on X-Z plane, leaning toward correct side
        float xDir = toRight ? 1f : -1f;
        Vector3 direction = new Vector3(Random.Range(0.5f, 1f) * xDir, 0f, Random.Range(-1f, 1f)).normalized;

        rb.velocity = direction * currentSpeed;
    }
    public void StopBall()
    {
        rb.velocity = Vector3.zero;
        enabled = false; // Disables this script
    }
    public void ApplySpeedBoost(float amount, float duration)
    {
        if (speedBoostCoroutine != null) StopCoroutine(speedBoostCoroutine);
        speedBoostCoroutine = StartCoroutine(SpeedBoost(amount, duration));
    }

    IEnumerator SpeedBoost(float amount, float duration)
    {
        float original = initialSpeed;
        initialSpeed += amount;
        yield return new WaitForSeconds(duration);
        initialSpeed = original;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lastPlayerBounced = collision.gameObject;
        }
    }
}
