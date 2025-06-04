using System.Collections;
using UnityEngine;

public class Ball3DMovement : MonoBehaviour
{
    public float initialSpeed = 25f;
    public float speedIncreaseRate = 2f;
    private float currentSpeed;
    private Rigidbody rb;
    private Vector3 startPosition;
    private Coroutine speedBoostCoroutine;

    public GameObject lastPlayerBounced;

    [Header("Sound Effects")]
    public AudioClip paddleHitSound;
    public AudioClip wallHitSound;
    public AudioClip powerupHitSound;
    public AudioClip resetSoundClip;
    public AudioClip gameOverMusicClip;

    private AudioSource audioSource;
    private static bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        ResetBall(true);
    }

    void FixedUpdate()
    {
        if (!isGameOver)
        {
            rb.velocity = rb.velocity.normalized * currentSpeed;
            currentSpeed += speedIncreaseRate * Time.fixedDeltaTime;
        }
    }

    public void ResetBall(bool toRight)
    {
        if (resetSoundClip != null)
            PlaySound(resetSoundClip);

        transform.position = startPosition;
        rb.velocity = Vector3.zero;
        currentSpeed = initialSpeed;

        float xDir = toRight ? 1f : -1f;
        Vector3 direction = new Vector3(Random.Range(0.5f, 1f) * xDir, 0f, Random.Range(-1f, 1f)).normalized;
        rb.velocity = direction * currentSpeed;
    }

    public void StopBall()
    {
        rb.velocity = Vector3.zero;
        enabled = false;
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lastPlayerBounced = collision.gameObject;
            PlaySound(paddleHitSound);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            PlaySound(wallHitSound);
        }
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            PlaySound(powerupHitSound);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
    }

    // Call this from GameManager when game ends
    public void PlayGameOverMusic()
    {
        isGameOver = true;
        rb.velocity = Vector3.zero;

        if (gameOverMusicClip != null)
        {
            audioSource.Stop(); // Stop any in-progress SFX
            audioSource.clip = gameOverMusicClip;
            audioSource.loop = false;
            audioSource.Play();
        }
    }
}
