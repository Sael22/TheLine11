using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign the bullet prefab in the inspector
    public Transform firePoint;    // Assign the shooting point in the inspector
    public float bulletSpeed = 20f;
    public float bulletLifetime = 5f; // Time before the bullet disappears

    private CharacterController parentController; // Reference to parent's CharacterController

    void Start()
    {
        // Get the CharacterController component from the parent object
        parentController = GetComponentInParent<CharacterController>();

        if (parentController == null)
        {
            Debug.LogError("CharacterController not found on parent!");
        }
    }

    void Update()
    {
        // Check for input to fire
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Create the bullet instance
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Ignore collision with the parent's CharacterController
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), parentController);

        // Access the Rigidbody component and apply velocity
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }

        // Destroy bullet after 5 seconds
        Destroy(bullet, bulletLifetime);
    }
}

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Destroy the bullet on collision with any object
        Destroy(gameObject);
    }
}

