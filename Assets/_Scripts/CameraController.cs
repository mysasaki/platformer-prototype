using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _cameraLerp;
    [SerializeField] private Vector3 _offset;

    private void FixedUpdate()
    {
        if (!_player)
        {
            return;
        }

        var position = transform.position;
        Vector3 targetPosition = new(_player.position.x + _offset.x, position.y, position.z);
        Vector3 lerpPosition = Vector3.Lerp(position, targetPosition, _cameraLerp);

        transform.position = lerpPosition;
    }
} 
