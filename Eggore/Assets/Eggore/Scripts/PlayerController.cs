namespace Eggore
{
    using UnityEngine;

    /// <summary>
    /// Handles user input for a player-controlled character.
    /// </summary>
    //[RequireComponent(typeof(CharacterMotor))]
    public class PlayerController : MonoBehaviour
    {

        // TODO: Get this by Raycast / collider touch point?
        protected const float GROUND_Y = 0F;
        protected const string MOUSE_AXIS_X = "Mouse X";

        public Mode mode;
        public CharacterMotor motor;
        public Rigidbody ragdoll;
        
        public float tilt = 22.5F;
        public float tiltSpeed = 3F;
        public float turnSpeed = 1F;

        protected Animator animator;
        protected CameraController cameraController;
        protected CharacterController characterController;
        protected Vector3 moveDirection;
        protected new Renderer renderer;
        protected Vector3 tiltDirection;
        protected Quaternion tiltRotation;

        public enum Mode
        {
            Motor,
            Ragdoll,
        }

        public void SwitchRagdoll()
        {
            switch (mode)
            {
                // Switch to Ragdoll
                case Mode.Motor:
                    // Pass transform to ragdoll
                    ragdoll.transform.position = renderer.transform.position;
                    ragdoll.transform.rotation = renderer.transform.rotation;
                    ragdoll.GetComponent<Rigidbody>().velocity = characterController.velocity;
                    // Add impulse?

                    animator.enabled = false;
                    animator.transform.localPosition = Vector3.zero;
                    animator.transform.localRotation = Quaternion.identity;
                    renderer.transform.localPosition = Vector3.zero;
                    renderer.transform.localRotation = Quaternion.identity;

                    cameraController.freeCamera = true;
                    motor.gameObject.SetActive(false);
                    ragdoll.gameObject.SetActive(true);
                    mode = Mode.Ragdoll;
                    return;

                // Switch to Motor
                case Mode.Ragdoll:
                    // Pass transform to motor
                    motor.transform.position = new Vector3(ragdoll.transform.position.x, GROUND_Y, ragdoll.transform.position.z);
                    motor.transform.rotation = Quaternion.Euler(0F, cameraController.transform.rotation.eulerAngles.y, 0F);
                    renderer.transform.position = ragdoll.transform.position;
                    renderer.transform.rotation = ragdoll.transform.rotation;

                    animator.transform.localPosition = Vector3.zero;
                    animator.transform.localRotation = Quaternion.identity;
                    animator.enabled = true;

                    cameraController.freeCamera = false;
                    motor.gameObject.SetActive(true);
                    ragdoll.gameObject.SetActive(false);
                    mode = Mode.Motor;
                    return;
            }
        }

        protected void Awake()
        {
            if (motor == null)
            {
                motor = GetComponentInChildren<CharacterMotor>();
            }
            if (ragdoll == null)
            {
                ragdoll = GetComponentInChildren<Rigidbody>();
            }

            switch (mode)
            {
                case Mode.Motor:
                    ragdoll.gameObject.SetActive(false);
                    break;
                case Mode.Ragdoll:
                    motor.gameObject.SetActive(false);
                    break;
            }

            animator = GetComponentInChildren<Animator>();
            cameraController = Camera.main.GetComponent<CameraController>();
            characterController = motor.GetComponent<CharacterController>();
            renderer = animator.GetComponentInChildren<Renderer>();
        }

        protected void Update()
        {
            switch (mode)
            {
                case Mode.Motor:
                    UpdateMotor();
                    return;
                case Mode.Ragdoll:
                    UpdateRagdoll();
                    return;
            }
        }

        protected void UpdateMotor()
        {
            moveDirection = Vector3.zero;
            tiltDirection = Vector3.zero;
            transform.Rotate(Vector3.up, Input.GetAxis(MOUSE_AXIS_X) * turnSpeed);

            if (Input.GetKey(KeyCode.W) == true)
            {
                moveDirection += transform.forward;
                tiltDirection += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S) == true)
            {
                moveDirection -= transform.forward;
                tiltDirection -= Vector3.forward;
            }

            if (Input.GetKey(KeyCode.D) == true)
            {
                moveDirection += transform.right;
                tiltDirection += Vector3.right;
            }
            if (Input.GetKey(KeyCode.A) == true)
            {
                moveDirection -= transform.right;
                tiltDirection -= Vector3.right;
            }

            if (moveDirection.sqrMagnitude > 0F)
            {
                motor.Steer(moveDirection.normalized);
                tiltRotation = Quaternion.AngleAxis(tilt, Vector3.Cross(Vector3.up, tiltDirection));
            }
            else
            {
                motor.Stop();
                tiltRotation = Quaternion.identity;
            }

            animator.transform.localRotation = Quaternion.Lerp(animator.transform.localRotation, tiltRotation, tiltSpeed * Time.deltaTime);

            transform.position = motor.transform.position;
            transform.rotation = motor.transform.rotation;
            motor.transform.localPosition = Vector3.zero;
            motor.transform.localRotation = Quaternion.identity;
        }

        protected void UpdateRagdoll()
        {
            transform.position = ragdoll.transform.position;
            transform.rotation = ragdoll.transform.rotation;
            ragdoll.transform.localPosition = Vector3.zero;
            ragdoll.transform.localRotation = Quaternion.identity;
        }

    }

}
