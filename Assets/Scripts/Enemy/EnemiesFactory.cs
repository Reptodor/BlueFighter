using System.Collections.Generic;
using UnityEngine;

public class EnemiesFactory : MonoBehaviour
{
    [SerializeField] private Transform _enemyTarget;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _spawnDistance;

    private SpawnPointGenerator _spawnPointGenerator;
    private EnemySpawner _enemySpawner;

    public void Initialize(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;

        _spawnPointGenerator = new SpawnPointGenerator(_spawnDistance);
    }

    public List<Enemy> Create(int enemyCount)
    {
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < enemyCount; i++)
            enemies.Add(Create());

        return enemies;
    }

    private Enemy Create()
    {
        var spawnpoint = _spawnPointGenerator.Generate(_enemyTarget.transform.position);
        Enemy enemy = Instantiate(_enemy, spawnpoint, _enemy.transform.rotation);
        enemy.Initialize(_enemyTarget, this);

        enemy.gameObject.SetActive(false);

        return enemy;
    }
}
