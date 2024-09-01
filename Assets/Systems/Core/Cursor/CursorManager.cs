using UnityEngine;

namespace Systems.Core.Cursor
{
    public static class CursorManager
    {
        public static void SetCursorVisible() => UnityEngine.Cursor.visible = true;
        public static void SetCursorNotVisible() => UnityEngine.Cursor.visible = false;
        public static void LockCursor() => UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        public static void UnlockCursor() => UnityEngine.Cursor.lockState = CursorLockMode.Confined;
    }
}
