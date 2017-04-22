namespace Eggore
{
	using UnityEngine;

	public class Sensor : MonoBehaviour {

		public AIController ai;

		// Use this for initialization
		void Start () {
			//		ai = transform.parent.gameObject.GetComponent<AIController> ();
		}

		void OnTriggerEnter (Collider other) 
		{
			// Check if the collider belongs to the player
			if (other.gameObject.tag == "Player") {
				// Move towards the player
				ai.onPlayerDetected(other.gameObject);
			}
		}

		void OnTriggerExit (Collider other)
		{
			// Check if the collider belongs to the character
			if (other.gameObject.tag == "Player") {
				ai.onPlayerEscape ();
			}
		}

		// Update is called once per frame
		void Update () {

		}
	}
}