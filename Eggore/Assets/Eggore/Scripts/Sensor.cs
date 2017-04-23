namespace Eggore
{
	using UnityEngine;

	/// <summary>
    /// Visual Sensor for AI Entities
    /// </summary>
	public class Sensor : MonoBehaviour {

		public AIController ai;

		// Use this for initialization
		void Start () {
			
		}

		void OnTriggerEnter (Collider other) 
		{
			checkPlayerDetected(other);
		}

		void OnTriggerStay (Collider other)
		{
			checkPlayerDetected(other);
		}

		void OnTriggerExit (Collider other)
		{
			checkPlayerEscaped(other);
		}

		// Update is called once per frame
		void Update () {

		}

		protected void checkPlayerDetected(Collider other)
		{
			if (other.gameObject.tag == "Player" && playerIsVisible(other)) 
			{
				ai.onPlayerDetected (other.gameObject);
			}
		}

		protected void checkPlayerEscaped(Collider other)
		{
			if (other.gameObject.tag == "Player")
			{
				// Stop moving
				ai.onPlayerEscape ();
			}
		}

		protected bool playerIsVisible(Collider player)
		{
			RaycastHit hitInfo;
			CharacterController character = (CharacterController) player;
			Vector3 lookAt = player.transform.position + character.center;
			Vector3 direction = (lookAt - transform.position); 
			if (Physics.Raycast(transform.position, direction, out hitInfo, Mathf.Infinity))
			{
				if (hitInfo.collider.gameObject.tag == "Player")
				{
					return true;
				}
			}
			return false;
		}
	}
}