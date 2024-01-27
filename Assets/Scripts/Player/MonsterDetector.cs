using System;
using UnityEngine;

public class MonsterDetector : MonoBehaviour
{
    [SerializeField] private Transform _firstMapCorner;
    [SerializeField] private Transform _secondMapCorner;

    [SerializeField] Transform _monster;

    public event Action<float> OnChangeImage;

    private float _maxMapLength;
    private float _distanceToMonster;
    private float _distanceToDrawInPercent;

    private void Start()
    {
        _maxMapLength = Vector3.Distance(_firstMapCorner.position, _secondMapCorner.position);
    }

    private void Update()
    {
        _distanceToMonster = Vector3.Distance(transform.position, _monster.transform.position);
        _distanceToDrawInPercent = (_distanceToMonster * 100 / _maxMapLength)/100;

        OnChangeImage?.Invoke(_distanceToDrawInPercent);
    }
}
