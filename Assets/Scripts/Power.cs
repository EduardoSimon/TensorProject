using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    public enum DamageColor
    {
        Red,
        Black,
    }

    [SerializeField] private float _range;
    [SerializeField] private DamageColor _powerColor;
    [SerializeField] private float _timeBetweenBullets;
    [SerializeField] private float _damage;
    [SerializeField] private AudioClip _shootingAudioClip;

    public float Range
    {
        get { return _range; }

        set { _range = value; }
    }

    public DamageColor PowerColor
    {
        get { return _powerColor; }

        set { _powerColor = value; }
    }

    public float TimeBetweenBullets
    {
        get { return _timeBetweenBullets; }

        set { _timeBetweenBullets = value; }
    }

    public int Damage
    {
        get { return (int)_damage; }

        set { _damage = value; }
    }

    public AudioClip ShootingAudioClip
    {
        get { return _shootingAudioClip; }

        set { _shootingAudioClip = value; }
    }

}
