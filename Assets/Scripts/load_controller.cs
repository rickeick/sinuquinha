using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro; // Import TextMeshPro namespace

public class load_controller : MonoBehaviour
{
    public Slider progressBar;  // Reference to the progress bar UI
    public TMP_Text progressText;  // Now using TextMeshPro

    void Start()
    {
        StartCoroutine(LoadAsync("Gameplay")); // Replace with actual scene name
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // Prevents scene from activating until fully loaded
        float fakeProgress = 0f; // Start from 0
        while (!operation.isDone){
            // Simulate a smooth progress bar filling
            fakeProgress += Time.deltaTime * 0.07f; // Adjust speed as needed
            fakeProgress = Mathf.Min(fakeProgress, operation.progress / 0.9f); // Clamp to real progress
            
            progressBar.value = fakeProgress;
            progressText.text = (fakeProgress * 100).ToString("F0") + "%";

            // Only allow activation once progress is fully "fake completed"
            if (fakeProgress >= 1f)
            {
                yield return new WaitForSeconds(1f); // Small delay for effect
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
