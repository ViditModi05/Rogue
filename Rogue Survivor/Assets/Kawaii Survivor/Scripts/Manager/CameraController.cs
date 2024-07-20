using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
 {
    
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Setings")]
    [SerializeField] private Vector2 minMaxXY;
    

  private void LateUpdate() 
   {
      if(player == null)
      {
          Debug.LogWarning("No player has been found");
          return;
      }
      Vector3 playerPosition = player.position;
      playerPosition.z = -10;
      playerPosition.x = Mathf.Clamp(playerPosition.x, -minMaxXY.x, minMaxXY.x);
      playerPosition.y = Mathf.Clamp(playerPosition.y, -minMaxXY.y, minMaxXY.y);
      transform.position = playerPosition;  
   }
}
