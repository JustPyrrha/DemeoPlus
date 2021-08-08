using Boardgame;
using DemeoPlus.Utilities;
using HarmonyLib;

namespace DemeoPlus.Patches
{
    [HarmonyPatch(typeof(GameStartup), "Awake")]
    public class GetGameContextPatch
    {
        static void Postfix(ref GameContext ___gameContext)
        {
            ContextUtilities.GameContext = ___gameContext;
        }
    }
}