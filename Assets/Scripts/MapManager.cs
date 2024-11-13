using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private TerrainGenerator _terrainGenerator;
    private NavMeshSurface[] _navMeshSurfaces;

    private void Awake()
    {
        _terrainGenerator = gameObject.GetComponent<TerrainGenerator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitMapBake();
        InitEnemy();
    }

    public void InitMapBake()
    {
        _navMeshSurfaces = _terrainGenerator._terrainObj.GetComponentsInChildren<NavMeshSurface>();

        foreach (NavMeshSurface surface in _navMeshSurfaces)
        {
            surface.BuildNavMesh();
        }
    }

    public void InitEnemy()
    {
        _terrainGenerator.PlaceEnemy();
    }
}
