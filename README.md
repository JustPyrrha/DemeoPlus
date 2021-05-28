![Demeo Version](https://img.shields.io/static/v1?label=Demeo&message=1.2&color=9cf&style=flat-square&logo=steam)
![Unity Version](https://img.shields.io/static/v1?label=Unity&message=2019.3.4&color=9cf&style=flat-square&logo=unity)
![Discord Version](https://img.shields.io/static/v1?label=DiscordSDK&message=2.5.6&color=9cf&style=flat-square&logo=discord&logoColor=white)
![Mod Version](https://img.shields.io/github/v/release/JoeZwet/DemeoDiscord?include_prereleases&label=DemeoDiscord&style=flat-square)

# DemeoDiscord
Discord integration for Demeo.

## Installation
DemeoDiscord depends on MelonLoader v0.3, available [here](https://github.com/LavaGang/MelonLoader).

1. Go to the releases tab and download the `DemeoDiscord-v{VERSION}.zip` file from the latest release.
2. Extract everything from the zip into the root directory of Demeo.\
    Make sure the `discord_game_sdk.dll` file is in the same folder as the `demeo.exe` file.
3. Profit.

## Planned Features
 * Display party member count/max.
 * Allow game invites via Discord.

## Support
For support, ping `@JoeZwet#0001` in the [Demeo Modding Group Discord server](https://discord.gg/XYphVbfaqh).

## Developers
If you are wanting to develop features for DemeoDiscord you will need to create a `DemeoDiscord/DemeoDiscord.csproj.user` file with the following xml:
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <!-- Set "YOUR OWN" Demeo folder here to resolve most of the dependency paths! -->
    <DemeoDir>C:\Program Files (x86)\Steam\steamapps\common\Demeo</DemeoDir>
  </PropertyGroup>
</Project>
```