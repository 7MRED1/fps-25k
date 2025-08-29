
using UnityEngine;

namespace Player
{
    public class MouseLook : MonoBehaviour
    {
        public float mouseSensitivity = 2f;
        public float maxLookAngle = 80f;
        
        private float verticalRotation = 0;
        private Transform playerBody;
        
        void Start()
        {
            playerBody = transform.parent;
        }
        
        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            
            // Rotate player body horizontally
            if (playerBody != null)
            {
                playerBody.Rotate(Vector3.up * mouseX);
            }
            
            // Rotate camera vertically
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
            transform.localEulerAngles = new Vector3(verticalRotation, 0, 0);
        }
    }
}
