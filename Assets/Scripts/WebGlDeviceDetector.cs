using UnityEngine;
using System.Runtime.InteropServices;

public class WebGLDeviceDetector : MonoBehaviour
{
    public enum DeviceType { Unknown, Touch, Keyboard }
    public static DeviceType CurrentDeviceType { get; private set; } = DeviceType.Unknown;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void DetectDeviceType(); // Call to JS plugin
#endif

    public void SetDeviceType(string type)
    {
        if (type == "Touch")
            CurrentDeviceType = DeviceType.Touch;
        else
            CurrentDeviceType = DeviceType.Keyboard;

        Debug.Log("Device Type: " + CurrentDeviceType);
    }

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        DetectDeviceType(); // Only works if plugin is defined
#else
        // Fallback detection
        CurrentDeviceType = Input.touchSupported ? DeviceType.Touch : DeviceType.Keyboard;
        Debug.Log("Fallback Device Type: " + CurrentDeviceType);
#endif
    }
}
