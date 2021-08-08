using System;
using Boardgame;
using Boardgame.Networking;
using DemeoPlus.Utilities;
using MelonLoader;
using RGCommon;
using UnityEngine;
using UnityEngine.SceneManagement;
using DiscordApp = Discord;

namespace DemeoPlus.Discord
{
     public class DiscordManager
    {
        private DiscordApp.Discord _discord;
        private DiscordApp.Activity _activity;
        private readonly INetworkController _networkController = ContextUtilities.GameContext.networkController;

        public void Init()
        {
            MelonLogger.Msg($"(Discord|INFO): Starting Client.");
            if (!DiscordConfig.Instance.Enabled) return;
            this._discord = new DiscordApp.Discord(844058872573722674, (ulong) DiscordApp.CreateFlags.Default);
            this._discord.SetLogHook(DiscordApp.LogLevel.Debug, (level, message) => MelonLogger.Msg($"(Discord|{level}): {message}"));
            this._discord.GetActivityManager().RegisterSteam(1484280);
            
            this._activity = new DiscordApp.Activity
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
            this._discord?.GetActivityManager().UpdateActivity(this._activity, result => MelonLogger.Msg($"(Discord|INFO): Setting Activity. ({result})"));
        }

        public void HandleSceneUnloaded(string sceneName)
        {
            if(this._discord == null) return;
            if (SceneManager.GetActiveScene().name != SceneNames.Lobby) return;
            MelonLogger.Msg($"(Unity|INFO): Active Scene changed. (LobbySteamVR)");
            this._activity.Details = "On the Main Menu";
            this._activity.State = "Waiting...";
            this._activity.Assets.LargeImage = "logo";
            this._activity.Assets.LargeText = $"v{Application.version} (Unity: v{Application.unityVersion})";
                    
            this._activity.Timestamps = new DiscordApp.ActivityTimestamps
            {
                Start = DateTimeOffset.Now.ToUnixTimeSeconds()
            };
                
            this._discord?.GetActivityManager().UpdateActivity(this._activity, result => MelonLogger.Msg($"(Discord|INFO): Setting Activity. ({result})"));
        }

        public void HandleSceneChange(string sceneName)
        {
            if(this._discord == null) return;
            MelonLogger.Msg($"(Unity|INFO): Active Scene changed. ({sceneName})");
            switch (sceneName)
            {
                case SceneNames.Lobby:
                {
                    if (this._networkController != null && DiscordConfig.Instance.DevOnly_EnableJoins)
                    {
                        this._discord.GetActivityManager().OnActivityJoin += secret =>
                            ContextUtilities.GameContext.gameStateMachine?.JoinGame(null,
                            secret, s => MelonLogger.Msg("(Discord|INFO): Joining via Discord"),
                            () => MelonLogger.Warning("(Discord|WARN): Failed to join through Discord."));
                    }
                    this._activity.Details = "On the Main Menu";
                    this._activity.State = "Waiting...";
                    this._activity.Assets = new DiscordApp.ActivityAssets
                    {
                        LargeImage = "logo",
                        LargeText = $"v{Application.version} (Unity: v{Application.unityVersion})"
                    };
                    
                    this._activity.Timestamps = new DiscordApp.ActivityTimestamps
                    {
                        Start = DateTimeOffset.Now.ToUnixTimeSeconds()
                    };
                    if (DiscordConfig.Instance.ShowParty || DiscordConfig.Instance.DevOnly_EnableJoins)
                    {
                        this._activity.Secrets = new DiscordApp.ActivitySecrets();
                        this._activity.Party = new DiscordApp.ActivityParty();
                    }
                    break;
                }
                default:
                {
                    try
                    {
                        if (ContextUtilities.GameContext.levelManager.GetCurrentGameType().Equals(LevelSequence.GameType.ElvenQueen))
                        {
                            if (this._activity.State.StartsWith("On "))
                            {
                                this._activity.Timestamps = new DiscordApp.ActivityTimestamps
                                {
                                    Start = DateTimeOffset.Now.ToUnixTimeSeconds()
                                };
                            }
                            this._activity.Details = "Playing The Black Sarcophagus.";
                            this._activity.State = $"On {Locale.GetInstance().GetString($"Ui/levels/levelName/{sceneName}")}";
                            this._activity.Assets.LargeImage = "book1";
                            this._activity.Assets.LargeText = "Book 1 - The Black Sarcophagus";
                            this._activity.Assets.SmallImage = "logo";
                            this._activity.Assets.SmallText = $"v{Application.version} (Unity: v{Application.unityVersion})";
                            if (this._networkController != null)
                            {
                                if (DiscordConfig.Instance.ShowParty)
                                {
                                    this._activity.Party = new DiscordApp.ActivityParty
                                    {
                                        Id = PhotonNetwork.room.Name + "_party",
                                        Size = new DiscordApp.PartySize
                                        {
                                            MaxSize = 4,
                                            CurrentSize = PhotonNetwork.playerList.Length
                                        }
                                    };
                                }
                                if (DiscordConfig.Instance.DevOnly_EnableJoins)
                                {
                                    this._activity.Secrets = new DiscordApp.ActivitySecrets
                                    {
                                        Join = PhotonNetwork.room.Name, // use just the room code for the join secret
                                        Match = PhotonNetwork.room.Name + "_match",
                                    };
                                }
                            }
                        } else if (ContextUtilities.GameContext.levelManager.GetCurrentGameType().Equals(LevelSequence.GameType.RatKing))
                        {
                            if (this._activity.State.StartsWith("On "))
                            {
                                this._activity.Timestamps = new DiscordApp.ActivityTimestamps
                                {
                                    Start = DateTimeOffset.Now.ToUnixTimeSeconds()
                                };
                            }
                            this._activity.Details = "Playing Realm of the Rat King.";
                            this._activity.State = $"On {Locale.GetInstance().GetString($"Ui/levels/levelName/{sceneName}")}";
                            this._activity.Assets.LargeImage = "book2";
                            this._activity.Assets.LargeText = "Book 2 - Realm of the Rat King";
                            this._activity.Assets.SmallImage = "logo";
                            this._activity.Assets.SmallText = $"v{Application.version} (Unity: v{Application.unityVersion})";
                            if (this._networkController != null)
                            {
                                if (DiscordConfig.Instance.ShowParty)
                                {
                                    this._activity.Party = new DiscordApp.ActivityParty
                                    {
                                        Id = PhotonNetwork.room.Name + "_party",
                                        Size = new DiscordApp.PartySize
                                        {
                                            MaxSize = 4,
                                            CurrentSize = PhotonNetwork.playerList.Length
                                        }
                                    };
                                }
                                if (DiscordConfig.Instance.DevOnly_EnableJoins)
                                {
                                    this._activity.Secrets = new DiscordApp.ActivitySecrets
                                    {
                                        Join = PhotonNetwork.room.Name, // use just the room code for the join secret
                                        Match = PhotonNetwork.room.Name + "_match",
                                    };
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // ignore
                    }
                    break;
                }
            }
            
            this._discord?.GetActivityManager().UpdateActivity(this._activity, result => MelonLogger.Msg($"(Discord|INFO): Setting Activity. ({result})"));
        }

        public void RunCallbacks()
        {
            this._discord?.RunCallbacks();
        }

        public void Dispose()
        {
            this._discord?.Dispose();
        }

        public void HandlePreferencesLoaded()
        {
            if (!DiscordConfig.Instance.Enabled)
            {
                this._discord?.Dispose();
                this._discord = null;
            }
            else if (this._discord == null)
            {
                this.Init();
            }
            
            if (!DiscordConfig.Instance.ShowParty)
            {
                this._activity.Party = new DiscordApp.ActivityParty();
                this._discord?.GetActivityManager().UpdateActivity(this._activity, result => MelonLogger.Msg($"(Discord|INFO): Clearing Activity. ({result})"));
            }
        }
    }
}