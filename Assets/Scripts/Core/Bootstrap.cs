
using UnityEngine;

public static class Bootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void EnsureGame()
    {
        if (Object.FindObjectOfType<Game>() == null)
        {
            var go = new GameObject("Game");
            go.AddComponent<Game>();
        }
    }
}
