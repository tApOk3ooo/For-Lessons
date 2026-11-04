using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _defaultTarget;

    private UnitSystem _unitSystem;
    private GameObject _instance;

    public void SpawnUnit(UnitDataSO data)
    {
        _instance = Instantiate(data.UnitPrefab, _spawnPoint.position, _spawnPoint.rotation);

        if (_instance.TryGetComponent(out UnitSystem unit))
        {
            unit.Initialize(data);
            unit.SetDefaultTarget(_defaultTarget);
        }
        else
        {
            print("UnitSystem не найден на юните");
        }
    }
}