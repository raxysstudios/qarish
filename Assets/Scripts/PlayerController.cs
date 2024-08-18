using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Orientation2D))]
public class PlayerController : MonoBehaviour
{
    public Vector2 attackOffset;
    public float attackRange = 0.5f; // Радиус атаки
    public LayerMask enemyLayer; // Слой с НПС
    [Header("Move")]
    public float walkSpeed;
    private Vector2 moveInput;
    private Vector2 worldMouse = Vector2.zero;
    [SerializeField] private int damage;

    private Rigidbody2D rb;
    private Animator anim;
    private Orientation2D orient;

    public FlockController flock;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        orient = GetComponent<Orientation2D>();
    }

    private void Update()
    {
        var screen = Mouse.current.position.ReadValue();
        worldMouse = Camera.main.ScreenToWorldPoint(
            new Vector3(screen.x, screen.y, 10)
        );
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
        orient.TurnTo(worldMouse);
        orient.LookAt(worldMouse);

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
        Vector2 attackPosition = (Vector2)transform.position + attackOffset;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            // Найдите компонент Health (или аналогичный) у НПС и нанесите ему урон
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
            }
        }
        anim.SetTrigger("Attack");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 attackPosition = (Vector2)transform.position + attackOffset;
        Gizmos.DrawWireSphere(attackPosition, attackRange);
        
    }


    void OnCall(InputValue inputValue)
    {
        var hits = Physics2D.OverlapCircleAll(worldMouse, 1,
            LayerMask.GetMask("Unit"));

        if (hits.Length == 0)
            flock.Move(worldMouse);
        else if (hits.Any((c) => c.CompareTag("Player")))
            flock.Follow(transform);
        else if (hits.Any((c) => c.CompareTag("Sheep")))
            flock.Stop();
    }
}
