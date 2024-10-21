using UnityEngine;

[RequireComponent(typeof(Vision))]
[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Orientation2D))]
public class Snake : MonoBehaviour
{
    public float chargeDelay;
    public float attackRadius;
    public float attackSpeed;
    public float pushRadius;

    public Transform aim;

    Vision vision;
    Animator animator;
    Orientation2D orient;
    Attack attack;

    Team target;
    bool isAttacking = false;

    void Awake()
    {
        vision = GetComponent<Vision>();
        attack = GetComponent<Attack>();
        animator = GetComponent<Animator>();
        orient = GetComponent<Orientation2D>();
    }

    void FixedUpdate()
    {
        if (isAttacking || !attack.IsReady) return;

        var closest = vision.GetClosestTarget(out var closestSqrDist);
        if (closest == null)
        {
            target = null;
            return;
        }

        if (closestSqrDist <= pushRadius * pushRadius)
            PushAway(closest);
        else if (closestSqrDist <= attackRadius * attackRadius)
            StartAttack(closest);
        else if (!vision.targets.Contains(target))
            target = closest;
    }

    void Update()
    {
        if (isAttacking) return;

        var point = target == null
            ? transform.position - transform.up
            : target.transform.position;

        animator.SetBool("aim", target != null);
        orient.LookAtSmooth(point);
    }

    void StartAttack(Team team)
    {
        isAttacking = true;
        aim.position = team.transform.position;

        animator.SetFloat("chargeDelay", 1 / chargeDelay);
        animator.SetFloat(
            "attackSpeed",
            1 / (aim.position - transform.position).magnitude * attackSpeed
        );
        animator.SetTrigger("attack");
    }

    void EndAttack()
    {
        attack.Hit();
        isAttacking = false;
    }

    void PushAway(Team team)
    {
        print("snake push");
    }
}
