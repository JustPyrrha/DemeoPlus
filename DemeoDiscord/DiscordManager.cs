using System;
using System.Reflection;
using Boardgame;
using Boardgame.Networking;
using DemeoDiscord.Utilities;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using DiscordApp = Discord;
using Object = UnityEngine.Object;

namespace DemeoDiscord
{
    public class DiscordManager
    {
        private DiscordApp.Discord _discord;
        private DiscordApp.Activity _activity;
        private INetworkController _networkController;

        public void Init()
        {
            MelonLogger.Msg($"(Discord|INFO): Starting RPC Client.");
            _discord = new DiscordApp.Discord(844058872573722674, (ulong) Discord.CreateFlags.Default);
            _discord.SetLogHook(DiscordApp.LogLevel.Debug, (level, message) => MelonLogger.Msg($"(Discord|{level}): {message}"));
            _discord.GetActivityManager().RegisterSteam(1484280);
            _discord.GetActivityManager().OnActivityJoin += secret => PhotonNetwork.JoinRoom(secret);
            
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
                case SceneNames.Home:
                {
                    DetectNetworking();
                    _activity.Details = "On the Main Menu";
                    _activity.State = "Waiting...";
                    _activity.Assets.LargeImage = "logo";
                    _activity.Assets.LargeText = $"v{Application.version} (Unity: v{Application.unityVersion})";
                    
                    _activity.Timestamps = new DiscordApp.ActivityTimestamps
                    {
                        Start = DateTimeOffset.Now.ToUnixTimeSeconds()
                    };
                    if (ModConfigManager.ModConfig.EnableExperimentalFeatures)
                    {
                        _activity.Secrets = new DiscordApp.ActivitySecrets();
                        _activity.Party = new DiscordApp.ActivityParty();
                    }
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
                    
                    if (_networkController != null)
                    {
                        _activity.Party = new DiscordApp.ActivityParty
                        {
                            Id = PhotonNetwork.room.Name + "_party",
                            Size = new DiscordApp.PartySize
                            {
                                MaxSize = 4,
                                CurrentSize = PhotonNetwork.playerList.Length
                            }
                        };
                        if (ModConfigManager.ModConfig.EnableExperimentalFeatures)
                        {
                            _activity.Secrets = new DiscordApp.ActivitySecrets
                            {
                                Join = PhotonNetwork.room.Name, // use just the room code for the join secret
                                Match = PhotonNetwork.room.Name + "_match",
                            };
                        }
                    }
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
                        if (_networkController != null)
                        {
                            _activity.Party = new DiscordApp.ActivityParty
                            {
                                Id = PhotonNetwork.room.Name + "_party",
                                Size = new DiscordApp.PartySize
                                {
                                    MaxSize = 4,
                                    CurrentSize = PhotonNetwork.playerList.Length
                                }
                            };
                            if (ModConfigManager.ModConfig.EnableExperimentalFeatures)
                            {
                                _activity.Secrets = new DiscordApp.ActivitySecrets
                                {
                                    Join = PhotonNetwork.room.Name, // use just the room code for the join secret
                                    Match = PhotonNetwork.room.Name + "_match",
                                };
                            }
                        }
                    }
                    break;
                }
            }
            
            _discord?.GetActivityManager().UpdateActivity(_activity, result => MelonLogger.Msg($"(Discord|INFO): Setting Activity. ({result})"));
        }

        private void DetectNetworking()
        {
            if(this._networkController != null) return;
            var gameStartupObj = Object.FindObjectOfType<GameStartup>();
            if (gameStartupObj is null) return;
            var gameContext = (GameContext) typeof (GameStartup).GetField("gameContext", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(gameStartupObj);
            this._networkController = gameContext.networkController;
            MelonLogger.Msg($"(Mod|INFO): Found Photon Network. (Account ID: {PlayerHub.GetAccountIDForPlayer(_networkController.Player.ID)})");
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