using DemeoPlus.Discord;
using UnityEngine;

namespace DemeoPlus.ToggleSystem.Controllers
{
    public class DiscordToggleController : MonoBehaviour, IToggleController
    {
        public void Enable()
        {
            if (this.ShouldEnable())
            {
                var go = new GameObject($"DemeoPlus_{this.GetDescriptor().Name}")
                    .AddComponent<DiscordBehaviour>();
                go.transform.parent = this.gameObject.transform;
            }
        }

        public void Disable()
        {
            if (!this.ShouldEnable())
            {
                Destroy(this.gameObject.transform.Find($"DemeoPlus_{this.GetDescriptor().Name}").gameObject);
            }
        }

        public ToggleDescriptor GetDescriptor() => new ToggleDescriptor
        {
            Name = "Discord",
            Tooltip = "Presence integration with Discord using the DiscordGameSDK."
        };

        public bool ShouldEnable() => DiscordConfig.Instance.Enabled;
    }
}