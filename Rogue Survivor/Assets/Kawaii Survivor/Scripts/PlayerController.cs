using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
       rig.velocity = mobileJoystick.GetMoveVector() * moveSpeed * Time.deltaTime; 
    }
}
