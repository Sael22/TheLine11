using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public Slider healthSlider; // الشريط الصحي
    public Slider laserSlider;  // شريط الليزر
    public GameObject laserPrefab; // نموذج الليزر
    public Transform laserSpawnPoint; // نقطة إطلاق الليزر

    private HealthSystem healthSystem = new HealthSystem(100);
    private int laserCount = 0; // عداد الليزر
    private int maxLasers = 10; // الحد الأقصى لليزر

    private void Start()
    {
        // إعداد شريط الصحة
        healthSlider.maxValue = 1f;
        healthSlider.value = healthSystem.getHealthPercentage();

        // إعداد شريط الليزر
        laserSlider.maxValue = maxLasers;
        laserSlider.value = laserCount;
    }

    private void Update()
    {
        // تحديث شريط الصحة
        healthSlider.value = healthSystem.getHealthPercentage();

        // تحديث شريط الليزر
        laserSlider.value = laserCount;

        // إطلاق الليزر عند الضغط على زر F وبوجود ذخيرة
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
            Destroy(other.gameObject); // حذف العدو
        }
        else if (other.gameObject.tag == "Heal")
        {
            healthSystem.Heal(10);
            Destroy(other.gameObject); // حذف عنصر الشفاء
        }
        else if (other.gameObject.tag == "Laser")
        {
            // زيادة عدد الليزر
            if (laserCount < maxLasers)
            {
                laserCount++;
                Destroy(other.gameObject); // حذف عنصر الليزر
            }
        }
    }

    private void ShootLaser()
    {
        // إطلاق الليزر
        GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
        Rigidbody rb = laser.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.AddForce(laserSpawnPoint.forward * 500f); // إطلاق القوة
        }
        laserCount--; // تقليل عدد الليزر بعد الإطلاق
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
