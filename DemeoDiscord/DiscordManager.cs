using System;
using DemeoDiscord.Utilities;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using DiscordApp = Discord;

namespace DemeoDiscord
{
    public class DiscordManager
    {
        private DiscordApp.Discord _discord;
        private DiscordApp.Activity _activity;

        public void Init()
        {
            MelonLogger.Msg($"(Discord|INFO): Starting RPC Client.");
            _discord = new DiscordApp.Discord(844058872573722674, (ulong) Discord.CreateFlags.Default);
            _discord.SetLogHook(DiscordApp.LogLevel.Debug, (level, message) => MelonLogger.Msg($"(Discord|{level}): {message}"));

            _activity = new DiscordApp.Activity
            {
                Details = "Loading...",
                Timestamps = new DiscordApp.ActivityTimestamps
                {
                    Start = DateTimeOffset.Now.ToUnixTimeSeconds()
                },
                Assets = new DiscordApp.ActivityAssets
                {
                    LargeImage = "logo",
                    LargeText = $"v{Application.version} (Unity: v{Application.unityVersion})"
                }
            };
            _discord?.GetActivityManager().UpdateActivity(_activity, result => MelonLogger.Msg($"(Discord|INFO): Setting Activity. ({result})"));

            SceneManager.sceneUnloaded += SceneManagerOnSceneUnloaded;
        }

        public void SceneManagerOnSceneUnloaded(Scene scene)
        {
            /* Workaround for a funky quirk with LobbySteamVR and SceneManager.activeSceneChanged.
               For some reason when switching from the crypt, shop or an elven floor to LobbySteamVR (for kick, quit etc) SceneManager.activeSceneChanged isn't fired.
               This workaround checks for when a scene is unloaded and sees if the new active scene is LobbySteamVR. 
               */
            if (SceneManager.GetActiveScene().name == SceneNames.Home)
            {
                MelonLogger.Msg($"(Unity|INFO): Active Scene changed. (LobbySteamVR)");
                _activity.Details = "On the Main Menu";
                _activity.State = "Waiting...";
                _activity.Assets.LargeImage = "logo";
                _activity.Assets.LargeText = $"v{Application.version} (Unity: v{Application.unityVersion})";
                    
                _activity.Timestamps = new DiscordApp.ActivityTimestamps
                {
                    Start = DateTimeOffset.Now.ToUnixTimeSeconds()
                };
                
                _discord?.GetActivityManager().UpdateActivity(_activity, result => MelonLogger.Msg($"(Discord|INFO): Setting Activity. ({result})"));
            }
        }

        public void HandleSceneChange(string sceneName)
        {
            MelonLogger.Msg($"(Unity|INFO): Active Scene changed. (\"{sceneName}\")");
            switch (sceneName)
            {
                // Even though we have SceneManagerOnSceneUnloaded keeping this here for if its ever changed.
                case SceneNames.Home:
                {
                    _activity.Details = "On the Main Menu";
                    _activity.State = "Waiting...";
                    _activity.Assets.LargeImage = "logo";
                    _activity.Assets.LargeText = $"v{Application.version} (Unity: v{Application.unityVersion})";
                    
                    _activity.Timestamps = new DiscordApp.ActivityTimestamps
                    {
                        Start = DateTimeOffset.Now.ToUnixTimeSeconds()
                    };
                    break;
                }
                case SceneNames.Lobby:
                {
                    _activity.Details = "In a lobby.";
                    _activity.State = "Waiting...";
                    _activity.Timestamps = new DiscordApp.ActivityTimestamps
                    {
                        Start = DateTimeOffset.Now.ToUnixTimeSeconds()
                    };
                    break;
                }
                default:
                {
                    if (sceneName.StartsWith("ElvenFloor"))
                    {
                        if (_activity.State != "Playing Adventure.")
                        {
                            _activity.Timestamps = new DiscordApp.ActivityTimestamps
                            {
                                Start = DateTimeOffset.Now.ToUnixTimeSeconds()
                            };
                        }
                        _activity.Details = "The Black Sarcophagus.";
                        _activity.State = "Playing Adventure.";
                        _activity.Assets.LargeImage = "book1";
                        _activity.Assets.LargeText = "Book 1 - The Black Sarcophagus";
                        _activity.Assets.SmallImage = "logo";
                        _activity.Assets.SmallText = $"v{Application.version} (Unity: v{Application.unityVersion})";
                    }
                    break;
                }
            }
            
            _discord?.GetActivityManager().UpdateActivity(_activity, result => MelonLogger.Msg($"(Discord|INFO): Setting Activity. ({result})"));
        }

        public void RunCallbacks()
        {
            _discord?.RunCallbacks();
        }

        public void Dispose()
        {
            _discord?.Dispose();
        }
    }
}