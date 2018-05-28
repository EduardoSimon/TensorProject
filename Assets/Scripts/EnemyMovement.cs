using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    Transform _destination;

    NavMeshAgent _navMeshAgent;
    public float MinSpeed = 8f;
    public float MaxSpeed = 16f;

    private float speed;

    void Awake()
    {
        _destination = GameObject.FindGameObjectWithTag("Player").transform;
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        speed = Random.Range(MinSpeed, MaxSpeed);
    }

    void Start()
    {
        _navMeshAgent.speed = speed;
    }

    void Update()
    {
        if(_navMeshAgent.isActiveAndEnabled)
            _navMeshAgent.SetDestination(_destination.position);
    }
}
