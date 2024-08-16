using System;
using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public Transform[] patrolPoints;
    public float detectionRange = 5f;
    public Transform player;
    public float attackRange = 1.5f;
    private int currentPatrolIndex;
    [SerializeField] private int hp;

    public int EnemyHP
    {
        get { return hp; }
        private set
        {
            hp = value;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        Patrol();
        DetectPlayer();
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         
    //     }
    // }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     
    //     if (other.CompareTag("Player"))
    //     {
    //         
    //     }
    // }
    
    void Patrol()
    {
        // Патрулирование между заданными точками
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void DetectPlayer()
    {
        // Проверка, находится ли игрок в пределах зоны обнаружения
        if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        // Атака игрока
        if (Vector2.Distance(transform.position, player.position) < attackRange)
        {
            Debug.Log("Атаковать игрока");
            // Здесь можно добавить логику атаки (уменьшение здоровья и т.д.)
        }
        else
        {
            // Перемещаемся к игроку
            transform.position = Vector2.MoveTowards(transform.position, player.position, patrolSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
    }
}
