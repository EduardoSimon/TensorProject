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
    public float DecreaseValueBeamPower = 0.1f;
    public float DecreaseValueGunshotPower = 1.0f;

    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _mask;
    private float _timer;
    private LineRenderer _gunLine;

    public Power ActivePower { get; private set; }

    private void Awake()
    {
        _gunLine = Gun.GetComponent<LineRenderer>();
        ActivePower = Powers[0];

        foreach (var power in Powers)
        {
            power.Awake();
        }
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
        _timer += Time.deltaTime;

        if (Input.GetButton(LeftPowerAxisName) && _timer >= ActivePower.TimeBetweenBullets && Powers[0].PowerQuantity > 0)
        {
            ActivePower = Powers[0];
            ActivePower.DecreasePower(1.0f);
            _gunLine.startColor = ActivePower.LineColor;
            Shoot();
        }
        else if (Input.GetButton(RightPowerAxisName) && _timer >= ActivePower.TimeBetweenBullets && Powers[1].PowerQuantity > 0)
        {
            ActivePower = Powers[1];
            ActivePower.DecreasePower(DecreaseValueBeamPower);
            _gunLine.startColor = ActivePower.LineColor;
            Shoot();
        }else if (ActivePower.PowerQuantity <= 0)
        {
            StartCoroutine(ReloadPower(ActivePower,ActivePower.ReloadingTime));
        }

    }

    private IEnumerator ReloadPower(Power power,float reloadingTime)
    {
        yield return new WaitForSeconds(reloadingTime);
        power.PowerQuantity = power.StartPowerQuantity;
    }

    void Shoot()
    {
        _timer = 0f;

        _gunLine.enabled = true;
        StartCoroutine(DissolveLineRenderer());
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
                enemyHealth.TakeDamage(ActivePower.Damage , _hit.point, ActivePower.PowerColor, _hit.normal);
            }

            _gunLine.SetPosition(1, _hit.point);
        }
        else
        {
            _gunLine.SetPosition(1, GunEnd.transform.position + _cam.transform.forward * ActivePower.Range);

        }
    }

    void PlayShootingSound()
    {
        GunAudioSource.Play();
    }

    IEnumerator DissolveLineRenderer()
    {
        yield return new WaitForEndOfFrame();
        _gunLine.enabled = false;
    }
}
