using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 MinimumValues, MaximumValues;
    [Range(1, 10)]
    [SerializeField] private float _smoothFactor;

    public void LateUpdate() => Follow();

    private void Follow()
    {
        transform.position = Vector3.Lerp(transform.position, CameraBound(), _smoothFactor * Time.fixedDeltaTime);
    }

    private Vector3 CameraBound()
    {
        var position = _player.transform.position + _offset;

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(position.x, MinimumValues.x, MaximumValues.x),
            Mathf.Clamp(position.y, MinimumValues.y, MaximumValues.y),
            Mathf.Clamp(position.z, MinimumValues.z, MaximumValues.z));

        return boundPosition;
    }
}
