using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    [System.Serializable]
	public class Wave
	{
		public string Name;
        public int Count;
        public float Rate;
	}

    public enum SpawnState
    {
        Spawning, Waiting, Counting
    };

    public Transform[] EnemiesTransform;
    public Transform[] SpawnPoints;
    public float TimeBetweenWaves = 5f;

    private float _waveCountDown;
    private float _searchCountDown = 1f;
    private SpawnState _state = SpawnState.Counting;

    private int minWaveCount = 5;
    private int maxWaveCount = 10;
    private float minRate = 1f;
    private float maxRate = 2f;

    private bool EnemyIsAlive
    {
        get
        {
            _searchCountDown -= Time.deltaTime;

            if (!(_searchCountDown <= 0f)) return true;

            _searchCountDown = 1f;

            return GameObject.FindGameObjectWithTag("Enemy") != null;
        }
    }

    private void Start()
    {
        if (SpawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced");
        }

        _waveCountDown = TimeBetweenWaves;
    }

    private void Update()
    {
        if(_state == SpawnState.Waiting)
        {
            //Check if enemies are still alive
            if(!EnemyIsAlive)
            {
                //Begin a new round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(_waveCountDown <= 0)
        {
            if(_state != SpawnState.Spawning)
            {
                //Start spawning wave
                StartCoroutine(SpawnWave());
            }
        }
        else
        {
            _waveCountDown -= Time.deltaTime;
        }
    }

    private void WaveCompleted()
    {
        _state = SpawnState.Counting;
        _waveCountDown = TimeBetweenWaves;
    }

    IEnumerator SpawnWave()
    {
        _state = SpawnState.Spawning;

        //Spawn
        for(var i = 0; i < Random.Range(minWaveCount, maxWaveCount); i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f/ Random.Range(minRate, maxRate));
        }

        minWaveCount += 3;
        maxWaveCount += 3;

        minRate += 0.3f;
        maxRate += 0.3f;

        _state = SpawnState.Waiting;

        yield break;
    }

    private void SpawnEnemy()
    {
        var sp = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        var enemy = EnemiesTransform[Random.Range(0, EnemiesTransform.Length)];
        var instanciedEnemy = Instantiate(enemy, sp.position, sp.rotation);

        var sound = instanciedEnemy.gameObject.GetComponent<EnemySound>();

        if(sound)
            sound.PlayEnemyClip(sound.alertClip);
    }

}
