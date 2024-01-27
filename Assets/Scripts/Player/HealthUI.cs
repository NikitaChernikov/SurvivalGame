using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private PlayerHealth _playerHealth;


    private void OnEnable()
    {
        _playerHealth.OnChangeHealth += PlayerHealth_OnChangeHealth;
    }

    private void OnDisable()
    {
        _playerHealth.OnChangeHealth -= PlayerHealth_OnChangeHealth;
    }

    private void PlayerHealth_OnChangeHealth(float amount)
    {
        _fillImage.fillAmount = amount;
    }
}
