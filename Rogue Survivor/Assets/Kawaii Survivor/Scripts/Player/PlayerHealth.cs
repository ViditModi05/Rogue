using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxHealth;
    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage , health);
        health -= realDamage;

        if(health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Debug.Log("Game Over");
    }
}
