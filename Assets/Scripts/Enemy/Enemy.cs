using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HealthDisplay))]
[RequireComponent (typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthDisplay _healthDisplay;
    [SerializeField] private NavMeshAgent _enemy;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDistance = 2f;

    private Fist[] _fists;
    private Transform _enemyTarget;
    private EnemyHealth _enemyHealth;
    private EnemiesFactory _enemyFactory;
    private EnemyMovement _enemyMovement;
    private EnemyCombatSystem _enemyCombatSystem;
    private Animator _animator;

    private Vector3 _spawnpoint;

    public event Action<Enemy> Disabled;

    public void Initialize(Transform enemyTarget, EnemiesFactory enemiesFactory)
    {
        _enemyTarget = enemyTarget;
        _enemyFactory = enemiesFactory;

        _spawnpoint = transform.position;
        StartCoroutine(SpawnCheck());

        _animator = GetComponent<Animator>();
        _fists = GetComponentsInChildren<EnemyFist>();

        _enemyMovement = new EnemyMovement(_enemy, _animator, _enemyTarget);
        _enemyCombatSystem = new EnemyCombatSystem(_animator);
        foreach (var fist in _fists)
        {
            fist.Initialize(_damage);
            fist.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _enemyHealth = new EnemyHealth(_animator, _health);
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
        ChooseAction();
    }

    public void RecieveDamage(int damage)
    {
        _enemyHealth.RecieveDamage(damage);
    }

    private void ChooseAction()
    {
        if (Vector3.Distance(_enemy.transform.position, _enemyTarget.transform.position) > _attackDistance)
        {
            _enemyMovement.ChasePlayer(_fists);
        }
        else
        {
            _enemyCombatSystem.Attack(_fists);
        }
    }

    private IEnumerator SpawnCheck()
    {
        yield return new WaitForSeconds(2f);

        if (transform.position == _spawnpoint)
        {
            OnDied();
            _enemyFactory.Create(1);
        }
    }

    private void OnDied()
    {
        Disabled?.Invoke(this);
        gameObject.SetActive(false);
    }
}
