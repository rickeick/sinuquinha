using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordasController : MonoBehaviour
{
    public AudioClip collisionAudio; // som da colisao
    void Start(){
        // cada bola
        foreach (Transform borda in transform){

            AudioSource audioSource = borda.gameObject.AddComponent<AudioSource>();

            // pega o script de som da borda
            BordasAudio bordarAudio = borda.gameObject.AddComponent<BordasAudio>();

            // configura o som de colisao
            bordarAudio.collisionAudio = collisionAudio;
            bordarAudio.audioSource = audioSource;
        }
    }
}
