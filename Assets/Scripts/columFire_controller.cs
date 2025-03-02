using System.Collections;
using UnityEngine;

public class columFire_controller : MonoBehaviour
{
    public Animator columnFireAnimator; // Assigned through the Inspector
    public Transform ObjTransform; // to get the position of the inspector
    private Animator animator; // The second Animator for the "isBurning" parameter
    private bool isCoroutineRunning = false; // To check if the coroutine is already running
    public BoxCollider2D columnFireCollider;
    Vector3 positionColumnFire;
    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator for "isBurning" parameter
        ObjTransform = GetComponent<Transform>(); // Get the Transform
        columnFireCollider = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component

        // Check if columnFireAnimator is assigned
        if (columnFireAnimator == null)
        {
            Debug.LogError("Colum fire is not assigned!");
            return;
        }

        // Check if the BoxCollider2D is assigned
        if (columnFireCollider != null)
        {
            Debug.Log("Collider is assigned!");
        }
        else
        {
            Debug.LogError("Colum fire Collider (BoxCollider2D) is not assigned!");
            return;
        }

        // Start the loop for the explosion effect
        StartExplosionLoop();
    }

    void StartExplosionLoop()
    {
        if (!isCoroutineRunning)
        {
            StartCoroutine(PlayExplosionLoop());
        }
    }

    IEnumerator PlayExplosionLoop()
    {
        isCoroutineRunning = true;

        while (true)
        {
            // Enable the collider for the flames
            columnFireCollider.enabled = true;
            
            // Trigger the colum fire animation
            columnFireAnimator.SetTrigger("Flaming");
            
            // Toggle the 'isFlaming' parameter
            bool isFlaming = !animator.GetBool("isFlaming");
            animator.SetBool("isFlaming", isFlaming); // This toggles the burning state

            // Wait until the explosion finishes, and then turn off the collider
            yield return new WaitForSeconds(5f); // Assuming explosion animation takes 0.5 seconds
            
            // Disable the collider after explosion is done
            columnFireCollider.enabled = false;

            // Wait 5 seconds before the next explosion
            yield return new WaitForSeconds(2f); // Total time = 5 seconds (adjust the remaining time)
        }
    }
}
