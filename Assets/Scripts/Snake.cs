using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Vision))]
[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Orientation2D))]
public class Snake : MonoBehaviour
{
    public float attackDelay;
    public float attackSpeed;
    public float attackRadius;
    public float pushRadius;

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
        if (isAttacking && !attack.IsReady) return;

        var closest = vision.GetClosestTarget(out var closestSqrDist);
        if (closest == null)
        {
            target = null;
            return;
        }

        if (closestSqrDist <= pushRadius * pushRadius)
            PushAway(closest);
        else if (closestSqrDist <= attackRadius * attackRadius)
            StartCoroutine(Attack(closest));
        else if (!vision.targets.Contains(target))
            target = closest;
    }


    void Update()
    {
        if (isAttacking) return;

        var point = target == null
            ? transform.position - transform.up
            : target.transform.position;
        orient.LookAtSmooth(point);
    }

    void PushAway(Team team)
    {
        print("snake push");
    }

    IEnumerator Attack(Team team)
    {
        print("snake attack");

        isAttacking = true;
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }
}
