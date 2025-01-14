using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Platformer {
    public class PlayerController : MonoBehaviour {
        [Header("References")]
        [SerializeField] Rigidbody rb;
        //[SerializeField] GroundChecker groundChecker;
        [SerializeField] Animator animator;
        [SerializeField] CinemachineFreeLook freeLookVCam;
        [SerializeField] InputReader input;
        
        [Header("Movement Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float smoothTime = 0.2f;
        [SerializeField] float force;
        
        


        const float ZeroF = 0f;
        
        Transform mainCam;
        
        float currentSpeed;
        float velocity;

        Vector3 movement;

        static readonly int Speed = Animator.StringToHash("Speed");

        void Awake() {
            mainCam = Camera.main.transform;
            freeLookVCam.Follow = transform;
            freeLookVCam.LookAt = transform;
            freeLookVCam.OnTargetObjectWarped(transform, transform.position - freeLookVCam.transform.position - Vector3.forward);
            
            rb.freezeRotation = true;
        }
        void Start() => input.EnablePlayerActions();

        void Update() {
            movement = new Vector3(input.Direction.x, 0f, input.Direction.y);

            UpdateAnimator();
        }
        private void FixedUpdate() {
            HandleMovement();
        }

        void UpdateAnimator() {
            animator.SetFloat(Speed, currentSpeed);
        }

        public void HandleMovement() {
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;
            
            if (adjustedDirection.magnitude > ZeroF) {
                HandleRotation(adjustedDirection);
                HandleHorizontalMovement(adjustedDirection);
                SmoothSpeed(adjustedDirection.magnitude);
            } else {
                SmoothSpeed(ZeroF);
                rb.velocity = new Vector3(ZeroF, rb.velocity.y, ZeroF);
            }
        }

        void HandleHorizontalMovement(Vector3 adjustedDirection) {
            // Move the player
            Vector3 velocity = adjustedDirection * (moveSpeed * Time.fixedDeltaTime);
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
            Debug.Log(velocity);
        }

        void HandleRotation(Vector3 adjustedDirection) {
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        void SmoothSpeed(float value) {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }
    }
}
