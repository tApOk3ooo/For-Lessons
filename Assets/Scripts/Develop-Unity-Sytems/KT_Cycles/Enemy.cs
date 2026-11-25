using TMPro;
using UnityEngine;

namespace KT_Cycles
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _levelText;

        public string UnitName { get; private set; }
        public int Health { get; private set; }
        public int Level { get; private set; }

        private string _initialName;
        private int _initialHealth;
        private int _initialLevel;

        private void Awake()
        {
            UnitName = _nameText.text;
            int.TryParse(_healthText.text, out int hp);
            int.TryParse(_levelText.text, out int lvl);

            Health = hp;
            Level = lvl;

            _initialName = UnitName;
            _initialHealth = Health;
            _initialLevel = Level;
        }

        public void SetStats(string newName, int newHealth, int newLevel)
        {
            UnitName = newName;
            Health = newHealth;
            Level = newLevel;

            _nameText.text = UnitName;
            _healthText.text = Health.ToString();
            _levelText.text = Level.ToString();
        }

        public void ResetStats()
        {
            SetStats(_initialName, _initialHealth, _initialLevel);
        }
    }
}
