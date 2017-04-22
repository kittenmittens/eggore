namespace Eggore
{
    using UnityEngine;

    /// <summary>
    /// Handles movement for a character.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMotor : MonoBehaviour
    {

        protected const float SQR_THRESHOLD = 10.0F;

        public float speed;
        public float turnSpeed;
        public enum State {Idle, Steering, Following}
        
        protected Vector3 destination;
        protected Vector3 direction;
        protected State state;

        private CharacterController controller;

        public Vector3 Destination
        {
            get
            {
                return destination;
            }
            set
            {
                direction = Vector3.ProjectOnPlane(value - transform.position, Vector3.up);
                if (direction.sqrMagnitude < SQR_THRESHOLD)
                {
                    Stop();
                    return;
                }
                destination = value;
                state = State.Following;
            }
        }

        public bool Moving
        {
            get
            {
                return (state != State.Idle);
            }
        }

        public void Stop()
        {
            destination = Vector3.zero;
            direction = Vector3.zero;
            state = State.Idle;
        }

        protected void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        public void Steer(Vector3 steerDirection)
        {
            direction = steerDirection;
            destination = Vector3.zero;
            state = State.Steering;
        }

        protected void Update()
        {
            switch (state)
            {
                case State.Steering:
                    controller.Move(direction * speed * Time.deltaTime + Physics.gravity * Time.deltaTime);
                    break;
                case State.Following:
                    direction = Vector3.ProjectOnPlane(destination - transform.position, Vector3.up);
                    if (direction.sqrMagnitude < speed * speed * Time.deltaTime * Time.deltaTime)
                    {
                        controller.Move(direction);
                        Stop();
                        return;
                    }
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), turnSpeed * Time.deltaTime);
                    controller.Move(transform.forward * speed * Time.deltaTime + Physics.gravity * Time.deltaTime);
                    break;
                default:
                    break;
            }

        }

    }

}
