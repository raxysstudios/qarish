using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class SheepControl : MonoBehaviour
{
    private Rigidbody2D rb;
    public float walkSpeed = 5f;
    private Vector2? moveTarget;
    
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
        if((moveTarget.Value - rb.position).sqrMagnitude <= 1)
        {
            moveTarget = null;
            return;
        }
        var move = walkSpeed *(moveTarget.Value - rb.position).normalized;
        rb.MovePosition(
            rb.position + move * Time.fixedDeltaTime
        );

    }
    
    public void RecieveCall(Vector2 point, bool isDiraction)
    {
        if (moveTarget == null)
        {
            moveTarget = point;
        }
        else
        {
            moveTarget = null;
        }
    }
    
}