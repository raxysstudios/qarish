using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;

    public float regenDelay = 3;
    public float regenInterval = 1;

    public UnityEvent<int, Transform> onDamage;

    void Awake()
    {
        RestartRegen();
    }

    public void Damage(int damage, Transform origin)
    {
        health -= damage;
        if (health <= 0)
            health = 0;

        onDamage.Invoke(damage, origin);
        RestartRegen();
    }

    void RestartRegen()
    {
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
