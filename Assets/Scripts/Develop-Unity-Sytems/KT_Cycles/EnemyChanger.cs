using System;
using TMPro;
using UnityEngine;

namespace KT_Cycles
{
    public class EnemyChanger : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private int _healthUpgradeMultiplier = 3;
        [SerializeField] private int _levelUpgradeAmount = 5;
        private Enemy[] _enemies;


        private void Start()
        {
            _enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        }

        public void FilterByHealthGreater()
        {
            if (!int.TryParse(_inputField.text.Trim(), out int minHealth))
            {
                return;
            }

            foreach (Enemy enemy in _enemies)
            {
                enemy.gameObject.SetActive(enemy.Health > minHealth);
            }
        }

        public void FilterByLevel()
        {
            if (!int.TryParse(_inputField.text.Trim(), out int targetLevel))
            {
                return;
            }

            foreach (Enemy enemy in _enemies)
            {
                enemy.gameObject.SetActive(enemy.Level == targetLevel);
            }
        }

        public void ResetAll()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.gameObject.SetActive(true);
                enemy.ResetStats();
            }
        }

        public void PromoteToBoss()
        {
            string targetName = _inputField.text;

            foreach (Enemy enemy in _enemies)
            {
                if (enemy.UnitName == targetName)
                {
                    enemy.SetStats("Boss", enemy.Health * _healthUpgradeMultiplier, enemy.Level + _levelUpgradeAmount);
                }
            }
        }
    }
}
