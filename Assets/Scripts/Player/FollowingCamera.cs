using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private Transform _player;
    [SerializeField] private float _cameraMovementSpeed = 0.1f;

    private Vector3 _finalCameraPosition;

    private void Update()
    {
        _finalCameraPosition = _player.position + _cameraOffset;

        transform.position = Vector3.Lerp(transform.position, _finalCameraPosition, _cameraMovementSpeed);
        
    }
}
