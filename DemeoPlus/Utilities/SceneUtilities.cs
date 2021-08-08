using UnityEngine;

namespace DemeoPlus.Utilities
{
    public static class SceneNames
    {
        public const string Startup = "StartupSteamVR";
        public const string Lobby = "LobbySteamVR";
    }

    public static class SceneUtils
    {
        public static GameObject GetMainCamera() =>
            Camera.main?.gameObject ?? GameObject.FindGameObjectsWithTag("MainCamera")[0];
    }
}