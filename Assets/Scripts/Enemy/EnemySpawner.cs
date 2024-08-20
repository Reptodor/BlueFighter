using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _enemiesInPoolCount;
    [SerializeField] private float _timeBetweenSpawn;

    private EnemiesFactory _enemiesFactory;
    private List<Enemy> _enemiesInPool;

    private float _counter = 20f;

    private int _spawnedCount = 0;

    public void Initialize(EnemiesFactory enemiesFactory)
    {
        _enemiesFactory = enemiesFactory;
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
        _spawnedCount--;
        _counter = _timeBetweenSpawn;
    }

    private void Spawn()
    {
        if (_spawnedCount < _enemiesInPoolCount)
        {
            Enemy enemy  = _enemiesInPool.First();
            enemy.gameObject.SetActive(true);
            _enemiesInPool.Remove(enemy);

            _spawnedCount++;
        }
    }

}
