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

    public Camera _cam;
    public LayerMask _mask;
    private float _timer;
    private LineRenderer _gunLine;

    public Power ActivePower { get; private set; }

    public ParticleSystem gunParticles;
    public Light gunLight;
    public Light faceLight;

    private void Awake()
    {
        gunParticles = Gun.GetComponent<ParticleSystem>();

        _gunLine = Gun.GetComponent<LineRenderer>();
        ActivePower = Powers[0];

        foreach (var power in Powers)
        {
            power.Awake();
            StartCoroutine(ReloadPower(power,power.ReloadingTime,power.ReloadingRate));
        }

        gunParticles.Stop();

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
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetButton(LeftPowerAxisName) && _timer >= ActivePower.TimeBetweenBullets && Powers[0].PowerQuantity > (0 + Powers[0].DecreasingRate) && Time.timeScale > 0)
        {
            power1ParticleColor();
            gunParticles.Play();

            ActivePower = Powers[0];
            ActivePower.DecreasePower(ActivePower.DecreasingRate);
            _gunLine.startColor = ActivePower.LineColor;
            Shoot();
        }
        else if (Input.GetButton(RightPowerAxisName) && _timer >= ActivePower.TimeBetweenBullets &&
                 Powers[1].PowerQuantity > (0 + Powers[1].DecreasingRate) && Time.timeScale > 0)
        {
            power2ParticleColor();
            gunParticles.Play();

            ActivePower = Powers[1];
            ActivePower.DecreasePower(ActivePower.DecreasingRate);
            _gunLine.startColor = ActivePower.LineColor;
            Shoot();
        }

        if (ActivePower == Powers[1] && !Input.GetButton(RightPowerAxisName))
        {
            GunAudioSource.Stop();
        }
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
            EnemyHealth enemyHealth = _hit.collider.transform.parent.GetComponent<EnemyHealth>();

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
        GunAudioSource.clip = ActivePower.ShootingAudioClip;

        if (ActivePower == Powers[1])
        {
            if (!GunAudioSource.isPlaying)
                GunAudioSource.Play();
        }
        else
        {
            GunAudioSource.Play();
        }
    }

    IEnumerator DissolveLineRenderer()
    {
        yield return new WaitForEndOfFrame();
        _gunLine.enabled = false;
    }

    private IEnumerator ReloadPower(Power power, float reloadingTime, float nToIncreasePower)
    {
        while (true)
        {
            yield return null;

            if(power.PowerQuantity <= 100)
            {
                power.PowerQuantity += nToIncreasePower;
            }
        }
    }

    void power1ParticleColor()
    {
        var col = gunParticles.colorOverLifetime;
        col.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color32(241, 143, 1, 255), 0.0f), new GradientColorKey(new Color32(255, 0, 0, 255), 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) });

        col.color = grad;
    }

    void power2ParticleColor()
    {
        var col = gunParticles.colorOverLifetime;
        col.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color32(45, 76, 117, 255), 0.0f), new GradientColorKey(new Color32(0, 141, 255, 255), 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) });

        col.color = grad;
    }
}
