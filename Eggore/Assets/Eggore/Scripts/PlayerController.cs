namespace Eggore
{
    using UnityEngine;

    /// <summary>
    /// Handles user input for a player-controlled character.
    /// </summary>
    [RequireComponent(typeof(CharacterMotor))]
    public class PlayerController : MonoBehaviour
    {

        protected const string MOUSE_AXIS_X = "Mouse X";

        public float tilt = 22.5F;
        public float tiltSpeed = 3F;
        public float turnSpeed = 1F;

        protected Animator animator;
        protected Transform focus;
        protected CharacterMotor motor;
        protected Vector3 moveDirection;
        protected Vector3 tiltDirection;
        protected Quaternion tiltRotation;

        public Transform Focus
        {
            get
            {
                if (focus == null)
                {
                    focus = transform.FindChild("Focus");
                }
                return focus;
            }
        }

        protected void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            motor = GetComponent<CharacterMotor>();
        }

        protected void Update()
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
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Pick Up"))
            {
                other.gameObject.SetActive(false);
            }
        }

    }

}
