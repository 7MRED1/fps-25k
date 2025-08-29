
using UnityEngine;

namespace Player
{
    public class FPSController : MonoBehaviour
    {
        public float walkSpeed = 5f;
        public float runSpeed = 8f;
        public float jumpForce = 300f;
        public float mouseSensitivity = 2f;
        
        private CharacterController characterController;
        private Camera playerCamera;
        private float verticalVelocity = 0f;
        private float gravity = -9.81f;
        
        void Start()
        {
            characterController = GetComponent<CharacterController>();
            playerCamera = GetComponentInChildren<Camera>();
            
            if (playerCamera == null)
            {
                GameObject camGO = new GameObject("PlayerCamera");
                camGO.transform.SetParent(transform);
                camGO.transform.localPosition = new Vector3(0, 1.8f, 0);
                playerCamera = camGO.AddComponent<Camera>();
                camGO.AddComponent<MouseLook>();
            }
        }
        
        void Update()
        {
            HandleMovement();
            HandleJump();
        }
        
        private void HandleMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            
            float currentSpeed = isRunning ? runSpeed : walkSpeed;
            
            Vector3 direction = transform.right * horizontal + transform.forward * vertical;
            Vector3 movement = direction * currentSpeed * Time.deltaTime;
            
            if (characterController.isGrounded)
            {
                verticalVelocity = -0.5f;
            }
            else
            {
                verticalVelocity += gravity * Time.deltaTime;
            }
            
            movement.y = verticalVelocity * Time.deltaTime;
            characterController.Move(movement);
        }
        
        private void HandleJump()
        {
            if (Input.GetButtonDown("Jump") && characterController.isGrounded)
            {
                verticalVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }
    }
}
