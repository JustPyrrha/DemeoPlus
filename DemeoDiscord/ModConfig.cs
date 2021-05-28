using MelonLoader;
using MelonLoader.Preferences;

namespace DemeoDiscord
{
    public class ModConfig
    {
        public bool EnableExperimentalFeatures = false;
    }

    public class ModConfigManager
    {
        public static ModConfig ModConfig
        {
            get => MelonPreferences.GetCategory<ModConfig>("DemeoDiscord");
            
        }

        public static void Register()
        {
            MelonPreferences.CreateCategory<ModConfig>("DemeoDiscord", "Demeo Discord");
        }
    }
}