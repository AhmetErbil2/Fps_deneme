using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealt : MonoBehaviour
{
    public int startHealth=100;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void hit(int damage)
    {
        currentHealth -= damage;
            if (currentHealth<0)
        {
            currentHealth = 0;
            Debug.Log("Zombi Öldü" + currentHealth);
        }
        Debug.Log("Zombi Hasar Aldý" + currentHealth);

    }
    void Update()
    {
        
    }
}
