using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(HealthDisplay))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotateSpeed;

    private CharacterController _characterController;
    private PlayerMovement _playerMovement;

    [Header("Health")]
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private int _health;

    private HealthDisplay _healthDisplay;
    private PlayerHealth _playerHealth;

    private Animator _animator;

    [Header("Combat")]
    [SerializeField] private int _damage;
    [SerializeField] private float _damageDistance;
    private Fist[] _fists;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _healthDisplay = GetComponent<HealthDisplay>();
        _animator = GetComponent<Animator>();
        _fists = GetComponentsInChildren<Fist>();

        _playerMovement = new PlayerMovement(_characterController, _movementSpeed, _rotateSpeed);
        _playerHealth = new PlayerHealth(_health);

        foreach (var fist in _fists)
        {
            fist.Initialize(_damage);
            fist.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _playerHealth.HealthChanged += _healthDisplay.OnHealthChanged;
        _playerHealth.Died += OnDied;
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= _healthDisplay.OnHealthChanged;
        _playerHealth.Died -= OnDied;
    }

    private void Update()
    {
        _playerMovement.MovePlayer(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        Attack();
    }

    public void RecieveDamage(int damage)
    {
        _playerHealth.RecieveDamage(damage);
    }

    private void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            _animator.SetBool("Attacking", true);
            foreach (var fist in _fists)
            {
                fist.gameObject.SetActive(true);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            _animator.SetBool("Attacking", false);
            foreach (var fist in _fists)
            {
                fist.gameObject.SetActive(false);
            }
        }

    }

    private void OnDied()
    {
        _deathMenu.SetActive(true);
    }
}
