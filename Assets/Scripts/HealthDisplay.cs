using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private GameObject _healthBar;

    [SerializeField] private Gradient _healthBarGradient;
    
    [SerializeField] private float _healthBarTime;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        LookAtCamera();
    }

    public void OnHealthChanged(float healthParcentage)
    {
        _healthBarFilling.fillAmount = healthParcentage;
        _healthBarFilling.color = _healthBarGradient.Evaluate(healthParcentage);
        ShowHealth();
    }

    private void ShowHealth()
    {
        _healthBar.SetActive(true);
        Invoke(nameof(HideHealth), _healthBarTime);
    }

    private void HideHealth()
    {
        _healthBar.SetActive(false);
    }

    private void LookAtCamera()
    {
        _healthBar.transform.LookAt(_camera.transform.position);
        _healthBar.transform.Rotate(0, 180, 0);
    }
}
