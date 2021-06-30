using DemeoPlus.Core.Utilities;
using MelonLoader;
using UnityEngine;

namespace DemeoPlus.Core
{
    public class DemeoPlusCore : MelonMod
    {
        public override void OnApplicationStart()
        {
            CoreConfigManager.Register();
        }

        public override void OnApplicationQuit()
        {
            
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName != SceneNames.Lobby) return;

            new GameObject("DemeoPlus_Clock", typeof(Clock.Clock));
        }
    }
}