using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(LineRenderer))]
public class PlayerShooting : MonoBehaviour {

    public Power[] Powers;
    public GameObject Gun;
    public AudioSource GunAudioSource;
    public Transform GunEnd;
    public string LeftPowerAxisName = "Fire1";
    public string RightPowerAxisName = "Fire2";

    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _mask;
    private float _timer;
    private float _effectsDisplayTime = 0.2f;
    private LineRenderer _gunLine;
    private Power _activePower;
    private Material _lineMaterial;

    public Power ActivePower
    {
        get { return _activePower;}
    }

    private void Awake()
    {
        _gunLine = Gun.GetComponent<LineRenderer>();
        _lineMaterial = _gunLine.material;
        _activePower = Powers[0];
    }

    private void Start()
    {
        if(_cam == null)
        {
            Debug.Log("No camera referenced");
            this.enabled = false;
        }

        GunAudioSource.clip = ActivePower.ShootingAudioClip;
    }

    private void Update()
    {
        Debug.Log(ActivePower.ToString());
        _timer += Time.deltaTime;

        if (_timer >= ActivePower.TimeBetweenBullets * _effectsDisplayTime)
        {
            _gunLine.enabled = false;
        }

        if (Input.GetButton(LeftPowerAxisName) && _timer >= ActivePower.TimeBetweenBullets && Time.timeScale != 0)
        {
            _gunLine.startColor = ActivePower.LineColor;

            _activePower = Powers[0];
            Shoot();
        }
        else if (Input.GetButton(RightPowerAxisName) && _timer >= ActivePower.TimeBetweenBullets && Time.timeScale != 0)
        {
            _gunLine.startColor = ActivePower.LineColor;

            _activePower = Powers[1];
            Shoot();
        }
    }

    void Shoot()
    {
        _timer = 0f;

        _gunLine.enabled = true;
        _gunLine.SetPosition(0, GunEnd.position);

        CheckShootingRaycast();
        PlayShootingSound();
    }

    private void CheckShootingRaycast()
    {
        RaycastHit _hit;

        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out _hit, ActivePower.Range, _mask))
        {
            EnemyHealth enemyHealth = _hit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(ActivePower.Damage , _hit.point, ActivePower.PowerColor);
            }

            _gunLine.SetPosition(1, _hit.point);
        }
        else
        {
            _gunLine.SetPosition(1, _cam.transform.position + _cam.transform.forward * ActivePower.Range);
        }
    }

    void PlayShootingSound()
    {
        GunAudioSource.Play();
    }
}
