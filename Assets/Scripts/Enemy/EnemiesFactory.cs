using System.Collections.Generic;
using UnityEngine;

public class EnemiesFactory : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _spawnDistance;

    private SpawnPointGenerator _spawnPointGenerator;
    private EnemySpawner _enemySpawner;
    private Player _player;

    private List<Enemy> _enemies;

    public List<Enemy> Enemies() => _enemies;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
        _enemySpawner = FindAnyObjectByType<EnemySpawner>();

        _spawnPointGenerator = new SpawnPointGenerator(_spawnDistance);

        _enemies = new List<Enemy>();
    }

    public void DestroyEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        _enemySpawner.SpawnedCount--;
        Destroy(enemy.gameObject);
    }

    public List<Enemy> Create(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
            _enemies.Add(Create());

        return _enemies;
    }

    private Enemy Create()
    {
        var spawnpoint = _spawnPointGenerator.Generate(_player.transform.position);
        Enemy enemy = Instantiate(_enemy, spawnpoint, Quaternion.identity);
        enemy.Initialize(_player, this, _health, _damage);

        enemy.gameObject.SetActive(false);

        return enemy;
    }
}
