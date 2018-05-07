using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerShooting : MonoBehaviour {

    public Power[] Powers;
    public int Damage = 10;
    public float TimeBetweenBullets = 0.15f;
    public int Range = 100;
    public GameObject Gun;
    public AudioClip ShootingSound;
    public AudioSource AudioSource;
    public Transform GunEnd;

    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _mask;
    private float _timer;
    private float _effectsDisplayTime = 0.2f;
    private LineRenderer _gunLine;

    private Power activePower;

    private void Awake()
    {
        _gunLine = Gun.GetComponent<LineRenderer>();
    }

    private void Start()
    {
        if(_cam == null)
        {
            Debug.Log("No camera referenced");
            this.enabled = false;
        }

        AudioSource.clip = ShootingSound;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && _timer >= TimeBetweenBullets && Time.timeScale != 0)
        {
            /*activePower = powers[0] as IPower;
            activePower.Shoot();*/

            Shoot();
        }
        else if (Input.GetButton("Fire2"))
        {
            /*activePower = powers[1] as IPower;
            activePower.Shoot();*/

            Shoot();
        }

        if (_timer >= TimeBetweenBullets * _effectsDisplayTime)
        {
            _gunLine.enabled = false;
        }
    }

    void Shoot()
    {
        _timer = 0f;

        _gunLine.enabled = true;
        _gunLine.SetPosition(0, GunEnd.position);

        PlayShootingSound();

        CheckShootingRaycast();
    }

    private void CheckShootingRaycast()
    {
        RaycastHit _hit;

        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out _hit, Range, _mask))
        {
            EnemyHealth enemyHealth = _hit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(Damage, _hit.point);
            }

            _gunLine.SetPosition(1, _hit.point);
        }
        else
        {
            _gunLine.SetPosition(1, _cam.transform.position + _cam.transform.forward * Range);
        }
    }

    void PlayShootingSound()
    {
        AudioSource.Play();
    }
}
