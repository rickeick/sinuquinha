using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tests : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(EsperarSegundos(3));
    }

    IEnumerator EsperarSegundos(float segundos)
    {
        rb.AddForce(new Vector3(1, 0, 0)*20, ForceMode.Impulse);
        yield return new WaitForSeconds(segundos);
        rb.AddForce(new Vector3(0, 0, 1)*20, ForceMode.Impulse);
    }

    void FixedUpdate() {
        if (rb.velocity.magnitude > 0.1f) {
            rb.velocity *= 0.99f;
        } else {
            rb.velocity = Vector3.zero;
        }
    }
}
