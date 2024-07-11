using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Orientation2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    public float walkSpeed;
    private Vector2 moveInput;

    private Rigidbody2D rb;
    private Animator anim;
    private Orientation2D orient;
    
    public float radius = 3f;

    public Transform player;
    
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
        Gizmos.DrawWireSphere(transform.position, 3);
    }

    void OnCall(InputValue inputValue)
    {
        print("Is called");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.position - transform.position).normalized, Mathf.Infinity, LayerMask.GetMask("Player"));

        if(hit.collider != null)
        {
            print("Go to the player");
            

            // NPC видит игрока, поэтому он начинает идти к нему
            // Реализуйте здесь логику передвижения NPC
        }

    }

    void OnMultiCall(InputValue inputValue)
    {
        print("MultiCall");
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero);
        
        foreach (RaycastHit2D hit in hits)
        {
            if (obj)
            {
                LayerMask.GetMask("Sheep");
                print("go away");
            }
            else
            {
                print("stay");
            }
        }
    }

    



}
