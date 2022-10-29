using UnityEngine;

namespace Game.Settings.Interfaces
{
    public interface ISpaceSettings
    {
        Vector2 LowerPoint
        {
            get;
        }

        Vector2 HighPoint
        {
            get;
        }
    }
}