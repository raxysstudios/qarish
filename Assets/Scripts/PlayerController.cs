using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Orientation2D))]
public class PlayerController : MonoBehaviour
{

    public float walkSpeed;
    private Vector2 moveInput;

    private Rigidbody2D rb;
    private Animator anim;
    private Orientation2D orient;
    
    public float callRadius = 3f;


    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        orient = GetComponent<Orientation2D>();
    }


    void FixedUpdate()
    {
        var move = walkSpeed * moveInput;
        rb.MovePosition(
            rb.position + move * Time.fixedDeltaTime
        );
        anim.SetFloat("Speed", move.magnitude);
        

    }

    void LateUpdate()
    {
        var screen = Mouse.current.position.ReadValue();
        var world = Camera.main.ScreenToWorldPoint(
            new Vector3(screen.x, screen.y, 10)
        );
        orient.TurnTo(world);
        orient.LookAt(world);

        // Vector2 viewport = Camera.main.ScreenToViewportPoint(screen);
        // var offset = (viewport - .5f * Vector2.one) * aimDistance;
        // aimComposer.TargetOffset = offset;
    }

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    void OnAttack(InputValue inputValue)
    {
        anim.SetTrigger("Attack");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, callRadius);
    }

    void OnCall(InputValue inputValue)
    {
        print("Is called");
        
        var hits = Physics2D.OverlapCircleAll(transform.position,
            callRadius, LayerMask.GetMask("Unit"));
    
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<SheepControl>(out var sheep))
            {
                
                sheep.ReceiveCall(transform.position, false);
                
            }
        }
    }

    void OnMultiCall(InputValue inputValue)
    {
        print("MultiCall");
        var hits = Physics2D.OverlapCircleAll(transform.position,
            callRadius, LayerMask.GetMask("Unit"));

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<SheepControl>(out var sheep))
            {
                sheep.ReceiveCall(transform.position, true);
                
            }
        }
    }
    
    
    
}
