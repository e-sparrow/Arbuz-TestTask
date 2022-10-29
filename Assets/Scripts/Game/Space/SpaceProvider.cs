using System;
using Game.Settings.Interfaces;
using Game.Space.Interfaces;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Game.Space
{
    public class SpaceProvider : ISpaceProvider
    {
        public SpaceProvider(ISpaceSettings settings)
        {
            _settings = settings;
        }

        private const int MaxIterations = 100;
        
        private static readonly LayerMask Mask = ~LayerMask.NameToLayer("Floor");
        private static readonly Vector3 BoxCheckerSize = Vector3.one * 3 + Vector3.up * 10;

        private readonly ISpaceSettings _settings;
        
        public Vector2 GetRandomValidPosition()
        {
            for (int i = 0; i < MaxIterations; i++)
            {
                var position = GetRandomPosition();
                if (IsPositionValid(position))
                {
                    return position;
                }
            }

            throw new Exception("No more space");
            
            Vector2 GetRandomPosition()
            {
                var randomX = Random.value;
                var randomY = Random.value;
            
                var x = Mathf.Lerp(_settings.LowerPoint.x, _settings.HighPoint.x, randomX);
                var y = Mathf.Lerp(_settings.LowerPoint.y, _settings.HighPoint.y, randomY);

                var result = new Vector2(x, y);
                return result;
            }
        }

        public bool IsPositionValid(Vector2 position)
        {
            var size = _settings.HighPoint - _settings.LowerPoint;
            var rect = new Rect(_settings.LowerPoint, size);
            var inBounds = rect.Contains(position);
            
            var point = position.ProjectToPerspective() + Vector3.up * BoxCheckerSize.y / 2;
            var cast = Physics.CheckBox(point, BoxCheckerSize / 2, Quaternion.identity, Mask);

            var result = inBounds && !cast;
            return result;
        }
    }
}