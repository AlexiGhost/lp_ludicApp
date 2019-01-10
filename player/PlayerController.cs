using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jumpAndLearn.player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class PlayerController : MonoBehaviour
    {


        #region Movements Settings
        [Header("Movement Settings")]
        [Range(1, 10)]
        public float forwardSpeed = 8.0f;
        [Range(1, 10)]
        public float backwardSpeed = 4.0f;
        [Range(1, 10)]
        public float strafeSpeed = 4.0f; //Left and right movements
        [Range(0, 5)]
        public float runMultiplayer = 2.0f; //Speed multiplayer when running
        public KeyCode runKey = KeyCode.LeftShift;
        [Range(0, 50)]
        public float jumpForce = 5f;
        [Range(1, 100)]
        public float rotationSpeed = 1f;
        [HideInInspector]
        public float currentSpeed = 8f;
        #endregion

        private Camera cam;
        private Rigidbody rb;
        private new CapsuleCollider collider; //'new' hide the base member
        private MouseLook rotationController = new MouseLook();
        private bool isJumping; //Also apply if player is falling
        private bool jump;
        //private float yRotation;


        void Start()
        {
            cam = Camera.main;
            rb = GetComponent<Rigidbody>();
            collider = GetComponent<CapsuleCollider>();
            rotationController.Init(transform, cam.transform);
        }

        void Update()
        {
            SetRotation();
            //Special inputs checking
            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                jump = true;
            }
        }

        private void FixedUpdate()
        {
            GroundCheck();
            Vector2 input = GetInput();
            //Movement
            if ((Mathf.Abs(input.x) > float.Epsilon) || (Mathf.Abs(input.y) > float.Epsilon))
            {
                Vector3 moveToPerform = cam.transform.forward * input.y + cam.transform.right * input.x;
                moveToPerform.x *= currentSpeed;
                moveToPerform.y *= currentSpeed;
                moveToPerform.z *= currentSpeed;
                if (rb.velocity.sqrMagnitude < (currentSpeed * currentSpeed))
                {
                    rb.AddForce(moveToPerform, ForceMode.Impulse);
                }
            }
            //Jump
            if (jump)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
                isJumping = true;
                jump = false;
            }
            //Drag
            if (isJumping)
            {
                rb.drag = 0f;
            }
            else
            {
                rb.drag = 5f;
                if (Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && rb.velocity.magnitude < 1f)
                {
                    rb.Sleep();
                }
            }

        }

        //Set the player speed
        public void SetMovementSpeed(Vector2 input)
        {
            //No movement
            if (input == Vector2.zero)
            {
                currentSpeed = 0f;
                return;
            }
            //Strafe
            if (input.x < 0 || input.x > 0)
            {
                currentSpeed = strafeSpeed;
            }
            //Backwards (same speed if strafe at the same time)
            if (input.y < 0)
            {
                currentSpeed = backwardSpeed;
            }
            //Forward (same speed if strafe at the same time)
            if (input.y > 0)
            {
                currentSpeed = forwardSpeed;
            }
        }

        //Set the player rotation
        private void SetRotation()
        {
            //Avoid rotation when the game is paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;
            //Get the current rotation
            float oldYRotation = transform.eulerAngles.y;
            //Look at the new point
            rotationController.LookRotation(transform, cam.transform);
            //Rotate the player velocity to follow the rotation
            Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
            rb.velocity = velRotation * rb.velocity;
        }

        private Vector2 GetInput()
        {
            Vector2 input = new Vector2
            {
                x = Input.GetAxis("Horizontal"),
                y = Input.GetAxis("Vertical")
            };
            SetMovementSpeed(input);
            return input;
        }

        ///Work only for capsule
        //Check if the player currently touch the ground
        private void GroundCheck()
        {
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, collider.radius * (1.0f - 0.01f), Vector3.down, out hitInfo,
                                           ((collider.height / 2f) - collider.radius) + 0.01f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                isJumping = false;
            }
            else
            {
                isJumping = true;
            }
        }
    }
}