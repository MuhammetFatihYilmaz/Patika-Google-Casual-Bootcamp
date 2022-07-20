using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Camera _mainCamera;
    //
    [SerializeField] private GameObject _meteorPrefab;
    [SerializeField] private float _radius;
    private Vector3 _randomSpawnPos;


    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_randomSpawnPos, _radius);
    }

    public void SpawnMeteor()
    {
        _randomSpawnPos = Random.insideUnitSphere * _radius;
        Instantiate(_meteorPrefab, _randomSpawnPos, Quaternion.identity);
    }

}
