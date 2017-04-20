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
            if (Input.GetMouseButtonDown(MOUSE_BUTTON))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    motor.Destination = hit.point;
                }
            }
        }

    }

}
