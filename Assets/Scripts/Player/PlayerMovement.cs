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
    [SerializeField] private float _gravity = -9.81f; // ��������� ���������� �������
    [SerializeField] private float _groundDistance = 0.2f; // ���������� �� ������ ������� �� �����, ����� �������, ��� �� �� �����
    [SerializeField] private LayerMask _groundMask; // ����� ����, ������������, ��� ����� "�����"


    private Vector3 _movementDirection;
    private Vector3 _velocity; // ������� ������������ ��������
    private bool _isGrounded; // ��������� �� �������� �� �����


    private void Update()
    {
        // ��������, ��������� �� �������� �� �����
        _isGrounded = Physics.CheckSphere(_characterController.bounds.center, _groundDistance, _groundMask);

        // ��������� ������������ �������� ��� ������� �����
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // ��������� ������������� ��������, ����� ���������� ��������� �� �����
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

        // ���������� ����������
        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime); // ����������� ��������� � ������ ����������

    }
}
