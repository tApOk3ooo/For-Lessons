using TMPro;
using UnityEngine;

namespace KT7
{
    public class WalletManager : MonoBehaviour
    {
        [SerializeField] private float _money = 10000f;
        [SerializeField] private TextMeshProUGUI _moneyText;

        public float Money => _money;

        private void Start()
        {
            UpdateUI();
        }

        public bool TrySpend(float amount)
        {
            if (_money >= amount)
            {
                _money -= amount;
                UpdateUI();
                return true;
            }

            return false;
        }

        public void UpdateUI()
        {
            _moneyText.SetText(_money.ToString("F1") + " $");
        }
    }
}
