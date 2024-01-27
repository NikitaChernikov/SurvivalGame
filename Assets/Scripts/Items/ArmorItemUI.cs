using UnityEngine;
using UnityEngine.UI;

public class ArmorItemUI : MonoBehaviour
{
    [SerializeField] private ArmorItem _armorItem;
    [SerializeField] private Image _fillImage;

    private void OnEnable()
    {
        _armorItem.OnCheckDistance += ArmorItem_OnCheckDistance;
    }

    private void OnDisable()
    {
        _armorItem.OnCheckDistance -= ArmorItem_OnCheckDistance;
    }

    private void ArmorItem_OnCheckDistance(float amount)
    {
        _fillImage.fillAmount = amount;
    }
}
