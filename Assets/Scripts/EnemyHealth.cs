using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public int scoreValue = 10;

    private EnemySound sound;

    private void Start()
    {
        sound = GetComponent<EnemySound>();
    }
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        health -= amount;

        if (health <= 0)
        {
            ScoreManager.score += scoreValue;
            sound.PlayEnemyClip(sound.destroyedClip);
            Destroy(gameObject);
        }
    }

}
