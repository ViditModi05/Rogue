using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class RangeEnemyAttack : MonoBehaviour
{

    [Header("References")]
    private Player player;
    [SerializeField] private EnemyBullet bulletPrefab;
    [SerializeField] private Transform shootingPos;

    [Header("Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    private float attackDelay;
    private float attackTimer;
    
    [Header("Bullet Pool")]
    [SerializeField] private ObjectPool<EnemyBullet> bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = attackDelay;
        bulletPool = new ObjectPool<EnemyBullet>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    private EnemyBullet CreateFunction()
    {
        EnemyBullet bulletInstance = Instantiate(bulletPrefab,shootingPos.position, Quaternion.identity);
        bulletInstance.Configure(this);
        return bulletInstance;
    }

    private void ActionOnGet(EnemyBullet enemyBullet)
    {
        enemyBullet.Reload();
        enemyBullet.transform.position = shootingPos.position;
        enemyBullet.gameObject.SetActive(true);
    }

    private void ActionOnRelease(EnemyBullet enemyBullet)
    {
       enemyBullet.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(EnemyBullet enemyBullet)
    {
        Destroy(enemyBullet.gameObject);
    }

    public void ReleaseBullet(EnemyBullet enemyBullet)
    {
        bulletPool.Release(enemyBullet);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StorePlayer(Player player)
    {
        this.player = player;
    }

    public void AutoAim()
    {
         
        ManageShooting();
    }

    private void ManageShooting()
    {
        attackTimer += Time.deltaTime;

        if(attackTimer >= attackDelay)
        {
            attackTimer = 0;
            Shoot();
        }
        
    }
   
    private void Shoot()
    {
        Vector2 direction = (player.GetCenter() - (Vector2)shootingPos.position).normalized;
        EnemyBullet bulletInstance = bulletPool.Get();
        bulletInstance.Shoot(damage, direction);
        //Vector2 targetPos = transform.position + direction * bulletSpeed * Time.deltaTtime;
    }

}
