namespace Eggore
{
	using UnityEngine;

	/// <summary>
	/// Handles state logic for AI controlled entities
	/// </summary>
	[RequireComponent(typeof(CharacterMotor))]
	public class AIController : MonoBehaviour
	{
		private const float ATTACK_THRESHOLD = 2.0F;

		protected CharacterMotor motor;

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

		protected void attack()
		{
			Debug.Log ("Attack player");
			// Attack Logic
		}

		protected bool canAttack(GameObject player)
		{
			Vector3 offset = player.transform.position - transform.position;
			float sqrLen = offset.sqrMagnitude;
			return (sqrLen <= (ATTACK_THRESHOLD * ATTACK_THRESHOLD));
		}

		public void onPlayerDetected(GameObject player)
		{
			// Check if player is close enough to attack, else move towards player
			if (canAttack(player)) {
				attack ();
			} else {
				motor.Destination = player.transform.position;
			}
		}

		public void onPlayerEscape()
		{
			motor.Stop ();
		}
	}
}
