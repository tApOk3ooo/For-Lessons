using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _levelAmount;

    [SerializeField] private DataLevelSO[] allDataLevels;

    [SerializeField] private GameObject _levelButtonPrefab;

    [SerializeField] private Transform _levelContent;

    private void CreateLevels()
    {
        foreach (DataLevelSO item in allDataLevels)
        {
            if (item == null)
            {
                Debug.Log("Вы пропустили поле в списке:" + item);
            }
            GameObject levelObject = Instantiate(_levelButtonPrefab, _levelContent);

            // дописать после создания скрипта ui для экземплаяров левелов
        }
    }
}
