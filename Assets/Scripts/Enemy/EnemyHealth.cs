public class EnemyHealth : Health
{
    private Enemy _enemy;
    private EnemiesFactory _enemiesFactory;
    private EnemySpawner _enemySpawner;
    private HealthDisplay _healthDisplay;

    public EnemyHealth(Enemy enemy, EnemiesFactory enemiesFactory, int health) : base(health)
    {
        _enemy = enemy;
        _enemiesFactory = enemiesFactory;
    }
}
