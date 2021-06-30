using MelonLoader;

namespace DemeoPlus.Desktop.Utilities
{
    public static class DesktopConfigManager
    {
        public const string DiscordConfig = "DemeoPlusDiscord";

        public static void Register()
        {
            MelonPreferences.CreateCategory<DiscordConfig>(DiscordConfig, "Demeo+ Discord Integration");
        }
    }
    
    public class DiscordConfig
    {
        public static DiscordConfig Instance => MelonPreferences.GetCategory<DiscordConfig>(DesktopConfigManager.DiscordConfig);

        public bool EnableDiscord = true;
        public bool ShowParty = true;
        public bool DevOnly_EnableJoins = false;
    }
}