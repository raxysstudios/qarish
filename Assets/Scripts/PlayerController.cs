using UnityEngine;
using UnityEngine.InputSystem;

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
}
