using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TerrainGenerator : MonoBehaviour
{
    public Terrain _terrain;
    public GameObject _terrainObj;
    public GameObject _enemyPrefab;
    public GameObject _obstaclePrefab;

    public int _terrainWidth = 100;
    public int _terrainHeight = 100;
    public int _terrainDepth = 20;
    public float _scale = 20f;
    public float _heightMultiplier = 10f;

    private float _offsetX;
    private float _offsetY;

    public int _obstacleCount = 20;
    public int _EnemyCount = 20;

    private List<Vector3> _placePos = new List<Vector3>();

    private void Awake()
    {
        Init();

        GenerateTerrain();
        PlaceObstacles();
    }

    private void Init()
    {
        var obj = Resources.Load<GameObject>("Prefabs/Terrain");
        _terrainObj = Instantiate(obj);
        _terrain = _terrainObj.GetComponent<Terrain>();
        _enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        _obstaclePrefab = Resources.Load<GameObject>("Prefabs/Obstacle");

        _offsetX = Random.Range(0f, 1000f);
        _offsetY = Random.Range(0f, 1000f);
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
                heights[x, y] = Mathf.PerlinNoise(xCoord + _offsetX, yCoord + _offsetY);
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

    private void PlaceObstacles()
    {
        TerrainData terrainData = _terrain.terrainData;

        for (int i = 0; i < _obstacleCount; i++)
        {
            // 랜덤 x, z 좌표 생성
            float x = Random.Range(0, _terrainWidth);
            float z = Random.Range(0, _terrainHeight);

            // x, z 좌표를 Terrain heightmap 해상도에 맞게 변환
            int xTerrain = (int)(x / terrainData.size.x * terrainData.heightmapResolution);
            int zTerrain = (int)(z / terrainData.size.z * terrainData.heightmapResolution);

            // 해당 위치의 높이 가져오기
            float y = terrainData.GetHeight(xTerrain, zTerrain);

            // 장애물 위치 설정
            Vector3 position = new Vector3(x, y, z);
            Instantiate(_obstaclePrefab, position, Quaternion.identity);

            _placePos.Add(position);
        }
    }

    public void PlaceEnemy()
    {
        TerrainData terrainData = _terrain.terrainData;

        for (int i = 0; i < _obstacleCount; i++)
        {
            // 랜덤 x, z 좌표 생성
            float x = Random.Range(0, _terrainWidth);
            float z = Random.Range(0, _terrainHeight);

            // x, z 좌표를 Terrain heightmap 해상도에 맞게 변환
            int xTerrain = (int)(x / terrainData.size.x * terrainData.heightmapResolution);
            int zTerrain = (int)(z / terrainData.size.z * terrainData.heightmapResolution);

            // 해당 위치의 높이 가져오기
            float y = terrainData.GetHeight(xTerrain, zTerrain);

            // 장애물 위치 설정
            Vector3 position = new Vector3(x, y, z);

            bool isEqual = false;
            foreach (Vector3 pos in _placePos)
            {
                if (pos == position)
                {
                    isEqual = true;
                    break;
                }
            }

            if (isEqual)
            {
                ++_obstacleCount;
                continue;
            }

            Instantiate(_enemyPrefab, position, Quaternion.identity);
        }
    }
}
