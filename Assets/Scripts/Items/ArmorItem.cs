using UnityEngine;
using System;

public class ArmorItem : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _firstCorner;
    [SerializeField] private Transform _secondCorner;

    public event Action<float> OnCheckDistance;

    private float _maxDistance;
    private void Start()
    {
        _maxDistance = Vector3.Distance(_firstCorner.position, _secondCorner.position);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        float distanceToPlayerInPercent = (distanceToPlayer * 100 / _maxDistance) / 100;
        OnCheckDistance?.Invoke(distanceToPlayerInPercent);
    }
}
