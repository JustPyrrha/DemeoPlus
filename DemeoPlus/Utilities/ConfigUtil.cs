using System.IO;
using MelonLoader;

namespace DemeoPlus.Utilities
{
    public class ConfigUtil
    {
        public static string GetConfigDirectory() => Path.Combine(MelonUtils.UserDataDirectory, "DemeoPlus");
        public static string GetConfigFilePath(string filename) => Path.Combine(GetConfigDirectory(), filename);
    }
}