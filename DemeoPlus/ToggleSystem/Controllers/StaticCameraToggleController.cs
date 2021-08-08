using DemeoPlus.StreamTools.Camera;
using UnityEngine;

namespace DemeoPlus.ToggleSystem.Controllers
{
    public class StaticCameraToggleController : MonoBehaviour, IToggleController
    {
        private StaticCamera _camera;
        
        public void Enable()
        {
            if (this.ShouldEnable())
            {
                var go = new GameObject($"DemeoPlus_{this.GetDescriptor().Name}");
                this._camera = go.AddComponent<StaticCamera>();
                go.transform.parent = this.gameObject.transform;
            }
        }

        public void Disable()
        {
            if (!this.ShouldEnable())
            {
                this._camera.enabled = false;
                Destroy(this.transform.Find($"DemeoPlus_{this.GetDescriptor().Name}").gameObject);
            }
        }

        public ToggleDescriptor GetDescriptor() => new ToggleDescriptor
        {
            Name = "StaticCamera",
            Tooltip = "A static camera above the game board, useful for content creation."
        };

        public bool ShouldEnable() => StaticCameraConfig.Instance.Enabled;
    }
}