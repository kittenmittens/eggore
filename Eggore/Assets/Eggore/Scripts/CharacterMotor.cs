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
        
        protected Vector3 destination;
        protected Vector3 direction;
        protected bool moving;

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
                moving = true;
            }
        }

        public bool Moving
        {
            get
            {
                return moving;
            }
        }

        public void Stop()
        {
            destination = Vector3.zero;
            moving = false;
        }

        protected void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        protected void Update()
        {
            if (moving)
            {
                direction = Vector3.ProjectOnPlane(destination - transform.position, Vector3.up);
                if (direction.sqrMagnitude < speed * speed * Time.deltaTime * Time.deltaTime)
                {
                    controller.Move(direction);
                    Stop();
                    return;
                }
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), turnSpeed * Time.deltaTime);
                controller.Move(transform.forward * speed * Time.deltaTime + Physics.gravity);
            }
        }

    }

}
