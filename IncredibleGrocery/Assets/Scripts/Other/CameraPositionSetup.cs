using UnityEngine;

public class CameraPositionSetup : MonoBehaviour
{
    [SerializeField] private Transform cameraNewPosition;
    [SerializeField] private float targetAspect;
    [SerializeField] private float aspectMultiplier;
    [SerializeField] private float newOrthographicSize;

    private Vector3 _cameraPosition;

    private void Start()
    {
        var mainCamera = Camera.main;
        if (mainCamera == null) return;
        
        _cameraPosition = mainCamera.transform.position;
        
        var aspect = mainCamera.aspect;
        if (aspect < targetAspect)
        {
            var newAspect = Mathf.Clamp(aspect * aspectMultiplier, 0, targetAspect);
            _cameraPosition.x = cameraNewPosition.position.x - newAspect;
            mainCamera.transform.position = _cameraPosition;
        }
        else mainCamera.orthographicSize = newOrthographicSize;
    }
}
