using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealt;
    // Start is called before the first frame update
    void Start()
    {
        currentHealt = maxHealth;
    }
    public int GetHealth() 
    {
        return currentHealt;    
    
    }
        


    public void DeductHealth(int damage) 
    {
        currentHealt = currentHealt - damage;
        Debug.Log("player�n can� azald�" + currentHealt);
        if (currentHealt<= 0)
        {

            KillPlayer();
        }
            
    }

    private void KillPlayer()
    {
        print("player �ld�");
    }

    public void AddHealt(int Value) 
    { 
        currentHealt=currentHealt + Value;
        if (currentHealt>maxHealth)
        {
            currentHealt = maxHealth;
        }
    
    
    }

    
    
}
