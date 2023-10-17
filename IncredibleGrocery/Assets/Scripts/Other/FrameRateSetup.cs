using UnityEngine;

public class FrameRateSetup : MonoBehaviour
{
#if PLATFORM_ANDROID
    void Awake()
    {
        Application.targetFrameRate = 90;
    }
#endif
}