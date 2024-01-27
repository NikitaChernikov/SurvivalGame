using UnityEngine;

public class TrailGenerator : MonoBehaviour
{
    [SerializeField] private Transform _toPoint;
    [SerializeField] private GameObject _trailPrefab;
    [SerializeField] private int _trailObjectCount = 50;
    [SerializeField] private float _noiseScale = 10.0f;
    [SerializeField] private float _pathDeviation = 5.0f;

    private Vector2 _perlinOffset;
    //private Terrain terrain; // ���������� ��� �������� ������ �� Terrain

    void Start()
    {
        _perlinOffset = new Vector2(Random.Range(0f, 100f), Random.Range(0f, 100f));
        //terrain = Terrain.activeTerrain; // ��������� ������ �� �������� Terrain
        GenerateAmmoPath();
    }

    void GenerateAmmoPath()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = _toPoint.position;

        for (int i = 0; i < _trailObjectCount; i++)
        {
            float t = (float)i / (_trailObjectCount - 1);
            Vector3 basePosition = Vector3.Lerp(startPosition, endPosition, t);

            float perlinValue = Mathf.PerlinNoise(_perlinOffset.x + t * _noiseScale, _perlinOffset.y) * 2 - 1;
            Vector3 offset = new Vector3(perlinValue * _pathDeviation, 0, perlinValue * _pathDeviation);

            // ��������� ������ Terrain � ������ �����
            //Vector3 worldPosition = basePosition + offset;
            //worldPosition.y = terrain.SampleHeight(worldPosition) + terrain.GetPosition().y;

            // ��������� ������ ��� Terrain �� ��������� ������, ����� �� �� "��������" � �����
            //worldPosition.y += 0.5f; // ��������� �� �������� ��� �� ����������� ��� ������

            // �������� ������� � ������� � ������ ������ Terrain
            Instantiate(_trailPrefab, basePosition + offset, Quaternion.identity); //��� ���������� �������� �� y ��������� worldPosition
        }
    }
}
