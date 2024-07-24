using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [Header("References")]
    
    [SerializeField] private Animator damageTextAnimation;
    [SerializeField] private TextMeshPro damageText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [NaughtyAttributes.Button]

    public void Animate(int damage)
    {
       damageText.text = damage.ToString();
       damageTextAnimation.Play("Animate");
    }
}
