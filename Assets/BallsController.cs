using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsController : MonoBehaviour
{
    public AudioClip collisionAudio; // som da colisao
    public AudioClip cacapaAudio;
    void Start(){
        // cada bola
        foreach (Transform ball in transform){

            AudioSource audioSource = ball.gameObject.AddComponent<AudioSource>();

            // pega o script de som da bola
            BallsAudio ballsAudio = ball.gameObject.AddComponent<BallsAudio>();

            BallController ballController = ball.gameObject.GetComponent<BallController>();

            // configura o som de colisao
            ballsAudio.collisionAudio = collisionAudio;
            ballsAudio.audioSource = audioSource;

            ballController.cacapaAudio = cacapaAudio;
            ballController.audioSource = audioSource;
        }
    }
}
