using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    string player = "Player";
    bool isActive = false;
    public int EnemyDamage;
    
    
    
    

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag(player) && Player._instance.health > 0 && !isActive)
        {
            isActive = true;
            Invoke("Damage", 1.0f);
            
        }
    }

    public void Damage()
    {
        isActive = false;
        Debug.Log("Área de daño: " + Player._instance.health);
        Player._instance.Damage(EnemyDamage);
        
    }
}
