using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player Tracking")]
    [SerializeField] private Player _player;
    [SerializeField] private float _playerLogInterval = 3f;
    public Func<int> PlayerHealthInfo;

    [Header("Enemies")]
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _enemyCount = 3;
    [SerializeField] private float _enemyDamageInterval = 2f;
    public event Action<int> EnemyDamageTick;

    private void Start()
    {
        PlayerHealthInfo = _player.TakeInfo;

        SpawnEnemies();

        StartCoroutine(LogPlayerHealthRoutine());
        StartCoroutine(DamageEnemiesRoutine());
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            Vector3 spawnPosition = new Vector3((i - 1) * 2f, 0, 0);

            Enemy enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);

            enemy.name = $"Enemy_{i + 1}";

            enemy.Init(this);
        }
    }

    private IEnumerator LogPlayerHealthRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_playerLogInterval);

            int hp = PlayerHealthInfo.Invoke();
            print($"Здоровье игрока: {hp}");
        }
    }

    private IEnumerator DamageEnemiesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_enemyDamageInterval);

            int randomDamage = UnityEngine.Random.Range(5, 15);
            Debug.Log($"Нанесение урона всем врагам: {randomDamage}");
            EnemyDamageTick?.Invoke(randomDamage);
        }
    }
}
