
using UnityEngine;

// Main entry point: loads configs, builds level, ensures player and enemies exist.
public class Game : MonoBehaviour
{
    void Awake()
    {
        ConfigIO.EnsureDefaultConfigs();
        Level.LevelBuilder.BuildFromJson(ConfigIO.ReadLevelJsonPath());
        Player.PlayerFactory.EnsurePlayerAtSpawn();
        AI.EnemyFactory.SpawnInitialEnemies(5);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
