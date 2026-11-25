using UnityEngine;


public class CubeBubbleSorter : MonoBehaviour
{
    [SerializeField] private Transform[] _cubes;
    [SerializeField] private Vector3 _startPosition = Vector3.zero;
    [SerializeField] private float _spacing = 0.5f;

    private void Start()
    {
        BubbleSortDescending();
        ArrangeInLine();
    }

    private void BubbleSortDescending()
    {
        for (int i = 0; i < _cubes.Length - 1; i++)
        {
            for (int j = 0; j < _cubes.Length - 1 - i; j++)
            {
                if (GetVolume(_cubes[j]) < GetVolume(_cubes[j + 1]))
                {
                    (_cubes[j], _cubes[j + 1]) = (_cubes[j + 1], _cubes[j]);
                }
            }
        }
    }

    private void ArrangeInLine()
    {
        float offset = 0f;

        for (int i = 0; i < _cubes.Length; i++)
        {
            float halfWidth = _cubes[i].localScale.x * 0.5f;
            offset += (i == 0) ? halfWidth : _spacing + halfWidth;

            Vector3 position = _startPosition + Vector3.right * offset;
            position.y = _startPosition.y + _cubes[i].localScale.y * 0.5f;
            _cubes[i].position = position;

            offset += halfWidth;
        }
    }

    private float GetVolume(Transform t) => t.localScale.x * t.localScale.y * t.localScale.z;
}
