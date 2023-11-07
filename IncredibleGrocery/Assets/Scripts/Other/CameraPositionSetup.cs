using UnityEngine;

public class CameraPositionSetup : MonoBehaviour
{
    [SerializeField] private Transform cameraNewPosition;
    [SerializeField] private float targetAspect;
    [SerializeField] private float aspectMultiplier;
    [SerializeField] private float newOrthographicSize;
    
    private Camera _mainCamera;
    private Vector3 _cameraPosition;

    private void Start()
    {
        _mainCamera = Camera.main;
        if (_mainCamera == null) return;
        
        _cameraPosition = _mainCamera.transform.position;
        
        var aspect = _mainCamera.aspect;

        if (aspect < targetAspect)
        {
            var newAspect = Mathf.Clamp(aspect * aspectMultiplier, 0, targetAspect);
            _cameraPosition.x = cameraNewPosition.position.x - newAspect;
            _mainCamera.transform.position = _cameraPosition;
        }
        else _mainCamera.orthographicSize = newOrthographicSize;
    }
}
