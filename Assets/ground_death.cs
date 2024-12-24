using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management

public class ground_death : MonoBehaviour
{
    // Tag to check for collision
    public string targetTag = "Player";

    // Called when an object enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the specified tag
        if (other.CompareTag(targetTag))
        {
            // Unlock the cursor before loading the "LOSE" scene
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Make the cursor visible

            // Load the "LOSE" scene
            SceneManager.LoadScene("LOS");
        }
    }
}
