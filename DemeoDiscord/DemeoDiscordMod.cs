using MelonLoader;

namespace DemeoDiscord
{
    public class DemeoDiscordMod : MelonMod
    {
        private readonly DiscordManager _discord = new DiscordManager();
        
        public override void OnApplicationStart()
        {
            _discord.Init();
        }

        public override void OnApplicationQuit()
        {
            _discord.Dispose();
        }

        public override void OnUpdate()
        {
            _discord.RunCallbacks();
        }
    }
}