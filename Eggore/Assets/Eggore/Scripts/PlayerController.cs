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
        
        public float turnSpeed = 1F;

        protected Transform focus;
        protected CharacterMotor motor;
        protected Vector3 direction;

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
            motor = GetComponent<CharacterMotor>();
        }

        protected void Update()
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(KeyCode.W) == true)
            {
                direction += transform.forward;
            }
            if (Input.GetKey(KeyCode.S) == true)
            {
                direction -= transform.forward;
            }

            if (Input.GetKey(KeyCode.D) == true)
            {
                direction += transform.right;
            }
            if (Input.GetKey(KeyCode.A) == true)
            {
                direction -= transform.right;
            }

            transform.Rotate(Vector3.up, Input.GetAxis(MOUSE_AXIS_X) * turnSpeed);
            if (direction.sqrMagnitude > 0F)
            {
                motor.Steer(direction.normalized);
                return;
            }
            motor.Stop();
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
