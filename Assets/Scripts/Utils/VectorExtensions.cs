using UnityEngine;

namespace Utils
{
    public static class VectorExtensions
    {
        public static Vector2 ProjectToOrthographic(this Vector3 self)
        {
            var result = new Vector2(self.x, self.z);
            return result;
        }

        public static Vector3 ProjectToPerspective(this Vector2 self)
        {
            var result = new Vector3(self.x, 0, self.y);
            return result;
        }
    }
}