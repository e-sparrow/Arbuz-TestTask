using UnityEngine;

namespace Game.Space.Interfaces
{
    public interface ISpaceProvider
    {
        Vector2 GetRandomValidPosition();
        
        bool IsPositionValid(Vector2 position);
    }
}