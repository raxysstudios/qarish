using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public float walkSpeed;
    public Vector2? moveTarget;
    public bool isDirection = false;
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!moveTarget.HasValue)
        {
            return;
        }

        if (!isDirection && (moveTarget.Value - rb.position).sqrMagnitude <= 1)
        {
            moveTarget = null;
            return;
        }

        var direction = isDirection ? moveTarget.Value : (moveTarget.Value - rb.position).normalized;
        var move = walkSpeed * direction;
        rb.MovePosition(
            rb.position + move * Time.fixedDeltaTime
        );
    }

    public void Follow(Vector2 point)
    {
        Debug.Log("Walk" + point);
    }
    
}