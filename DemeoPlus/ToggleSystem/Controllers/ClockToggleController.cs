using DemeoPlus.Enhancements;
using UnityEngine;

namespace DemeoPlus.ToggleSystem.Controllers
{
    public class ClockToggleController : MonoBehaviour, IToggleController
    {
        public void Enable()
        {
            if (this.ShouldEnable())
            {
                var go = new GameObject($"DemeoPlus_{this.GetDescriptor().Name}")
                    .AddComponent<Clock>();
                go.transform.parent = this.gameObject.transform;
            }
        }

        public void Disable()
        {
            if (!this.ShouldEnable())
            {
                Destroy(this.transform.Find($"DemeoPlus_{this.GetDescriptor().Name}").gameObject);
            }
        }

        public ToggleDescriptor GetDescriptor() => new ToggleDescriptor
        {
            Name = "Clock",
            Tooltip = "An in-game clock."
        };

        public bool ShouldEnable() => ClockConfig.Instance.Enabled;
    }
}