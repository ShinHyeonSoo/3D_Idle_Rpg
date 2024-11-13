using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour
{
    private TerrainGenerator _terrainGenerator;
    private NavMeshSurface[] _navMeshSurfaces;

    private void Awake()
    {
        _terrainGenerator = gameObject.GetComponent<TerrainGenerator>();
        _terrainGenerator.Init();
        Init();
    }

    public void Init()
    {
        InitMapBake();
        InitEnemy();
        InitPlayer();
    }

    private void InitMapBake()
    {
        _navMeshSurfaces = _terrainGenerator._terrainObj.GetComponentsInChildren<NavMeshSurface>();

        foreach (NavMeshSurface surface in _navMeshSurfaces)
        {
            surface.BuildNavMesh();
        }
    }

    private void InitEnemy()
    {
        _terrainGenerator.PlaceEnemy();
    }

    private void InitPlayer()
    {
        NavMeshAgent agent = CharacterManager.Instance.Player.NavMeshAgent;
        Vector3 position = agent.transform.position;
        NavMeshHit hit;

        // 현재 위치의 NavMesh 위에 해당하는 위치를 찾음
        if (NavMesh.SamplePosition(position, out hit, 100f, NavMesh.AllAreas))
        {
            // Y 위치를 NavMesh 위치에 맞춤
            position.y = hit.position.y;
            agent.transform.position = position;
        }
        agent.enabled = true;
    }
}
