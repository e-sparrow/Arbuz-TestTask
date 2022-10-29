using UnityEngine;

namespace Game.Player.Interfaces
{
    public interface IPlayerView
    {
        void Move(Vector2 force);
        
        Vector2 Position
        {
            get;
        }
    }
}