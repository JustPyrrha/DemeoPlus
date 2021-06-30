using MelonLoader;
using UnityEngine;

namespace DemeoPlus.Core.Utilities
{
    public class ClockConfig
    {
        public static ClockConfig Instance => MelonPreferences.GetCategory<ClockConfig>(CoreConfigManager.ClockConfig);
        
        public Vector3 Location = new Vector3(-100, 65, 0);
        public Vector3 Rotation = new Vector3(-20, -90, 0);
        public string Format = "hh:mm tt";
        public int FontSize = 45;
    }

    public static class CoreConfigManager
    {
        public const string ClockConfig = "DemeoPlusClock";
        
        public static void Register()
        {
            MelonPreferences.CreateCategory<ClockConfig>(ClockConfig, "Demeo+ Clock");
        }
    }
}