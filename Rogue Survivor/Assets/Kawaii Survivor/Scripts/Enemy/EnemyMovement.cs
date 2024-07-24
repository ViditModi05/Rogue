using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    private Player player;
    
    [Header("Settings")]
    [SerializeField] private float moveSpeed;
   
    public void StorePlayer(Player player)
    {
        this.player = player;
    }
    void Update()
    {  
        // if(player != null)
        // {
        //    FollowPlayer(); 
        // } 
    }
    public void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;   //Getting the direction for the enemy to move in by normalizing
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime; //getting the target position for the enemy
        transform.position = targetPosition; //Setting the target position to the enemy
    }
}
