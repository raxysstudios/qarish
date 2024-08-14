using System;
using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;
    

    private int locationIndex = 0;
    private int hp = 5;

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

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Attack");
            EnemyHP -= 1;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("No Attack");
        }
    }
    
    
    
}
