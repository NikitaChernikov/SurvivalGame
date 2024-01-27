using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed;
    [Header("Rotation Settings")]
    [SerializeField] private Transform _playerModel;
    [SerializeField] private float _rotatingSpeed = 5f;
    [Header("Gravity Settings")]
    [SerializeField] private float _gravity = -9.81f; // Ускорение свободного падения
    [SerializeField] private float _groundDistance = 0.2f; // Расстояние от центра объекта до земли, чтобы считать, что он на земле
    [SerializeField] private LayerMask _groundMask; // Маска слоя, определяющая, что такое "земля"


    private Vector3 _movementDirection;
    private Vector3 _velocity; // Текущая вертикальная скорость
    private bool _isGrounded; // Находится ли персонаж на земле


    private void Update()
    {
        // Проверка, находится ли персонаж на земле
        _isGrounded = Physics.CheckSphere(_characterController.bounds.center, _groundDistance, _groundMask);

        // Обнуление вертикальной скорости при касании земли
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // Небольшое отрицательное значение, чтобы удерживать персонажа на земле
        }

        float _horizontalVector = Input.GetAxis("Horizontal");
        float _verticalVector = Input.GetAxis("Vertical");

        _movementDirection = new Vector3(_horizontalVector, 0, _verticalVector).normalized;

        if (_movementDirection != Vector3.zero)
        {
            _characterController.Move(_movementDirection * _movementSpeed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(_movementDirection);
            _playerModel.rotation = Quaternion.Slerp(_playerModel.rotation, targetRotation, _rotatingSpeed * Time.deltaTime);
        }

        // Применение гравитации
        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime); // Перемещение персонажа с учетом гравитации

    }
}
