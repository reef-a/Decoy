using UnityEngine;

namespace _Abdullah
{
    /// <summary>
    /// Controls camera rotation for the player. Player can only rotate the camera, not move.
    /// Supports mouse (PC), touch (mobile), and VR controller rotation.
    /// </summary>
    public class CameraRotationController : MonoBehaviour
    {
        [Header("Rotation Settings")]
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField] private float maxVerticalAngle = 80f;
        [SerializeField] private float minVerticalAngle = -80f;

        [Header("Mobile Settings")]
        [SerializeField] private float touchSensitivity = 1f;

        private float currentYaw = 0f;
        private float currentPitch = 0f;
        private Transform cameraTransform;

        private void Awake()
        {
            if (Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
            }
            
            if (cameraTransform == null)
            {
                Camera cam = GetComponentInChildren<Camera>();
                if (cam != null)
                {
                    cameraTransform = cam.transform;
                }
            }
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            HandleMouseInput();
#elif UNITY_ANDROID || UNITY_IOS
            HandleTouchInput();
#endif

            // VR input is handled automatically by Unity's XR system
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButton(0))
            {
                float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
                float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

                currentYaw += mouseX;
                currentPitch -= mouseY;
                currentPitch = Mathf.Clamp(currentPitch, minVerticalAngle, maxVerticalAngle);

                ApplyRotation();
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Moved)
                {
                    float deltaX = touch.deltaPosition.x * touchSensitivity * Time.deltaTime;
                    float deltaY = touch.deltaPosition.y * touchSensitivity * Time.deltaTime;

                    currentYaw += deltaX;
                    currentPitch -= deltaY;
                    currentPitch = Mathf.Clamp(currentPitch, minVerticalAngle, maxVerticalAngle);

                    ApplyRotation();
                }
            }
        }

        private void ApplyRotation()
        {
            if (cameraTransform != null)
            {
                cameraTransform.rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
            }
        }

        /// <summary>
        /// Resets camera rotation to default position
        /// </summary>
        public void ResetRotation()
        {
            currentYaw = 0f;
            currentPitch = 0f;
            ApplyRotation();
        }
    }
}
