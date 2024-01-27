using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _detectionDistance;
    [SerializeField] private Transform _playerObj;
    [SerializeField] private float _shootingDistance;
    [SerializeField] private GameObject _weaponModel;

    private GameObject[] _enemies;
    private ItemPicker _itemPicker;
    private bool hasPistol = false;
    private bool hasAmmo = false;
    private Transform _closestEnemy;

    private void Awake()
    {
        _itemPicker = GetComponent<ItemPicker>();
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Start()
    {
        _closestEnemy = _enemies[0].transform;
    }

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
            case ItemsEnum.pistol:
                hasPistol = true;
                _weaponModel.SetActive(true);
                break;
            case ItemsEnum.ammo:
                hasAmmo = true;
                break;
        }
        
    }

    private void Update()
    {
        if (hasAmmo && hasPistol)
        {
            float closestDistance = Vector3.Distance(transform.position, _enemies[0].transform.position);
            foreach (GameObject enemy in _enemies)
            {
                float checkDistance = Vector3.Distance(transform.position, enemy.transform.position);
                if (checkDistance < closestDistance && !enemy.GetComponent<MonsterHealth>().IsDead())
                {
                    _closestEnemy = enemy.transform;
                    closestDistance = checkDistance;
                    Debug.Log(_closestEnemy);
                }
            }
            if (closestDistance <= _detectionDistance)
            {
                _playerObj.LookAt(_closestEnemy.position + new Vector3(0,1,0));
            }

            RaycastHit hitObj;
            Ray ray = new Ray(_playerObj.transform.position, _playerObj.transform.forward);
            Debug.DrawRay(_playerObj.transform.position, _playerObj.transform.forward * _shootingDistance, Color.red);
            if (Physics.Raycast(ray, out hitObj, _shootingDistance))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hitObj.collider.CompareTag("Enemy"))
                    {
                        hitObj.transform.GetComponent<MonsterHealth>().GetDamage(20);
                    }
                }
            }
        }
    }
}
