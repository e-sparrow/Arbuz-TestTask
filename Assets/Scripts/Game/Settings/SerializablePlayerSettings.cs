using System;
using Game.Settings.Interfaces;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class SerializablePlayerSettings : IPlayerSettings
    {
        [field: SerializeField]
        public float Speed
        {
            get;
            private set;
        }
    }
}