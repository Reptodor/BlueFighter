using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemiesFactory _enemiesFactory;
    [SerializeField] private int _enemiesInPoolCount;
    [SerializeField] private float _timeBetweenSpawn;

    private List<Enemy> _enemiesInPool;

    private float _counter = 20f;

    [HideInInspector] public int SpawnedCount = 0;

    private void Awake()
    {
        _enemiesInPool = new List<Enemy>(_enemiesFactory.Create(_enemiesInPoolCount));
    }

    private void OnEnable()
    {
        _enemiesInPool.ForEach(enemy => enemy.Disabled += OnEnemyDisabled);
    }

    private void OnDisable()
    {
        _enemiesInPool.ForEach(enemy => enemy.Disabled -= OnEnemyDisabled);
    }

    private void Update()
    {
        _counter += Time.deltaTime;
        if (_counter > _timeBetweenSpawn)
        {
            Spawn();
            _counter = 0;
        }
    }

    private void OnEnemyDisabled(Enemy enemy)
    {
        _enemiesInPool.Add(enemy);
        _counter = _timeBetweenSpawn;
    }

    private void Spawn()
    {
        if (SpawnedCount < 5)
        {
            Enemy enemy  = _enemiesInPool.First();
            enemy.gameObject.SetActive(true);
            _enemiesInPool.Remove(enemy);

            SpawnedCount++;
        }
    }

}
