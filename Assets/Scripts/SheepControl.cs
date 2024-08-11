using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Orientation2D))]
[RequireComponent(typeof(Animator))]
public class SheepControl : MonoBehaviour
{
    public float speed;
    int currentWaypoint = 0;
    public float arrivalDistance = 2;

    public Vector2 target;
    Path path;

    [HideInInspector]
    public Rigidbody2D rb;
    Seeker seeker;
    Orientation2D orient;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        orient = GetComponent<Orientation2D>();
        anim = GetComponent<Animator>();

        InvokeRepeating(nameof(UpdatePath), 0, .5f);
    }

    void UpdatePath()
    {
        seeker.StartPath(rb.position, target, (p) =>
        {
            if (p.error) return;
            path = p;
            currentWaypoint = 0;
        });
    }

    void FixedUpdate()
    {
        if (path == null || currentWaypoint >= path.vectorPath.Count)
        {
            anim.SetFloat("Speed", 0);
            return;
        }
        var waypoint = (Vector2)path.vectorPath[currentWaypoint];
        if ((rb.position - waypoint).sqrMagnitude
            <= arrivalDistance * arrivalDistance)
        {
            currentWaypoint++;
            return;
        }

        var dir = (waypoint - rb.position).normalized;
        var move = speed * dir;
        rb.MovePosition(
           rb.position + move * Time.fixedDeltaTime
        );
        anim.SetFloat("Speed", move.magnitude);

        orient.Sign = move.x >= .1f ? 1 : -1;
    }
}
