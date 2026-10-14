using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _levelAmount;

    [SerializeField] private DataLevelSO[] allDataLevels;

    [SerializeField] private GameObject _levelButtonPrefab;

    [SerializeField] private Transform _levelContent;
    [SerializeField] private TextMeshProUGUI _levelAmountText;

    private void Start()
    {
        CreateLevels();
    }

    private void CreateLevels()
    {
        foreach (DataLevelSO item in allDataLevels)
        {
            if (item == null)
            {
                Debug.Log("Вы пропустили поле в списке:" + item);
            }
            GameObject levelObject = Instantiate(_levelButtonPrefab, _levelContent);

            LevelCardUI currentCard = levelObject.GetComponent<LevelCardUI>();

            currentCard.Setup(item, this);
        }
    }
}
