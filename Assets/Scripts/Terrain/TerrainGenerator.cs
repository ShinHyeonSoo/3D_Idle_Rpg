using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TerrainGenerator : MonoBehaviour
{
    public Terrain _terrain;

    public int _terrainWidth = 100;
    public int _terrainHeight = 100;
    public int _terrainDepth = 20;
    public float _scale = 20f;
    public float _heightMultiplier = 10f;

    public GameObject _terrainPrefab;

    void Start()
    {
        GenerateTerrain();
        GenerateTerrain(_terrainPrefab);
    }

    private void GenerateTerrain()
    {
        TerrainData terrainData = _terrain.terrainData;
        terrainData.heightmapResolution = _terrainWidth + 1;
        terrainData.size = new Vector3(_terrainWidth, _terrainDepth, _terrainHeight);

        float[,] heights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int y = 0; y < terrainData.heightmapResolution; y++)
            {
                float xCoord = (float)x / terrainData.heightmapResolution * _scale;
                float yCoord = (float)y / terrainData.heightmapResolution * _scale;
                heights[x, y] = Mathf.PerlinNoise(xCoord, yCoord);
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }

    private void GenerateTerrain(GameObject obj)
    {
        for (int x = 0; x < _terrainWidth; x++)
        {
            for (int z = 0; z < _terrainDepth; z++)
            {
                // 노이즈 값을 사용해 높이 설정
                float y = Mathf.PerlinNoise(x / _scale, z / _scale) * _heightMultiplier;

                Vector3 position = new Vector3(x, y, z);
                Instantiate(obj, position, Quaternion.identity);
            }
        }
    }
}
