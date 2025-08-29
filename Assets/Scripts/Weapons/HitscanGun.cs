
using UnityEngine;

namespace Weapons
{
    public class HitscanGun : MonoBehaviour
    {
        public float damage = 20f;
        public float range = 100f;
        public float fireRate = 0.1f;
        public int maxAmmo = 30;
        
        private int currentAmmo;
        private float nextFireTime = 0f;
        private Camera playerCamera;
        
        void Start()
        {
            currentAmmo = maxAmmo;
            playerCamera = GetComponent<Camera>();
        }
        
        void Update()
        {
            if (Input.GetButton("Fire1") && Time.time >= nextFireTime && currentAmmo > 0)
            {
                Fire();
                nextFireTime = Time.time + fireRate;
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }
        
        private void Fire()
        {
            currentAmmo--;
            
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
            {
                Health targetHealth = hit.collider.GetComponent<Health>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(damage);
                }
                
                // Visual feedback
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * hit.distance, Color.red, 0.1f);
            }
            else
            {
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * range, Color.white, 0.1f);
            }
        }
        
        private void Reload()
        {
            currentAmmo = maxAmmo;
        }
        
        public int GetCurrentAmmo() { return currentAmmo; }
        public int GetMaxAmmo() { return maxAmmo; }
    }
}
