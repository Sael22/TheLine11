using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public Slider healthSlider;
    public Slider laserSlider;
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;

    public AudioSource healAudioSource;
    public AudioSource enemyAudioSource;
    public AudioSource laserAudioSource;

    private HealthSystem healthSystem = new HealthSystem(100);
    private int laserCount = 0;
    private int maxLasers = 10;

    private void Start()
    {
        healthSlider.maxValue = 1f;
        healthSlider.value = healthSystem.getHealthPercentage();

        laserSlider.maxValue = maxLasers;
        laserSlider.value = laserCount;
    }

    private void Update()
    {
        healthSlider.value = healthSystem.getHealthPercentage();
        laserSlider.value = laserCount;

        if (Input.GetKeyDown(KeyCode.F) && laserCount > 0)
        {
            ShootLaser();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            healthSystem.Damage(10);
            if (enemyAudioSource != null) enemyAudioSource.Play();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Heal")
        {
            healthSystem.Heal(10);
            if (healAudioSource != null) healAudioSource.Play();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Laser")
        {
            if (laserCount < maxLasers)
            {
                laserCount++;
                if (laserAudioSource != null) laserAudioSource.Play();
                Destroy(other.gameObject);
            }
        }
    }

    private void ShootLaser()
    {
        GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
        Rigidbody rb = laser.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.AddForce(laserSpawnPoint.forward * 500f);
        }
        laserCount--;
    }
}

public class HealthSystem
{
    private int maxHealth;
    private int currentHealth;

    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public float getHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }
}
