
using UnityEngine;

namespace AI
{
    public static class EnemyFactory
    {
        public static void SpawnInitialEnemies(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnEnemy();
            }
        }
        
        public static GameObject SpawnEnemy()
        {
            Vector3 spawnPos = FindRandomSpawnPosition();
            
            GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            enemy.transform.position = spawnPos;
            enemy.transform.localScale = new Vector3(0.8f, 1f, 0.8f);
            enemy.name = "Enemy";
            
            // Add AI components
            var cc = enemy.AddComponent<CharacterController>();
            cc.radius = 0.4f;
            cc.height = 2f;
            cc.center = new Vector3(0, 1, 0);
            
            enemy.AddComponent<ChaserAI>();
            enemy.AddComponent<Health>();
            
            // Visual distinction
            var renderer = enemy.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red;
            }
            
            return enemy;
        }
        
        private static Vector3 FindRandomSpawnPosition()
        {
            // Try to find a position away from player
            Vector3 playerPos = Vector3.zero;
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerPos = player.transform.position;
            
            for (int attempts = 0; attempts < 10; attempts++)
            {
                Vector3 randomPos = new Vector3(
                    Random.Range(-10f, 10f),
                    1f,
                    Random.Range(-10f, 10f)
                );
                
                if (Vector3.Distance(randomPos, playerPos) > 5f)
                {
                    return randomPos;
                }
            }
            
            return new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f));
        }
    }
}
