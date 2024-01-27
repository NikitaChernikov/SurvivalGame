using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action<float> OnChangeHealth;

    private int health;
    private ItemPicker _itemPicker;

    private void Awake()
    {
        _itemPicker = GetComponent<ItemPicker>();
    }

    private void Start()
    {
        ChangeHealth(10);
    }

    private void OnEnable()
    {
        _itemPicker.OnPickedItem += ItemPicker_OnPickedItem;
    }
    private void OnDisable()
    {
        _itemPicker.OnPickedItem -= ItemPicker_OnPickedItem;
    }

    public void ChangeHealth(int amount)
    {
        health += amount;
        float healthInPercents = health / 100f;
        OnChangeHealth?.Invoke(healthInPercents);
        if (health <= 0)
        {
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private void ItemPicker_OnPickedItem(System.Enum item)
    {
        switch (item)
        {
            case ItemsEnum.armor:
                ChangeHealth(99);
                break;
            default:
                break;
        }
    }
}
