using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public float regenDelay;
    public float regenInterval;

    public UnityEvent onDamage;

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
            health = 0;

        onDamage.Invoke();
        CancelInvoke(nameof(Regen));
        InvokeRepeating(nameof(Regen), regenDelay, regenInterval);
    }

    public void Regen()
    {
        if (health < maxHealth)
            health++;
        if (health >= maxHealth)
            CancelInvoke(nameof(Regen));
    }
}
