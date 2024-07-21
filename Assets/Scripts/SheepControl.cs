using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class SheepControl : MonoBehaviour
{
    private Rigidbody2D rb;
    public float walkSpeed = 5f;
    private Vector2? moveTarget;
    private bool isDirection = false;
    
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
        if(!isDirection && (moveTarget.Value - rb.position).sqrMagnitude <= 1)
        {
            moveTarget = null;
            return;
        }
        var direction = isDirection ? moveTarget.Value: (moveTarget.Value - rb.position).normalized;
        var move = walkSpeed * direction;
        rb.MovePosition(
            rb.position + move * Time.fixedDeltaTime
        );

    }

    public void Stop()
    {
        print("Stop");
    }

    public void Move(Vector2 point)
    {
        print("Move " + point);
    }

    public void Follow(Transform target)
    {
        print("Follow " + target);
    }
    
}
