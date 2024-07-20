using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [Header("References")]
    private Player player;
    private EnemyMovement movement; 

    [Header("Spwan Sequence")]
    [SerializeField] private SpriteRenderer Erenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private bool hasSpawned;

    [Header("Effects")]
    [SerializeField] private ParticleSystem deathParticles;

    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
     [SerializeField] private float playerDetectionRadius;
    private float attackDelay;
    private float attackTimer;

    [Header("Debug")]
    [SerializeField] private bool gizmos;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EnemyMovement>();
        player = FindFirstObjectByType<Player>();

        if(player == null)
        {
            Debug.LogWarning("No player found");
            Destroy(gameObject);
        }
        
       StartSpawnSequence();
       attackDelay = 1f / attackFrequency ;
    }

    private void StartSpawnSequence()
    {
        SetRendererVisibilty(false);

        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, 3f)
        .setSpeed(0.5f)
        .setLoopPingPong(4)
        .setOnComplete(SpawnSequenceCompleted);
    }

    private void SpawnSequenceCompleted()
    {
        SetRendererVisibilty(true);
        hasSpawned = true;  
        movement.StorePlayer(player);
    }

    private void SetRendererVisibilty(bool visibility)
    {
        Erenderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;
    }


    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }

    private void Attack()
    {
        attackTimer = 0;
        player.TakeDamage(damage);    
    }
    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if(distanceToPlayer <= playerDetectionRadius)
        {
            Attack();
        }
    }
    private void Death()
    {
        deathParticles.transform.SetParent(null);
        deathParticles.Play();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
       if(attackTimer >= attackDelay)        
       {
         TryAttack();  
       }
       else
       {
         Wait();
       }
        
    }
    private void OnDrawGizmos()
    {
        if(!gizmos)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
