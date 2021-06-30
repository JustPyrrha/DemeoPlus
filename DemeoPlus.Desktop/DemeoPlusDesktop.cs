using DemeoPlus.Desktop.Discord;
using DemeoPlus.Desktop.Utilities;
using MelonLoader;

namespace DemeoPlus.Desktop
{
    public class DemeoPlusDesktop : MelonMod
    {
        private DiscordManager _discord;
        
        public override void OnApplicationStart()
        {
            DesktopConfigManager.Register();
            _discord = new DiscordManager();
            _discord.Init();
        }

        public override void OnApplicationQuit()
        {
            _discord.Dispose();
        }
        
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            _discord.HandleSceneChange(sceneName);
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            _discord.HandleSceneUnloaded(sceneName);
        }

        public override void OnPreferencesLoaded()
        {
            _discord.HandlePreferencesLoaded();
        }
        
        public override void OnUpdate()
        {
            
            _discord.RunCallbacks();
        }
    }
}