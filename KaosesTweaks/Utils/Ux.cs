using TaleWorlds.Core;
using TaleWorlds.Library;


namespace KaosesTweaks.Utils
{
    public static class Ux
    {
        public static bool logToFile = false;
        public static bool Debug = false;

        /**
         * colour codes https://cssgenerator.org/rgba-and-hex-color-generator.html
         * colour codes https://quantdev.ssri.psu.edu/sites/qdev/files/Tutorial_ColorR.html
         * 
         * Ux.ShowMessage("CustomSpawns " + version + " is now enabled. Enjoy! :)", Color.ConvertStringToColor("#001FFFFF"));
         */
        private static void ShowMessage(string message, Color messageColor, bool logToFile = false)
        {
            InformationManager.DisplayMessage(new InformationMessage(message, messageColor));
            if (logToFile)
            {
                logMessage(message);
            }
        }

        private static void logMessage(string message)
        {
            Logging.Lm(message);
        }

        public static void MessageInfo(string message)
        {
            Ux.ShowMessage(message, Color.ConvertStringToColor("#42FF00FF"), logToFile);
        }

        public static void MessageDebug(string message)
        {
            if (Debug)
            {
                Ux.ShowMessage(message, Color.ConvertStringToColor("#E6FF00FF"), true);
            }
        }
        public static void MessageError(string message)
        {
            Ux.ShowMessage(message, Color.ConvertStringToColor("#FF000000"));
            logMessage(message);
        }

    }
}
