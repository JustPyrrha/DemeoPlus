using System;
using System.Reflection;
using Boardgame;
using Boardgame.Networking;
using DemeoPlus.Core.Utilities;
using DemeoPlus.Desktop.Utilities;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using DiscordApp = Discord;
using Object = UnityEngine.Object;

namespace DemeoPlus.Desktop.Discord
{
     public class DiscordManager
    {
        private DiscordApp.Discord _discord;
        private DiscordApp.Activity _activity;
        private GameContext _context;
        private INetworkController _networkController;

        public void Init()
        {
            MelonLogger.Msg($"(Discord|INFO): Starting Client.");
            if (DiscordConfig.Instance.EnableDiscord)
            {
                _discord = new DiscordApp.Discord(844058872573722674, (ulong) DiscordApp.CreateFlags.Default);
                _discord.SetLogHook(DiscordApp.LogLevel.Debug, (level, message) => MelonLogger.Msg($"(Discord|{level}): {message}"));
                _discord.GetActivityManager().RegisterSteam(1484280);
                
            
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
            }
        }

        public void HandleSceneUnloaded(string sceneName)
        {
            if(_discord == null) return;
            if (SceneManager.GetActiveScene().name == SceneNames.Lobby)
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
            if(_discord == null) return;
            MelonLogger.Msg($"(Unity|INFO): Active Scene changed. ({sceneName})");
            switch (sceneName)
            {
                case SceneNames.Lobby:
                {
                    DetectNetworking();
                    _activity.Details = "On the Main Menu";
                    _activity.State = "Waiting...";
                    _activity.Assets = new DiscordApp.ActivityAssets
                    {
                        LargeImage = "logo",
                        LargeText = $"v{Application.version} (Unity: v{Application.unityVersion})"
                    };
                    
                    _activity.Timestamps = new DiscordApp.ActivityTimestamps
                    {
                        Start = DateTimeOffset.Now.ToUnixTimeSeconds()
                    };
                    if (DiscordConfig.Instance.ShowParty || DiscordConfig.Instance.DevOnly_EnableJoins)
                    {
                        _activity.Secrets = new DiscordApp.ActivitySecrets();
                        _activity.Party = new DiscordApp.ActivityParty();
                    }
                    break;
                }
                default:
                {
                    try
                    {
                        if (_context.levelManager.GetCurrentGameType().Equals(LevelSequence.GameType.ElvenQueen))
                        {
                            if (_activity.State != "Playing Adventure.")
                            {
                                _activity.Timestamps = new DiscordApp.ActivityTimestamps
                                {
                                    Start = DateTimeOffset.Now.ToUnixTimeSeconds()
                                };
                            }
                            _activity.Details = "Playing The Black Sarcophagus.";
                            _activity.State = "Playing Adventure.";
                            _activity.Assets.LargeImage = "book1";
                            _activity.Assets.LargeText = "Book 1 - The Black Sarcophagus";
                            _activity.Assets.SmallImage = "logo";
                            _activity.Assets.SmallText = $"v{Application.version} (Unity: v{Application.unityVersion})";
                            if (_networkController != null)
                            {
                                if (DiscordConfig.Instance.ShowParty)
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
                                }
                                if (DiscordConfig.Instance.DevOnly_EnableJoins)
                                {
                                    _activity.Secrets = new DiscordApp.ActivitySecrets
                                    {
                                        Join = PhotonNetwork.room.Name, // use just the room code for the join secret
                                        Match = PhotonNetwork.room.Name + "_match",
                                    };
                                }
                            }
                        } else if (_context.levelManager.GetCurrentGameType().Equals(LevelSequence.GameType.RatKing))
                        {
                            if (_activity.State != "Playing Adventure.")
                            {
                                _activity.Timestamps = new DiscordApp.ActivityTimestamps
                                {
                                    Start = DateTimeOffset.Now.ToUnixTimeSeconds()
                                };
                            }
                            _activity.Details = "Playing Realm of the Rat King.";
                            _activity.State = "Playing Adventure.";
                            _activity.Assets.LargeImage = "book2";
                            _activity.Assets.LargeText = "Book 2 - Realm of the Rat King";
                            _activity.Assets.SmallImage = "logo";
                            _activity.Assets.SmallText = $"v{Application.version} (Unity: v{Application.unityVersion})";
                            if (_networkController != null)
                            {
                                if (DiscordConfig.Instance.ShowParty)
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
                                }
                                if (DiscordConfig.Instance.DevOnly_EnableJoins)
                                {
                                    _activity.Secrets = new DiscordApp.ActivitySecrets
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
            
            _discord?.GetActivityManager().UpdateActivity(_activity, result => MelonLogger.Msg($"(Discord|INFO): Setting Activity. ({result})"));
        }

        private void DetectNetworking()
        {
            if(this._networkController != null) return;
            var gameStartupObj = Object.FindObjectOfType<GameStartup>();
            if (gameStartupObj is null) return;
            _context = (GameContext) typeof (GameStartup).GetField("gameContext", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(gameStartupObj);
            this._networkController = _context.networkController;
            MelonLogger.Msg(
                $"(Mod|INFO): Found Photon Network. (Account ID: {PlayerHub.GetAccountIDForPlayer(_networkController.Player.ID)})");
            if (DiscordConfig.Instance.DevOnly_EnableJoins)
            {
                _discord.GetActivityManager().OnActivityJoin += secret => _context.gameStateMachine?.JoinGame(null,
                    secret, s => MelonLogger.Msg("(Discord|INFO): Joining via Discord"),
                    () => MelonLogger.Warning("(Discord|WARN): Failed to join through Discord."));
            }
        }

        public void RunCallbacks()
        {
            _discord?.RunCallbacks();
        }

        public void Dispose()
        {
            _discord?.Dispose();
        }

        public void HandlePreferencesLoaded()
        {
            if (!DiscordConfig.Instance.EnableDiscord)
            {
                _discord?.Dispose();
                _discord = null;
            }
            else if (_discord == null)
            {
                DetectNetworking();
                Init();
            }
            
            if (!DiscordConfig.Instance.ShowParty)
            {
                _activity.Party = new DiscordApp.ActivityParty();
                _discord?.GetActivityManager().UpdateActivity(_activity, result => MelonLogger.Msg($"(Discord|INFO): Clearing Activity. ({result})"));
            }
        }
    }
}