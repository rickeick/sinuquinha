using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("AudioControler"); 
        if(musicObj.Length > 1){
            Destroy(this.gameObject);
        }else{
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
