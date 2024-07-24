using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rig;
    private Collider2D collider;
    private RangeEnemyAttack rangeEnemyAttack;

    [Header("Settings")]
    private int damage;
    [SerializeField] private float bulletSpeed;
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        LeanTween.delayedCall(gameObject, 5, () => rangeEnemyAttack.ReleaseBullet(this));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(int damage, Vector2 direction)
    {
        this.damage = damage;
        transform.right = direction;
        rig.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.TryGetComponent(out Player player))
        {
            LeanTween.cancel(gameObject);
            player.TakeDamage(damage);
            this.collider.enabled = false;

            rangeEnemyAttack.ReleaseBullet(this);
        }
    }

    public void Configure(RangeEnemyAttack rangeEnemyAttack)
    {
        this.rangeEnemyAttack = rangeEnemyAttack;
    }

    public void Reload()
    {
        rig.velocity = Vector2.zero;
        collider.enabled = true;
    }
}
