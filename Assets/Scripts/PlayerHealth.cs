using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float StartingHealth = 10;

    public float CurrentHealthPercentage
    {
        get { return 100 * CurrentHealth / StartingHealth; }
    }

    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = StartingHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
    }

    //Llama cuando cae del escenario
    public void InstaKill()
    {
        CurrentHealth = 0;
    }
}
