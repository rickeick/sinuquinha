using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (rb.velocity.magnitude > 0.1f) {
            rb.velocity *= 0.999f;
        } else {
            rb.velocity = Vector3.zero;
        }
    }
}
