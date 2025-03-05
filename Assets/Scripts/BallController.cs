using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip cacapaAudio;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (rb.velocity.magnitude > 0.5f) {
            rb.velocity *= 0.999f;
        } else {
            rb.velocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Caçapa"))
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            rb.AddForce(rb.velocity.normalized, ForceMode.Force);
            if (gameObject.name == "0")
            {
                StartCoroutine(restaurar());
            }
            else
            {
                StartCoroutine(desabilitar());
            }
        }
    }

    IEnumerator desabilitar()
    {
        yield return new WaitForSeconds(1);
        //fazer som aqui
        audioSource.PlayOneShot(cacapaAudio, 1.0f);
        yield return new WaitForSeconds(2);
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        rb.isKinematic = true;
    }

    IEnumerator restaurar()
    {
        yield return new WaitForSeconds(3);
        //fazer som aqui
        audioSource.PlayOneShot(cacapaAudio, 1.0f);
        transform.localPosition = new Vector3(-1f, 2.5f, -31f);
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        yield return new WaitForSeconds(1);
        rb.constraints |= RigidbodyConstraints.FreezePositionY;
    }
}
