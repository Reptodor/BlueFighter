using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private GameObject _healthBar;

    [SerializeField] private Gradient _healthBarGradient;
    
    [SerializeField] private float _healthBarTime;

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
}
