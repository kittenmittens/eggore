namespace Eggore
{
    using UnityEngine;

    public class GameController : MonoBehaviour
    {
        
        public PlayerController playerPrefab;
        public Transform playerSpawn;
        
        protected PlayerController player;


        protected void Start()
        {
            SpawnPlayer();
        }

        protected void SpawnPlayer()
        {
            player = Instantiate(playerPrefab);
            player.transform.position = playerSpawn.position;
            player.transform.rotation = playerSpawn.rotation;
        }

    }

}
