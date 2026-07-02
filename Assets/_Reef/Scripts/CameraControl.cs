using UnityEngine;
using Unity.Cinemachine;

public class CameraControl : MonoBehaviour
{
    [Header("Cameras")]
    public CinemachineCamera cameraA;
    public CinemachineCamera cameraB;

    private void Start()
    {
        cameraA.Priority = 10;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cameraA.Priority > cameraB.Priority)
                ShowCameraB();
            else
                ShowCameraA();
        }
    }

    public void ShowCameraA()
    {
        cameraA.Priority = 10;
        cameraB.Priority = 0;
    }
    public void ShowCameraB()
    {
        cameraA.Priority = 0;
        cameraB.Priority = 10;
    }
}