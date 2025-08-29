
using UnityEngine;

namespace Player
{
    public static class PlayerFactory
    {
        public static void EnsurePlayerAtSpawn()
        {
            if (Object.FindObjectOfType<FPSController>() != null) return;
            
            GameObject player = new GameObject("Player");
            player.transform.position = new Vector3(2f, 1f, 2f);
            
            // Add components
            var cc = player.AddComponent<CharacterController>();
            cc.radius = 0.5f;
            cc.height = 2f;
            cc.center = new Vector3(0, 1, 0);
            
            player.AddComponent<FPSController>();
            player.AddComponent<Health>();
            
            // Add camera as child
            GameObject camera = new GameObject("PlayerCamera");
            camera.transform.SetParent(player.transform);
            camera.transform.localPosition = new Vector3(0, 1.8f, 0);
            camera.AddComponent<Camera>();
            camera.AddComponent<MouseLook>();
            camera.AddComponent<Weapons.HitscanGun>();
            
            // Add HUD
            camera.AddComponent<UI.SimpleHUD>();
            
            player.tag = "Player";
        }
    }
}
