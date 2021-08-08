using UnityEngine;
using UnityEngine.SceneManagement;

namespace DemeoPlus.Discord
{
    public class DiscordBehaviour : MonoBehaviour
    {
        private DiscordManager _discordManager;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += this.OnSceneManagerSceneLoaded;
            SceneManager.sceneUnloaded += this.OnSceneManagerSceneUnloaded;
        }

        private void OnSceneManagerSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (this._discordManager == null)
            {
                this._discordManager = new DiscordManager();
                this._discordManager.Init();
            }

            this._discordManager.HandleSceneChange(scene.name);
        }

        private void OnSceneManagerSceneUnloaded(Scene scene)
        {
            this._discordManager.HandleSceneUnloaded(scene.name);
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= this.OnSceneManagerSceneLoaded;
            SceneManager.sceneUnloaded -= this.OnSceneManagerSceneUnloaded;
            this._discordManager.Dispose();
            this._discordManager = null;
        }
        
        public void OnUpdate()
        {
            this._discordManager.RunCallbacks();
        }
    }
}