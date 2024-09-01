using UnityEngine;

namespace Systems.Core.Extensions
{
    public static class TransformExtensions
    {
        public static Transform FindWithTag(this Transform transform, string tag)
        {
            foreach (Transform child in transform)
                if (child.CompareTag(tag))
                    return child;

            return null;
        }
    }
}