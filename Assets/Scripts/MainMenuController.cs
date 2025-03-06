using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenuController : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    } 
    public void OptionsOrMainMenu(){
        if(SceneManager.GetActiveScene().buildIndex == 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }else if(SceneManager.GetActiveScene().buildIndex == 1){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }else if(SceneManager.GetActiveScene().buildIndex == 4){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
        }else if(SceneManager.GetActiveScene().buildIndex == 5){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 5);
        }
    } 
    public void QuitGame(){
        Debug.Log("quit!");
        Application.Quit();
    } 
}
