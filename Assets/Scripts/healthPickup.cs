using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    PlayerController playerHealth;
    public float healthBonus = 20f;
        
void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        //PlayerController.currentHealth = PlayerController.currentHealth + healthBonus;
    }
}
