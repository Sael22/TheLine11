using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public Slider healthSlider; // الشريط الصحي
    public Slider laserSlider;  // شريط الليزر
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            healthSystem.Damage(10);
            Destroy(collision.gameObject); // حذف العدو
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Heal")
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
