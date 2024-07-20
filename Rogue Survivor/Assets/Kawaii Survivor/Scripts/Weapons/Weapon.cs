using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] private bool gizmos;
    [SerializeField] private LayerMask enemyMask;

    [Header("Settings")]
    [SerializeField] private float aimLerp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AutoAim();
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
        }
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);

    }

    private void OnDrawGizmos()
    {
        if(!gizmos)
        {
            return;
        }
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);

    }
}
