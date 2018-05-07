using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerShooting : MonoBehaviour {

    public BasePower[] powers;
    public int damage = 10;
    public float timeBetweenBullets = 0.15f;
    public int range = 100;
    public GameObject gun;
    public AudioClip shootingSound;
    public AudioSource audioSource;
    public Transform gunEnd;

    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask mask;
    private float timer;
    private float effectsDisplayTime = 0.2f;
    private LineRenderer gunLine;

    private IPower activePower;

    void Awake()
    {
        gunLine = gun.GetComponent<LineRenderer>();
    }

    void Start()
    {
        if(cam == null)
        {
            Debug.Log("No camera referenced");
            this.enabled = false;
        }

        audioSource.clip = shootingSound;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
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

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            gunLine.enabled = false;
        }
    }

    void Shoot()
    {
        timer = 0f;

        //hace que el line renderer gire
        //gunLine.material.mainTextureOffset = new Vector2(0, Time.time);

        gunLine.enabled = true;
        gunLine.SetPosition(0, gunEnd.position);

        PlayShootingSound();

        CheckShootingRaycast();
    }

    private void CheckShootingRaycast()
    {
        RaycastHit _hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, range, mask))
        {
            EnemyHealth enemyHealth = _hit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage, _hit.point);
            }

            gunLine.SetPosition(1, _hit.point);
        }
        else
        {
            gunLine.SetPosition(1, cam.transform.position + cam.transform.forward * range);
        }
    }

    void PlayShootingSound()
    {
        audioSource.Play();
    }
}
