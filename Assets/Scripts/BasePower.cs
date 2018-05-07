using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : MonoBehaviour
{
    public enum DamageColor
    {
        Red, Black,
    }

    [SerializeField] protected float range;
    [SerializeField] protected DamageColor powerColor;
    [SerializeField] protected float timeBetweenBullets;
    [SerializeField] protected float damage;
    [SerializeField] protected AudioSource audioSource;

}
