using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainToScreenBehaviour : MonoBehaviour
{
    // Class to allow minimizing padding values in inspector
    [System.Serializable]
    private struct Padding
    {
        public float Top;
        public float Bottom;
        public float Left;
        public float Right;
    }

    [SerializeField] private Padding _padding;
    [SerializeField] private Vector2 _objectSize;
    // Optional camera
    [SerializeField] private Camera _cam;
    [Space]
    [SerializeField] private bool _debugDraw = true;

    private Vector3 _screenBounds;

    private void Awake()
    {
        if (!_cam)
            _cam = Camera.main;
    }

    private void Start()
    {
        // Retrieve screen bounds as world coordinates
        _screenBounds = _cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _cam.transform.position.z));
    }

    private void LateUpdate()
    {

        // Fix: This code causes the player to hitch on boundaries and move slower
        Vector3 position = transform.position;

        position.x = Mathf.Clamp(position.x, _screenBounds.x + _padding.Left + (_objectSize.x / 2), -_screenBounds.x - _padding.Right - (_objectSize.x / 2));
        position.y = Mathf.Clamp(position.y, _screenBounds.y + _padding.Bottom + (_objectSize.y / 2), -_screenBounds.y - _padding.Top - (_objectSize.y / 2));

        transform.position = position;
        // /Fix
    }

    private void OnDrawGizmos()
    {
        // Draw screen bounds
        if (!_debugDraw || !_cam) return;
        Gizmos.color = Color.red;
        Vector3 position = new Vector3(_cam.transform.position.x, _cam.transform.position.y, transform.position.z);
        Vector3 size = _screenBounds * 2;
        _screenBounds.z = 1;
        Gizmos.DrawWireCube(position, size);

        // Draw object
        size = _objectSize;
        size.z = 1;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
