using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Ammo")]
    [SerializeField] private Image _ammoImage;
    [Header("Armor")]
    [SerializeField] private GameObject _armorDetector;
    [SerializeField] private Image _armorImage;
    [Header("Pistol")]
    [SerializeField] private Image _pistolImage;

    [SerializeField] private ItemPicker _itemPicker;

    private void OnEnable()
    {
        _itemPicker.OnPickedItem += ItemPicker_OnPickedItem;
    }
    private void OnDisable()
    {
        _itemPicker.OnPickedItem -= ItemPicker_OnPickedItem;
    }

    private void ItemPicker_OnPickedItem(System.Enum item)
    {
        switch (item)
        {
            case ItemsEnum.ammo:
                _ammoImage.color = Color.red;
                break;
            case ItemsEnum.armor:
                _armorImage.color = Color.red;
                _armorDetector.SetActive(false);
                break;
            case ItemsEnum.pistol:
                _pistolImage.color = Color.red;
                break;

        }
    }
}
