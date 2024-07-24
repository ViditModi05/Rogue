using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DamageText damageTextPrefab;

    [Header("Pooling")]
    private ObjectPool<DamageText> damageTextPool;

    private void Awake()
    {
        Enemy.onDamageTaken += EnemyHitCallback;
    }
    // Start is called before the first frame update
    void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    private DamageText CreateFunction()
    {
        return Instantiate(damageTextPrefab, transform);
    }

    private void ActionOnGet(DamageText damageText)
    {
       damageText.gameObject.SetActive(true); 
    }

    private void ActionOnRelease(DamageText damageText)
    {
       damageText.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(DamageText damageText)
    {
        Destroy(damageText.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnemyHitCallback(int damage, Vector2 enemyPos)
    {
        DamageText damageTextInstance = damageTextPool.Get();

        Vector3 spawnPos = enemyPos + Vector2.up * 1.5f;
        damageTextInstance.transform.position = spawnPos;

        damageTextInstance.Animate(damage);

        LeanTween.delayedCall(1, () => damageTextPool.Release(damageTextInstance));
    }

    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyHitCallback;
    }
}
