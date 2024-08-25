using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Team))]
public class Attack : MonoBehaviour
{
    public int damage;
    public float cooldown;

    [Space(10)]
    [SerializeField]
    GameObject effect;
    [SerializeField]
    Transform effectOrigin;

    [SerializeField]
    Collider2D hitBox;
    Team team;

    readonly static List<Collider2D> hits = new();

    public bool IsReady { get; private set; }
    public UnityEvent onReady;

    void Awake()
    {
        team = GetComponent<Team>();
    }

    public void Hit()
    {
        hitBox.enabled = true;
        Physics2D.OverlapCollider(
            hitBox,
            new ContactFilter2D { layerMask = 1 << 3 },
            hits
        );
        hitBox.enabled = false;

        foreach (var hit in hits)
            if (hit.TryGetComponent<Health>(out var health))
                if (team.CanHit(hit.gameObject))
                    health.Damage(damage);

        ShowEffect();

        CancelInvoke(nameof(Cooldown));
        IsReady = false;
        Invoke(nameof(Cooldown), cooldown);
    }

    void ShowEffect()
    {
        if (this.effect == null) return;
        var effect = Instantiate(
            this.effect,
            effectOrigin.position,
            Quaternion.identity
        ).transform;

        effect.right = effectOrigin.right;
        effect.localScale = new Vector3(
            effect.localScale.x
                * Mathf.Sign(effectOrigin.transform.lossyScale.x),
            effect.localScale.y, effect.localScale.z
        );
    }

    void Cooldown()
    {
        onReady.Invoke();
        IsReady = true;
    }
}
