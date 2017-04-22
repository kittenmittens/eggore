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

        protected Transform focus;
        protected CharacterMotor motor;
        protected Vector3 direction;
        public float xSpeed = 1F;

        public float speed;

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

        protected void UpdateRotation()
        {
            //transform.RotateAround(Input.GetAxis(MOUSE_AXIS_X), xSpeed);
        }

        protected void Update()
        {
            UpdateRotation();
            if (Input.GetKeyDown(KeyCode.W) == true)
            {
                motor.Steer(transform.forward);
            }
            if (Input.GetKeyDown(KeyCode.A) == true)
            {
                direction = Vector3.left;
            }
            if (Input.GetKeyDown(KeyCode.S) == true)
            {
                direction = Vector3.back;
            }
            if (Input.GetKeyDown(KeyCode.D) == true)
            {
                direction = Vector3.right;
            }
        }

        public Vector3 getDirection()
        {
            return direction;
        }

    }

}