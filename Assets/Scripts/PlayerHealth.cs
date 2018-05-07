using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;    
    
    private int currentHealth;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    //Llama cuando cae del escenario
    public void InstaKill()
    {
        currentHealth = 0;
    }
}
