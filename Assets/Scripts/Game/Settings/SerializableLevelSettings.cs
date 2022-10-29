using System;
using Game.Settings.Interfaces;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class SerializableLevelSettings : ILevelSettings
    {
        [field: SerializeField]
        public float LoadingTimerLength
        {
            get;
            private set;
        }

        [field: SerializeField]
        public float InGameTimerLength
        {
            get;
            private set;
        }

        [field: SerializeField]
        public int CoinsCount
        {
            get;
            private set;
        }
    }
}