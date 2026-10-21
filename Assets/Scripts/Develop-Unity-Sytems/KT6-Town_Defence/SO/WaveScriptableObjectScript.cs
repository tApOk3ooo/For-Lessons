using UnityEngine;

[CreateAssetMenu(fileName = "Wave-Data", menuName = "Tower Defence Data/Wave")]
public class WaveScriptableObjectScript : ScriptableObject
{
    [SerializeField] private int _baseUnitPerWave;
    [SerializeField] private float _unitPerWaveMultiplyer;


    public float BaseUnitPerWave => _baseUnitPerWave;
    public float UnitPerWaveMultiplyer => _unitPerWaveMultiplyer;
}