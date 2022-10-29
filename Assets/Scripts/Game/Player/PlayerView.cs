using Game.Player.Interfaces;
using UnityEngine;
using Utils;

namespace Game.Player
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public void Move(Vector2 force)
        {
            var projectedForce = new Vector3(force.x, 0, force.y);
            var position = transform.position + projectedForce;
            transform.position = position;
        }

        public Vector2 Position => transform.position.ProjectToOrthographic();
    }
}