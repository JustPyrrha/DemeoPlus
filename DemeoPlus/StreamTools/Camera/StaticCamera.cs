using DemeoPlus.ToggleSystem;
using DemeoPlus.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace DemeoPlus.StreamTools.Camera
{
    public class StaticCamera : MonoBehaviour
    {
        public UnityEngine.Camera camera;

        private void Start()
        {
            this.gameObject.SetActive(false);
            this.SetupCamera();
        }

        private void SetupCamera()
        {
            var cameraClone = Instantiate(SceneUtils.GetMainCamera(), Vector3.zero, Quaternion.identity,
                this.transform);
            this.camera = cameraClone.GetComponent<UnityEngine.Camera>();
            this.camera.enabled = false;

            this.camera.clearFlags = CameraClearFlags.SolidColor;
            this.camera.stereoTargetEye = StereoTargetEyeMask.None;
            this.camera.targetDisplay = 0;
            this.camera.transform.SetPositionAndRotation(StaticCameraConfig.Instance.Position, Quaternion.Euler(StaticCameraConfig.Instance.Rotation));

            this.camera.enabled = true;
        }
    }
}