namespace Eggore
{
    using UnityEngine;

    /// <summary>
    /// Handles user input for a player-controlled character.
    /// </summary>
    [RequireComponent(typeof(CharacterMotor))]
    public class PlayerController : MonoBehaviour
    {

        protected const int MOUSE_BUTTON = 1;

        protected Transform focus;
        protected CharacterMotor motor;

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

        protected void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                motor.Destination = hit.point;
            }

            if (Input.GetKey(KeyCode.W) == true)
            {
                Debug.Log("Pressing W");
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.A) == true)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.S) == true)
            {
                transform.Translate(Vector3.back * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.D) == true)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
        }

    }

}