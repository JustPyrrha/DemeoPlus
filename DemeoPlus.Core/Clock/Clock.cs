using System;
using System.Linq;
using DemeoPlus.Core.Utilities;
using MelonLoader;
using Prototyping;
using TMPro;
using UnityEngine;

namespace DemeoPlus.Core.Clock
{
    public class Clock : MonoBehaviour
    {
        private TextMeshPro _textMesh;
        
        private void Awake()
        {
            MelonLogger.Msg("(Mod|INFO): Creating Clock.");
            
            var canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = RG.ActiveCamera;
            canvas.transform.position = ClockConfig.Instance.Location;
            canvas.transform.rotation = Quaternion.Euler(ClockConfig.Instance.Rotation);
            
            _textMesh = canvas.gameObject.AddComponent<TextMeshPro>();
            _textMesh.gameObject.SetActive(false);
            _textMesh.fontSize = ClockConfig.Instance.FontSize;
            _textMesh.alignment = TextAlignmentOptions.Center;
            _textMesh.text = DateTime.Now.ToString(ClockConfig.Instance.Format);
            _textMesh.font = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().First(x => x.name == "Demeo SDF");
            _textMesh.gameObject.SetActive(true);
        }

        private void Update()
        {
            _textMesh.text = DateTime.Now.ToString(ClockConfig.Instance.Format);
        }
    }
}