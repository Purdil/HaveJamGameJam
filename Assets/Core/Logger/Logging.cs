using UnityEngine;

namespace Core.Logger
{
    public static class Logging
    {
#if UNITY_EDITOR
        public static void Log(string message)
        {
            Debug.Log(message);
        }

        public static void LogError(string message)
        {
            Debug.LogError(message);
        }

        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }
#endif
        
    }
}