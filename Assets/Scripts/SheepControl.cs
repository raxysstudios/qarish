using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(Orientation2D))]
public class SheepControl : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public AIPath ai;

    private Orientation2D orient;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ai = GetComponent<AIPath>();
        orient = GetComponent<Orientation2D>();
    }

    void Update()
    {
        orient.Sign = ai.desiredVelocity.x >= .1f ? 1 : -1;
    }
}
