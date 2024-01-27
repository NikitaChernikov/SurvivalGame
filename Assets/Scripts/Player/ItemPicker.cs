using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ItemPicker : MonoBehaviour 
{
    private static string AMMO_BOX_TAG = "AmmoBox";
    private static string ARMOR_BODY_TAG = "ArmorBody";
    private static string WEAPON_ANGAR_TAG = "WeaponAngar";

    public event Action<Enum> OnPickedItem;

    [SerializeField] private NavMeshAgent _navMeshAgent;

    private Transform _itemWayPoint;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }


    private void OnTriggerEnter(Collider other)
    {
        _itemWayPoint = other.transform.GetChild(0);
        StartCoroutine(GoToItem(other.gameObject));
        if (other.CompareTag(AMMO_BOX_TAG))
        {
            OnPickedItem?.Invoke(ItemsEnum.ammo);
        }
        else if (other.CompareTag(ARMOR_BODY_TAG))
        {
            OnPickedItem?.Invoke(ItemsEnum.armor);
        }
        else if (other.CompareTag(WEAPON_ANGAR_TAG))
        {
            OnPickedItem?.Invoke(ItemsEnum.pistol);
        }
    }

    private IEnumerator GoToItem(GameObject item)
    {
        _navMeshAgent.enabled = true;
        _playerMovement.enabled = false;
        _navMeshAgent.SetDestination(_itemWayPoint.position);
        yield return new WaitForSeconds(3f);
        _navMeshAgent.enabled = false;
        _playerMovement.enabled = true;
        item.GetComponent<Collider>().enabled = false;
    }
}
