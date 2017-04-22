namespace Eggore
{
	using UnityEngine;

	public class Sensor : MonoBehaviour {

		public AIController ai;

		// Use this for initialization
		void Start () {
			
		}

		void OnTriggerEnter (Collider other) 
		{
			// Check if the collider belongs to the player
			if (other.gameObject.tag == "Player") 
			{
				// Move towards the player
				ai.onPlayerDetected (other.gameObject);
			}
		}

		void OnTriggerStay (Collider other)
		{
			if (other.gameObject.tag == "Player")
			{
				ai.onPlayerDetected (other.gameObject);
			}
		}

		void OnTriggerExit (Collider other)
		{
			if (other.gameObject.tag == "Player")
			{
				// Stop moving
				ai.onPlayerEscape ();
			}
		}

		// Update is called once per frame
		void Update () {

		}
	}
}