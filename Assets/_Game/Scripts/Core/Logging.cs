using UnityEngine;

namespace Core
{
    public static class Logging
    {
        public static void Log(string message) => UnityLog(message: message);
        public static void Log(string source, string message) => UnityLog(source, message);
                
        public static void Error(string message) => UnityError(message: message);
        public static void Error(string source, string message) => UnityError(source, message);
                
        public static void Warning(string message) => UnityWarning(message: message);
        public static void Warning(string source, string message) => UnityWarning(source, message);

        /////////////////////////////////////

        private static void UnityLog(string source = "", string message ="")
            => Debug.Log(CreateMessage(source, message));

        private static void UnityError(string source = "", string message = "")
            => Debug.LogError(CreateMessage(source, message));

        private static void UnityWarning(string source = "", string message = "")
            => Debug.LogWarning(CreateMessage(source, message));

        /////////////////////////////////////

        private static string CreateMessage(string source = "", string message = "")
            => string.IsNullOrEmpty(source) ? message : $"{source}: {message}";
    }
}
