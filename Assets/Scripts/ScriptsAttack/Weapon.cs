using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int damage;
    public float cooldown;
    public bool isEnemy = false;
    public GameObject dagger;

    public void Attak()
    {
        
    }

}
