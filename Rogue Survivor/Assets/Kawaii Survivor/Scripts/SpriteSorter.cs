using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer sRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //sRenderer.sortingOrder = -(int)(transform.position.y * 10);
    }
}
