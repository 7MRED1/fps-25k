
using UnityEngine;

namespace UI
{
    public class SimpleHUD : MonoBehaviour
    {
        private Health playerHealth;
        private Weapons.HitscanGun playerWeapon;
        
        void Start()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerHealth = player.GetComponent<Health>();
                playerWeapon = GetComponent<Weapons.HitscanGun>();
            }
        }
        
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 200, 100));
            
            if (playerHealth != null)
            {
                GUILayout.Label($"Health: {playerHealth.currentHealth:F0}/{playerHealth.maxHealth:F0}");
            }
            
            if (playerWeapon != null)
            {
                GUILayout.Label($"Ammo: {playerWeapon.GetCurrentAmmo()}/{playerWeapon.GetMaxAmmo()}");
            }
            
            GUILayout.Label("Controls:");
            GUILayout.Label("WASD - Move");
            GUILayout.Label("Mouse - Look");
            GUILayout.Label("LMB - Shoot");
            GUILayout.Label("R - Reload");
            
            GUILayout.EndArea();
            
            // Crosshair
            float centerX = Screen.width / 2f;
            float centerY = Screen.height / 2f;
            float size = 10f;
            
            GUI.DrawTexture(new Rect(centerX - 1, centerY - size, 2, size * 2), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(centerX - size, centerY - 1, size * 2, 2), Texture2D.whiteTexture);
        }
    }
}
