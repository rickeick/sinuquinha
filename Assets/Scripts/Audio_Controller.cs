using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Controller : MonoBehaviour
{
     [Header("-------------------Audio Source---------------------")]
     [SerializeField]AudioSource musicSource;
     [SerializeField]AudioSource SFXSource;

     [Header("-------------------Audio Clip---------------------")]
     public AudioClip background;
     public AudioClip walking;
     public AudioClip clicking;

     private static Audio_Controller instance; // Declare at class level
     public void Awake(){
        if (instance == null)
        {
            instance = this; // Set instance to this script
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject); // Keep this GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
 
     public void Start(){
          musicSource.clip = background;
          musicSource.Play();
     }
     public void PlaySFXClick(){
          SFXSource.clip = clicking;
          SFXSource.Play();
     }

}
