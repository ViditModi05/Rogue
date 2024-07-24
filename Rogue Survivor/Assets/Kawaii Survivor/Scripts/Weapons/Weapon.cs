using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Rendering;

public class Weapon : MonoBehaviour
{
    enum State
    {
        Idle,
        Attack
    }

    private State state;

    [Header("References")]
    [SerializeField] private Transform hitDetectionTransform;
    [SerializeField] private BoxCollider2D hitCollider;
    
    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] private bool gizmos;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float hitDetectionRadius;
    [SerializeField] private float aimLerp;

    [Header("Attack")]
    [SerializeField] private Animator animator;
    [SerializeField] private int damage;
    private List<Enemy> damagedEnemies = new List<Enemy>();
    [SerializeField] private float attackDelay;
    private float attackTimer;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;

    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Idle:
            {
                AutoAim();
                break;
            }

            case State.Attack:
            {
                Attacking();
                break;
            }
        }
    }

    [NaughtyAttributes.Button]
    private void StartAttack()
    {
        animator.Play("ClubAnimation");
        state = State.Attack;
        damagedEnemies.Clear();
        animator.speed = 1f / attackDelay;
    }
    private void Attacking()
    {
        Attack();
    }

    private void StopAttack()
    {
        state = State.Idle;
        damagedEnemies.Clear();
    }

    private void Attack()
    {
       //Collider2D[] enemies = Physics2D.OverlapCircleAll(hitDetectionTransform.position, range, enemyMask);
        Collider2D[] enemies = Physics2D.OverlapBoxAll(hitDetectionTransform.position, 
        hitCollider.bounds.size, 
        hitDetectionTransform.localEulerAngles.z,
        enemyMask);

        for(int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            if(!damagedEnemies.Contains(enemy))
            {
                enemy.TakeDamage(damage);
                damagedEnemies.Add(enemy);
            }
            
        }

    }
    
    private Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

        if(enemies.Length <= 0)
        {
            return null;            
        }

        float minDistance = range;

        for( int i = 0 ; i < enemies.Length ; i++)
        {
            Enemy enemyChecked = enemies[i].GetComponent<Enemy>();
            float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);
            if(distanceToEnemy < minDistance)
            {
                closestEnemy = enemyChecked;
                minDistance = distanceToEnemy;
            }
        }

        return closestEnemy;
        
    }

    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        Vector2 targetUpVector = Vector3.up;
        if(closestEnemy != null)
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;
            ManageAttack();
        }
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
       IncrementAttackTimer();
    }

    private void ManageAttack()
    {

        if(attackTimer >= attackDelay)
        {
            attackTimer = 0;
            StartAttack();
        }
    }

    private void IncrementAttackTimer()
    {
        attackTimer += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        if(!gizmos)
        {
            return;
        }
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(hitDetectionTransform.position,hitDetectionRadius);

    }
}
