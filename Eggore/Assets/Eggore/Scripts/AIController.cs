namespace Eggore
{
	using UnityEngine;

	/// <summary>
	/// Handles state logic for AI controlled entities
	/// </summary>
	[RequireComponent(typeof(CharacterMotor))]
	public class AIController : MonoBehaviour
	{
		public enum State {IDLE, ATTACK};
		private State activeState;

		protected CharacterMotor motor;
		protected GameObject player;

		protected void Awake()
		{
			motor = GetComponent<CharacterMotor>();
			foreach (Sensor sensor in GetComponentsInChildren<Sensor>())
			{
				sensor.ai = this;
			}
		}

		protected void Update()
		{
			
		}

		public void onPlayerDetected(GameObject player)
		{
			motor.Destination = player.transform.position;
		}

		public void onPlayerEscape()
		{
			motor.Stop ();
		}
	}
}
