using UnityEngine;

public class LevelInitializator : MonoBehaviour
{
    [SerializeField] private EnemiesFactory _enemiesFactory;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Player _player;

    private void Awake()
    {
        _enemiesFactory.Initialize(_enemySpawner);
        _enemySpawner.Initialize(_enemiesFactory);
        _player.Initialize();

        Time.timeScale = 0;
    }
}
