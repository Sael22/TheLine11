using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            // Destroy the object with the specified tag
            Destroy(other.gameObject);
        }
    }
}
