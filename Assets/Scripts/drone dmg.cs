using UnityEngine;
using UnityEngine.UI;

public class DroneTrigger : MonoBehaviour
{
    public Slider healthSlider; // Reference to the slider UI
    private const float damageAmount = 0.1f; // Amount to reduce from the slider

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Reduce the slider value by 10
            healthSlider.value -= damageAmount;
            Destroy(this.gameObject);

            // Make sure the slider value doesn't go below 0
            if (healthSlider.value < 0)
                healthSlider.value = 0;

            // Optionally, destroy the enemy object
            Destroy(this.gameObject);
        }
    }
}
