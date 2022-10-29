using System;
using Game.Settings.Interfaces;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class SerializableSpaceSettings : ISpaceSettings
    {
        [field: SerializeField]
        public Vector2 LowerPoint
        {
            get;
            private set;
        }

        [field: SerializeField]
        public Vector2 HighPoint
        {
            get;
            private set;
        }
    }
}