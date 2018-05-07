using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyHealth : MonoBehaviour
{
    public int Health = 100;
    public int ScoreValue = 10;
    public Power.DamageColor EnemyColor;

    private EnemySound _sound;

    private void Start()
    {
        _sound = GetComponent<EnemySound>();
    }

    public void TakeDamage(int amount, Vector3 hitPoint, Power.DamageColor activePowerColor)
    {
        if (activePowerColor != EnemyColor) return;

        Health -= amount;

        if (Health <= 0)
        {
            ScoreManager.score += ScoreValue;
            _sound.PlayEnemyClip(_sound.destroyedClip);
            Destroy(gameObject);
        }
    }

}
