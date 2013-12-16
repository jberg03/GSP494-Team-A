using UnityEngine;
using System.Collections;

namespace Junior
{
    //Require a character controller to be attached to the same game object
    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu("Third Person Player/Third Person Controller")]


    class ThirdPersonController : MonoBehaviour
    {
        //The speed when moving slow
        public float slowSpeed = 3.0f;
        //The speed when moving fast
        public float fastSpeed = 6.0f;

        public float inAirControlAcceleration = 3.0f;

        //max height for the thrusters
        public float thrusterHeight = 4.0f;

        //gravity for the character
        public float gravity = 20.0f;
        //gravity in controlled descent mode
        public float controlledDescentGravity = 2.0f;
        public float speedSmoothing = 10.0f;
        public float rotateSpeed = 500.0f;
        public float fastAfterSeconds = 3.0f;

        //booleans for checking if some actions can occur
        public bool canThrust = true;
        public bool canControlDescent = true;

        private float thrustRepeatTime = 0.05f;
        private float thrustTimeout = 0.15f;
        private float groundedTimeout = 0.25f;

        // The camera doesnt start following the target immediately but waits for a split second to avoid too much waving around.
        private float lockCameraTimer = 0.0f;

        //The current direction of movement
        private Vector3 moveDirection = Vector3.zero;
        //The current vertical speed
        private float verticalSpeed = 0.0f;
        //The current movement speed
        private float moveSpeed = 0.0f;

        // The last Collision flags returned from controller.Move
        private CollisionFlags collisionFlags;

        //check to see if we are thrusting or if we've reach max height
        private bool thrusting = false;
        private bool thrustingReachedApex = false;

        //bool to lock the camera from doing a 180 when moving backwards
        private bool movingBackwards = false;
        //is there any input from player
        private bool isMoving = false;
        //The time the user started moving slow
        private float slowTimeStart = 0.0f;
        // Last time the jump button was clicked down
        private float lastThrustButtonTime = -10.0f;
        // Last time we performed a jump
        private float lastThrustTime = -1.0f;

        //In Air Velocity
        private Vector3 inAirVelocity = Vector3.zero;

        private float lastGroundedTime = 0.0f;

        private float lean = 0.0f;
        private bool slammed = false;
        private bool isControllable = true;

        public void Awake()
        {
            moveDirection = transform.TransformDirection(Vector3.forward);
        }

        //Hides the player and makes him non-controllable
        public void HidePlayer()
        {
            GameObject.Find("rootJoint").GetComponent<SkinnedMeshRenderer>().enabled = false; // stop rendering
            isControllable = false; //disable player controls
        }

        //Just in case we need to be able to make the player non-usable and then usable again
        public void ShowPlayer()
        {
            GameObject.Find("rootJoint").GetComponent<SkinnedMeshRenderer>().enabled = true; // start rendering
            isControllable = true; // enable player controls
        }

        public void UpdateSmoothedMovementDirection()
        {
            Transform cameraTransform = Camera.main.transform;
            bool grounded = IsGrounded();

            ////Forward vector relative to the camera along x-z plane
            Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
            forward.y = 0.0f;
            forward = forward.normalized;

            //Right vector relative to the camera
            //Always orthogonal to the forward vector
            Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

            float v = Input.GetAxisRaw("Vertical");
            float h = Input.GetAxisRaw("Horizontal");

            if (v < -2.0f)
                movingBackwards = true;
            else
                movingBackwards = false;

            bool wasMoving = isMoving;
            isMoving = Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f;

            //Target Direction relative to the camera
            Vector3 targetDirection = h * right + v * forward;

            //Grounded controls
            if (grounded)
            {
                lockCameraTimer += Time.deltaTime;
                if (isMoving != wasMoving)
                    lockCameraTimer = 0.0f;

                // speed and direction are separate
                // that way when the character is still it is still facing forward
                //moveDirection is always normalized and we only update it if there is user input
                if (targetDirection != Vector3.zero)
                {
                    //If we are really slow just snap to the target direction
                    if (moveSpeed < slowSpeed * 0.9f && grounded)
                    {
                        moveDirection = targetDirection.normalized;
                    }
                    else
                    {
                        moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);

                        moveDirection = moveDirection.normalized;
                    }
                }

                //Smooth the speed based on the current target direction
                float curSmooth = speedSmoothing * Time.deltaTime;

                //Choose target speed
                float targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);

                //Pick speed modifier
                if (Input.GetButton("Fire3"))
                {
                    targetSpeed *= fastSpeed;
                }
                else if (Time.time - fastAfterSeconds > slowTimeStart)
                {
                    targetSpeed *= fastSpeed;
                }
                else
                {
                    targetSpeed *= slowSpeed;
                }

                moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);

                //Reset slow time start when we slow down
                if (moveSpeed < slowSpeed * 0.3f)
                    slowTimeStart = Time.time;
            }
            //In air controls
            else
            {
                //Lock the camera while in the air
                if (thrusting)
                    lockCameraTimer = 0.0f;

                if (isMoving)
                    inAirVelocity += targetDirection.normalized * Time.deltaTime * inAirControlAcceleration;
            }
        }

        public void ApplyThrusting()
        {
            // Prevent jumping too fast after each other
            if (lastThrustTime + thrustRepeatTime > Time.time)
                return;

            if (IsGrounded())
            {
                //Thrust
                //-only when pressing SPACE
                if (canThrust && Time.time < lastThrustButtonTime + thrustTimeout)
                {
                    verticalSpeed = CalculateThrustVerticalSpeed(thrusterHeight);
                    SendMessage("DidThrust", SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        public void ApplyGravity()
        {
            if (isControllable)
            {
                //Apply Gravity
                bool thrustButton = Input.GetButton("Jump");

                //When falling we control descent
                bool controlledDescent = canControlDescent && verticalSpeed <= 0.0f && thrusting && thrustButton;

                //When we reach the Apex of our jump we send out a message
                if (thrusting && !thrustingReachedApex && verticalSpeed <= 0.0f)
                {
                    thrustingReachedApex = true;
                    SendMessage("DidThrustReachApex", SendMessageOptions.DontRequireReceiver);
                }

                if (controlledDescent)
                    verticalSpeed -= controlledDescentGravity * Time.deltaTime;
                else if (IsGrounded())
                    verticalSpeed = 0.0f;
                else
                    verticalSpeed -= gravity * Time.deltaTime;
            }
        }

        public float CalculateThrustVerticalSpeed(float targetThrustHeight)
        {
            //from the jump height and gravity we deduce the upwards speed for the player to reach the apex
            return Mathf.Sqrt(2 * targetThrustHeight * gravity);
        }

        public void DidThrust()
        {
            thrusting = true;
            thrustingReachedApex = false;
            lastThrustTime = Time.time;
            lastThrustButtonTime = -10;
        }

        public void Update()
        {
            if (!isControllable)
            {
                Input.ResetInputAxes();
            }

            if (Input.GetButtonDown("Jump"))
            {
                lastThrustButtonTime = Time.time;
            }

            UpdateSmoothedMovementDirection();

            //Apply Gravity and Thrusters
            ApplyGravity();
            ApplyThrusting();

            //Calculate movement
            Vector3 movement = moveDirection * moveSpeed + new Vector3(0, verticalSpeed, 0) + inAirVelocity;
            movement *= Time.deltaTime;

            //Move the controller
            CharacterController controller = GetComponent<CharacterController>();
            collisionFlags = controller.Move(movement);

            //Set rotation to move direction
            if (IsGrounded())
            {
                if (slammed)
                {
                    slammed = false;
                    controller.height = 2;
                    float y = transform.position.y;
                    y += 0.75f;
                }

                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
            else
            {
                if (!slammed)
                {
                    Vector3 xzMove = movement;
                    xzMove.y = 0;
                    if (xzMove.sqrMagnitude > 0.001f)
                    {
                        transform.rotation = Quaternion.LookRotation(xzMove);
                    }
                }
            }

            if (IsGrounded())
            {
                lastGroundedTime = Time.time;
                inAirVelocity = Vector3.zero;
                if (thrusting)
                {
                    thrusting = false;
                    SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        public void OnControllerHit(ControllerColliderHit hit)
        {
            if (hit.moveDirection.y > 0.01)
                return;
        }

        public float GetSpeed()
        {
            return moveSpeed;
        }

        public bool IsThrusting()
        {
            return thrusting && !slammed;
        }

        public bool IsGrounded()
        {
            return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
        }

        public void Slam(Vector3 direction)
        {
            verticalSpeed = CalculateThrustVerticalSpeed(1);
            inAirVelocity = direction * 6;
            direction.y = 0.6f;
            Quaternion.LookRotation(-direction);
            CharacterController controller = GetComponent<CharacterController>();
            controller.height = 0.5f;
            slammed = true;
            collisionFlags = CollisionFlags.None;
            SendMessage("DidThrust", SendMessageOptions.DontRequireReceiver);
        }

        public Vector3 GetDirection()
        {
            return moveDirection;
        }

        public bool IsMovingBackwards()
        {
            return movingBackwards;
        }

        public float GetLockCameraTimer()
        {
            return lockCameraTimer;
        }

        public bool IsMoving()
        {
            return Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f;
        }

        public bool HasThrustReachedApex()
        {
            return thrustingReachedApex;
        }

        public bool IsControlledDescent()
        {
            bool thrustButton = Input.GetButton("Jump");
            return canControlDescent && verticalSpeed <= 0.0f && thrustButton && thrusting;
        }

        public void Reset()
        {
            gameObject.tag = "Player";
        }
    }
}