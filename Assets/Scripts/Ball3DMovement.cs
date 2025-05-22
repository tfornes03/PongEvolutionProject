using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball3DMovement : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Set a random direction on the X-Z plane
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        rb.velocity = direction * speed;
    }

    void FixedUpdate()
    {
        // Keep velocity at constant speed
        rb.velocity = rb.velocity.normalized * speed;
    }
}
