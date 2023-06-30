using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private Transform _coin;
    [SerializeField] private float _rotationSpeed;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        _coin.Rotate(new Vector3(0, _rotationSpeed, 0) * Time.deltaTime);
    }
}