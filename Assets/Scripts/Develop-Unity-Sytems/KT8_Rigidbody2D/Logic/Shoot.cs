using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletForce = 10f;
    [SerializeField] private float _fireRate = 0.25f;

    private float _nextFireTime;

    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.leftButton.wasPressedThisFrame && Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + _fireRate;
            Fire();
        }
    }

    private void Fire()
    {
        if (_bulletPrefab == null || _firePoint == null) return;

        Bullet bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

        Vector2 shootDirection = _firePoint.right * Mathf.Sign(transform.lossyScale.x);

        bullet.Launch(shootDirection, _bulletForce);
    }
}
