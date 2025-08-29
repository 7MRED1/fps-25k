
using UnityEngine;

namespace AI
{
    public class ChaserAI : MonoBehaviour
    {
        public float speed = 3f;
        public float detectionRange = 10f;
        public float attackRange = 2f;
        public float attackDamage = 10f;
        public float attackCooldown = 1f;
        
        private Transform player;
        private CharacterController controller;
        private float lastAttackTime = 0f;
        
        void Start()
        {
            controller = GetComponent<CharacterController>();
            var playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
                player = playerGO.transform;
        }
        
        void Update()
        {
            if (player == null) return;
            
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            
            if (distanceToPlayer <= detectionRange)
            {
                if (distanceToPlayer > attackRange)
                {
                    ChasePlayer();
                }
                else if (Time.time >= lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                }
            }
        }
        
        private void ChasePlayer()
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // Keep on ground
            
            controller.Move(direction * speed * Time.deltaTime);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
        
        private void AttackPlayer()
        {
            lastAttackTime = Time.time;
            
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}
