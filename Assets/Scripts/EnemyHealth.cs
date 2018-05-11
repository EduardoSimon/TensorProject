using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class EnemyHealth : MonoBehaviour
{
    public int Health = 100;
    public int ScoreValue = 10;
    public Power.DamageColor EnemyColor;
    public float DestroyTime;
    public float ForceMultiplier;

    private Material _material;
    private EnemySound _sound;
    private Rigidbody _rb;
    private NavMeshAgent _agent;

    private void Start()
    {
        _sound = GetComponent<EnemySound>();
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _material = GetComponentInChildren<MeshRenderer>().material;
    }

    public void TakeDamage(int amount, Vector3 hitPoint, Power.DamageColor activePowerColor)
    {
        if (activePowerColor != EnemyColor) return;

        Health -= amount;

        if (Health <= 0)
        {
            _agent.enabled = false;
            _rb.isKinematic = false;
            //_rb.AddForce(new Vector3(0,-1,0) * ForceMultiplier,ForceMode.Force);
            _sound.PlayEnemyClip(_sound.destroyedClip);

            StartCoroutine(Dissolve());
        }
    }

    private IEnumerator Dissolve()
    {
        yield return new WaitForSeconds(1.0f);

        float percent = 0;

        while (percent < 1)
        {
            _material.SetFloat("_Dissolve_Amount",percent);
            percent += 0.01f;
            yield return null;
        }
        ScoreManager.score += ScoreValue;
        Destroy(gameObject);
    }
}
