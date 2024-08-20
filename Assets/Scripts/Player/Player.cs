using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(HealthDisplay))]
public class Player : MonoBehaviour
{
    private Animator _animator;

    [Header("Movement")]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotateSpeed;

    private CharacterController _characterController;
    private PlayerMovement _playerMovement;
    

    [Header("Health")]
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private int _health;

    private HealthDisplay _healthDisplay;
    private PlayerHealth _playerHealth;


    [Header("Combat")]
    [SerializeField] private int _damage;
    private PlayerCombatSystem _playerCombatSystem;
    private Fist[] _fists;

    public void Initialize()
    {
        _characterController = GetComponent<CharacterController>();
        _healthDisplay = GetComponent<HealthDisplay>();
        _animator = GetComponent<Animator>();
        _fists = GetComponentsInChildren<PlayerFist>();

        _playerMovement = new PlayerMovement(_animator, _characterController, _movementSpeed, _rotateSpeed);
        _playerHealth = new PlayerHealth(_animator, _health);
        _playerCombatSystem = new PlayerCombatSystem(_animator, _fists);

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
        _playerMovement.MovePlayer(new Vector3(_joystick.Horizontal, 0, _joystick.Vertical));
    }

    public void Attack(bool isAttacking)
    {
        _playerCombatSystem.Attack(isAttacking);
    }

    public void RecieveDamage(int damage)
    {
        _playerHealth.RecieveDamage(damage);
    }

    private void OnDied()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
