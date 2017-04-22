namespace Eggore
{
    using UnityEngine;

    public class EnemySpawn : MonoBehaviour
    {

        public GameObject[] enemyPrefabs;
        public float spawnTime;

        protected float elapsedTime;

        protected void Spawn()
        {
            int index = Random.Range(0, enemyPrefabs.Length);            
            GameObject enemy = Instantiate(enemyPrefabs[index]);
            enemy.transform.position = transform.position;
            enemy.transform.rotation = transform.rotation;
        }

        protected void Update()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > spawnTime)
            {
                Spawn();
                elapsedTime -= spawnTime;
            }
        }

    }

}
