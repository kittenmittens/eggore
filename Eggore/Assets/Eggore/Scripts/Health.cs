namespace Eggore
{
	using UnityEngine;

	public class Health : MonoBehaviour, IHittable {

		protected const int DEFAULT_STARTING_HEALTH = 5;
		
		protected float healthPool;

		protected void Awake () {
			healthPool = DEFAULT_STARTING_HEALTH;
		}

		public bool IsAlive()
		{
			return (healthPool > 0);
		}

		
		public void OnHit(HitType hit, int damage)
		{
			LoseHealth(damage);
		}

		protected void LoseHealth(int healthLost)
		{
			healthPool -= healthLost;
		}

		void Update () {
			
		}
	}
}

