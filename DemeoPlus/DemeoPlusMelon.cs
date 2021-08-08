using DemeoPlus.Discord;
using DemeoPlus.Enhancements;
using DemeoPlus.StreamTools;
using DemeoPlus.StreamTools.Camera;
using DemeoPlus.ToggleSystem;
using DemeoPlus.Utilities;
using MelonLoader;
using UnityEngine;

namespace DemeoPlus
{
    public class DemeoPlusMelon : MelonMod
    {
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName == SceneNames.Startup)
            {
                ConfigManager.Register();

                new GameObject("DemeoPlus").AddComponent<ToggleManager>();
            }
        }
    }
}