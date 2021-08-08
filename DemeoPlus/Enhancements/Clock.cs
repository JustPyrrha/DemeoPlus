using System;
using System.Linq;
using Prototyping;
using TMPro;
using UnityEngine;

namespace DemeoPlus.Enhancements
{
    public class Clock : MonoBehaviour
    {
        private TextMeshPro _textMesh;
        
        private void Start()
        {
            var canvas = this.gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = RG.ActiveCamera;
            canvas.transform.position = ClockConfig.Instance.Position;
            canvas.transform.rotation = Quaternion.Euler(ClockConfig.Instance.Rotation);
            
            this._textMesh = canvas.gameObject.AddComponent<TextMeshPro>();
            this._textMesh.gameObject.SetActive(false);
            this._textMesh.fontSize = ClockConfig.Instance.FontSize;
            this._textMesh.alignment = TextAlignmentOptions.Center;
            this._textMesh.text = DateTime.Now.ToString(ClockConfig.Instance.Format);
            this._textMesh.font = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().First(x => x.name == "Demeo SDF");
            this._textMesh.gameObject.SetActive(true);
        }

        private void Update()
        {
            this._textMesh.text = DateTime.Now.ToString(ClockConfig.Instance.Format);
        }
    }
}