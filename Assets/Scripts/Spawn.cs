using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    [Serializable]
    public class Enemy
    {
        public GameObject enemyPrefab;
        public float timeToSpawn;

        [NonSerialized] public float _nextSpawn = 1f;
    }

    public Enemy[] enemyList;

    private float _posX, _posY, _spawnCooldown;
    private int _spawnerNum;

    private Vector3[] _spawners = new Vector3[4];

    private void Start()
    {
        _spawnCooldown = Time.time;
        _spawners = new[]
        {
            // Top
            new Vector3(Random.Range(-10, 10), 12, 0),
            // Right
            new Vector3(12, Random.Range(-10, 10), 0),
            // Bottom
            new Vector3(Random.Range(-10, 10), -12, 0),
            // Left
            new Vector3(-12, Random.Range(-10, 10), 0)
        };
    }

    private void Update()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach (var enemy in enemyList)
        {
            if (Time.time >= enemy._nextSpawn)
            {
                Instantiate(enemy.enemyPrefab, _spawners[(int) (Random.value * 4)], Quaternion.identity);
                enemy._nextSpawn = Time.time + enemy.timeToSpawn;
                _spawners = new[]
                {
                    // Top
                    new Vector3(Random.Range(-10, 10), 12, 0),
                    // Right
                    new Vector3(12, Random.Range(-10, 10), 0),
                    // Bottom
                    new Vector3(Random.Range(-10, 10), -12, 0),
                    // Left
                    new Vector3(-12, Random.Range(-10, 10), 0)
                };
            }
        }
    }
}