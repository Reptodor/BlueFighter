using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(HealthDisplay))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthDisplay _healthDisplay;
    [SerializeField] private NavMeshAgent _enemy;
    [SerializeField] private float _distanceToPlayer = 2f;
    [SerializeField] private float _damageCooldown;
    [SerializeField] private bool _canAttack;

    private Player _player;
    private EnemyHealth _enemyHealth;
    private EnemiesFactory _enemyFactory;

    private Vector3 _spawnpoint;

    private int _health;
    private int _damage;

    public event Action<Enemy> Disabled;

    public void Initialize(Player player, EnemiesFactory enemiesFactory, int health, int damage)
    {
        _player = player;
        _enemyFactory = enemiesFactory;
        _damage = damage;
        _health = health;


        transform.LookAt(_player.transform.position);

        _spawnpoint = transform.position;
        StartCoroutine(SpawnCheck());
    }

    private void OnEnable()
    {
        _enemyHealth = new EnemyHealth(this, _enemyFactory, _health);
        _enemyHealth.HealthChanged += _healthDisplay.OnHealthChanged;
        _enemyHealth.Died += OnDied;
    }

    private void OnDisable()
    {
        _enemyHealth.HealthChanged -= _healthDisplay.OnHealthChanged;
        _enemyHealth.Died -= OnDied;
    }

    private void FixedUpdate()
    {
        Attack();
        ChasePlayer();
    }

    public void RecieveDamage(int damage)
    {
        _enemyHealth.RecieveDamage(damage);
    }

    private void ChasePlayer()
    {
        if (Vector3.Distance(_enemy.transform.position, _player.transform.position) > _distanceToPlayer)
            _enemy.SetDestination(_player.transform.position);
    }

    private void Attack()
    {
        //if (Vector3.Distance(_enemy.transform.position, _player.transform.position) < _damageDistance && _canAttack)
        //{
        //    //_player.Damaged?.Invoke(_damage);
        //    StartCoroutine(Cooldown());
        //}
    }

    private IEnumerator Cooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_damageCooldown);
        _canAttack = true;
    }

    private IEnumerator SpawnCheck()
    {
        yield return new WaitForSeconds(2f);

        if(transform.position == _spawnpoint)
        {
            _enemyFactory.DestroyEnemy(this);
            _enemyFactory.Create(1);
        }
    }

    private void OnDied()
    {
        gameObject.SetActive(false);
        Disabled?.Invoke(this);
    }
}
