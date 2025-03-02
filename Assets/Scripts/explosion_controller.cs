using System.Collections;
using UnityEngine;

public class explosion_controller : MonoBehaviour
{
    public Animator explosionAnimator; // Assigned through the Inspector
    private Animator animator; // The second Animator for the "isBurning" parameter
    private bool isCoroutineRunning = false; // To check if the coroutine is already running
    public BoxCollider2D explosionCollider;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator for "isBurning" parameter
        explosionCollider = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component

        // Check if explosionAnimator is assigned
        if (explosionAnimator == null)
        {
            Debug.LogError("Explosion Animator is not assigned!");
            return;
        }

        // Check if the BoxCollider2D is assigned
        if (explosionCollider != null)
        {
            Debug.Log("Collider is assigned!");
        }
        else
        {
            Debug.LogError("Explosion Collider (BoxCollider2D) is not assigned!");
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
            // Enable the collider for the explosion
            explosionCollider.enabled = true;
            
            // Trigger the explosion animation
            explosionAnimator.SetTrigger("Explode");
            
            // Toggle the 'isBurning' parameter
            bool isBurning = !animator.GetBool("isBurning");
            animator.SetBool("isBurning", isBurning); // This toggles the burning state

            // Wait until the explosion finishes, and then turn off the collider
            yield return new WaitForSeconds(3f); // Assuming explosion animation takes 0.5 seconds
            
            // Disable the collider after explosion is done
            explosionCollider.enabled = false;

            // Wait 5 seconds before the next explosion
            yield return new WaitForSeconds(5f); // Total time = 5 seconds (adjust the remaining time)
        }
    }
}
