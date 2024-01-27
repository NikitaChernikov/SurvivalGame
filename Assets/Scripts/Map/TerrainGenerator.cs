using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 256; // Ширина ландшафта
    public int depth = 20; // Максимальная высота холмов
    public int height = 256; // Длина ландшафта

    public float scale = 20; // Масштаб ландшафта

    // Добавленные параметры для более плавного ландшафта
    public float offsetX = 100f;
    public float offsetY = 100f;

    void Start()
    {
        // Создаем новый seed для генерации шума
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        // Используем offsetX и offsetY для смещения шума, чтобы избежать повторяющихся узоров
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        // Mathf.PerlinNoise возвращает значение от 0.0 до 1.0
        // Умножаем на 2 и вычитаем 1, чтобы получить диапазон от -1 до 1
        // Затем умножаем на 'depth' для регулировки высоты ландшафта
        return (Mathf.PerlinNoise(xCoord, yCoord) * 2 - 1) * depth;
    }
}
