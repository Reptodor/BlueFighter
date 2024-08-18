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
    

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _healthDisplay = GetComponent<HealthDisplay>();

        _playerMovement = new PlayerMovement(_characterController, _movementSpeed, _rotateSpeed);
        _playerHealth = new PlayerHealth(_health);
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
    }

    public void RecieveDamage(int damage)
    {
        _playerHealth.RecieveDamage(damage);
    }

    private void OnDied()
    {
        _deathMenu.SetActive(true);
    }
}
