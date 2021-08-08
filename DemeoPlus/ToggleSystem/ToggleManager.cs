using System;
using System.Collections.Generic;
using DemeoPlus.Enhancements;
using DemeoPlus.ToggleSystem.Controllers;
using UnityEngine;

namespace DemeoPlus.ToggleSystem
{
    public class ToggleManager : MonoBehaviour
    {
        private readonly List<IToggleController> _toggleControllers = new List<IToggleController>();

        private void Awake() => DontDestroyOnLoad(this);

        private void Start()
        {
            this._toggleControllers.AddRange(new IToggleController[]
            {
                this.gameObject.AddComponent<StaticCameraToggleController>(),
                this.gameObject.AddComponent<ClockToggleController>(),
                this.gameObject.AddComponent<DiscordToggleController>()
            });


            foreach (var toggleController in this._toggleControllers)
            {
                toggleController.Enable();
            }
        }
    }
}