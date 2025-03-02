using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordasAudio : MonoBehaviour
{
    public AudioClip collisionAudio; // som de colisao
    public AudioSource audioSource;

    void OnCollisionEnter(Collision collision)
    {
        // quando colidir com uma bola
        if (collision.gameObject.CompareTag("Bola"))
        {
            // volume com base na força de colisao
            float volume = Mathf.Clamp(collision.relativeVelocity.magnitude / 10f, 0.1f, 1f);
            audioSource.PlayOneShot(collisionAudio, volume*0.5f); // reproduz som
        }
    }
}
