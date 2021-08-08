using System.IO;
using DemeoPlus.Utilities;
using MelonLoader;
using UnityEngine;

namespace DemeoPlus
{

    public class ClockConfig
    {
        public static ClockConfig Instance => MelonPreferences.GetCategory<ClockConfig>(ConfigManager.ClockConfig);

        public bool Enabled = true;
        public Vector3 Position = new Vector3(-100, 65, 0);
        public Vector3 Rotation = new Vector3(-20, -90, 0);
        public string Format = "hh:mm tt";
        public int FontSize = 45;
    }

    public class DiscordConfig
    {
        public static DiscordConfig Instance =>
            MelonPreferences.GetCategory<DiscordConfig>(ConfigManager.DiscordConfig);
        
        public bool Enabled = true;
        public bool ShowParty = true;
        public bool DevOnly_EnableJoins = false;
    }

    public class StaticCameraConfig
    {
        public static StaticCameraConfig Instance =>
            MelonPreferences.GetCategory<StaticCameraConfig>(ConfigManager.StaticCameraConfig);

        public bool Enabled = true;
        public Vector3 Position = new Vector3(0, 40, 0);
        public Vector3 Rotation = new Vector3(90, 0, 0);
    }

    public static class ConfigManager
    {
        public const string ClockConfig = "Clock";
        public const string DiscordConfig = "Discord";
        public const string StaticCameraConfig = "StaticCamera";

        public static void Register()
        {
            var categories = new[]
            {
                MelonPreferences.CreateCategory<ClockConfig>(ClockConfig, "Demeo+ Clock"),
                MelonPreferences.CreateCategory<DiscordConfig>(DiscordConfig, "Demeo+ Discord Integration"),
                MelonPreferences.CreateCategory<StaticCameraConfig>(StaticCameraConfig, "Demeo+ Static Camera")
            };
            
            if(!Directory.Exists(ConfigUtil.GetConfigDirectory())) Directory.CreateDirectory(ConfigUtil.GetConfigDirectory());
            
            foreach (var category in categories)
            {
                category.SetFilePath(ConfigUtil.GetConfigFilePath($"{category.Identifier}.toml"));
            }
        }
    }
}