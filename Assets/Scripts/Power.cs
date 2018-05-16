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

    [SerializeField] private int _powerId;
    [SerializeField] private float _range;
    [SerializeField] private DamageColor _powerColor;
    [SerializeField] private float _timeBetweenBullets;
    [SerializeField] private float _damage;
    [SerializeField] private AudioClip _shootingAudioClip;
    [SerializeField] private Color _lineColor;
    [SerializeField] private float _startPowerQuantity;
    [SerializeField] private float _reloadingTime;

    private float _powerQuantity;

    public void Awake()
    {
        _powerQuantity = StartPowerQuantity;
    }


    public void DecreasePower(float delta)
    {
        if (PowerQuantity <= 0)
            return;

        _powerQuantity -= delta;

        if (PowerQuantity <= 0)
            _powerQuantity = 0;
    }

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

    public Color LineColor
    {
        get
        {
            return _lineColor;
        }

        set
        {
            _lineColor = value;
        }
    }

    public float PowerQuantity
    {
        get { return _powerQuantity; }
        set { _powerQuantity = value; }
    }

    public float StartPowerQuantity
    {
        get { return _startPowerQuantity; }
    }

    public int PowerId
    {
        get
        {
            return _powerId;
        }

        set
        {
            _powerId = value;
        }
    }

    public float ReloadingTime
    {
        get
        {
            return _reloadingTime;
        }

        set
        {
            _reloadingTime = value;
        }
    }
}
