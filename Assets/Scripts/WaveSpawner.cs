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
    public Wave[] Waves;
    public Transform[] SpawnPoints;
    public float TimeBetweenWaves = 5f;

    private float _waveCountDown;
    private float _searchCountDown = 1f;
    private int _nextWave = 0;
    private SpawnState _state = SpawnState.Counting;

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
                StartCoroutine(SpawnWave(Waves[_nextWave]));
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

        if(_nextWave + 1 > Waves.Length - 1)
        {
            _nextWave = 0;
        }
        else
        {
            _nextWave++;
        }

    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.Name);
        _state = SpawnState.Spawning;

        //Spawn
        for(var i = 0; i < _wave.Count; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f/_wave.Rate);
        }

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
