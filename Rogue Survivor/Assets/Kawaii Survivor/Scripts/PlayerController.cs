using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private MobileJoystick mobileJoystick;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    void Start()
    {
        rig.velocity = Vector2.right;   
        
    }

    private void FixedUpdate()
    {
        rig.velocity = mobileJoystick.GetMoveVector() * moveSpeed * Time.deltaTime; 
    }
}
