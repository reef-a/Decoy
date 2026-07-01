using UnityEngine;

namespace _Abdullah
{
    /// <summary>
    /// Detects when player looks at interactable objects (PC monitor, phone)
    /// Uses raycasting from camera to detect gaze
    /// </summary>
    public class ObjectInteraction : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float interactionDistance = 3f;
        [SerializeField] private LayerMask interactableLayer;

        [Header("References")]
        [SerializeField] private EmailSimulation emailSimulation;
        [SerializeField] private PhoneSimulation phoneSimulation;

        private Camera playerCamera;
        private EmailSimulation currentEmailTarget;
        private PhoneSimulation currentPhoneTarget;

        private void Awake()
        {
            playerCamera = Camera.main;
            if (playerCamera == null)
            {
                playerCamera = GetComponentInChildren<Camera>();
            }
        }

        private void Update()
        {
            CheckGaze();
        }

        private void CheckGaze()
        {
            if (playerCamera == null) return;

            RaycastHit hit;
            Vector3 rayOrigin = playerCamera.transform.position;
            Vector3 rayDirection = playerCamera.transform.forward;

            if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance, interactableLayer))
            {
                GameObject hitObject = hit.collider.gameObject;

                // Check for email simulation
                EmailSimulation emailSim = hitObject.GetComponent<EmailSimulation>();
                if (emailSim != null && currentEmailTarget != emailSim)
                {
                    if (currentEmailTarget != null)
                        currentEmailTarget.OnPlayerLookAway();
                    
                    currentEmailTarget = emailSim;
                    currentEmailTarget.OnPlayerLook();
                }

                // Check for phone simulation
                PhoneSimulation phoneSim = hitObject.GetComponent<PhoneSimulation>();
                if (phoneSim != null && currentPhoneTarget != phoneSim)
                {
                    if (currentPhoneTarget != null)
                        currentPhoneTarget.OnPlayerLookAway();
                    
                    currentPhoneTarget = phoneSim;
                    currentPhoneTarget.OnPlayerLook();
                }
            }
            else
            {
                // Player is not looking at any interactable object
                if (currentEmailTarget != null)
                {
                    currentEmailTarget.OnPlayerLookAway();
                    currentEmailTarget = null;
                }

                if (currentPhoneTarget != null)
                {
                    currentPhoneTarget.OnPlayerLookAway();
                    currentPhoneTarget = null;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (playerCamera != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionDistance);
            }
        }
    }
}
