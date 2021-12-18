using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace KaosesTweaks.Utils
{
    public static class IM
    {
        public static bool logToFile = false;
        public static bool Debug = false;
        public static string PrePrend = "";

        /**
         * colour codes https://cssgenerator.org/rgba-and-hex-color-generator.html
         * colour codes https://quantdev.ssri.psu.edu/sites/qdev/files/Tutorial_ColorR.html
         * 
         * Ux.ShowMessage("CustomSpawns " + version + " is now enabled. Enjoy! :)", Color.ConvertStringToColor("#001FFFFF"));
         */
        private static void ShowMessage(string message, Color messageColor, bool logToFile = false)
        {
            InformationManager.DisplayMessage(new InformationMessage(PrePrend + " : " + message, messageColor));
            if (logToFile)
            {
                logMessage(message);
            }
        }

        private static void logMessage(string message)
        {
            Logging.Lm(message + "; GameVersion: " + Statics.GameVersion + "; ModVersion: " + Statics.ModVersion);
        }

        public static void MessageInfo(string message)
        {
            ShowMessage(message, Color.ConvertStringToColor("#42FF00FF"), logToFile);
        }


        public static void MessageError(string message)
        {
            ShowMessage(message, Color.ConvertStringToColor("#FF000000"));
            logMessage(message);
        }

        public static void DisplayModLoadedMessage()
        {
            MessageInfo("Loaded");
        }

        public static void DisplayModHarmonyErrorMessage()
        {
            MessageInfo("Will not function properly with out Harmony");
        }


        //~ BT
        public static void ColorRedMessage(string message)
        {
            ShowMessage(message, Color.ConvertStringToColor("#FF0042FF"), logToFile);
        }

        public static void ColorGreenMessage(string message)
        {
            ShowMessage(message, Color.ConvertStringToColor("#42FF00FF"), logToFile);
        }

        public static void ColorBlueMessage(string message)
        {
            ShowMessage(message, Color.ConvertStringToColor("#0042FFFF"), logToFile);
        }

        public static void ColorOrangeMessage(string message)
        {
            ShowMessage(message, Color.ConvertStringToColor("#00F16D26"), logToFile);
        }

        public static void QuickInformationMessage(string message)
        {
            //IM.ShowMessage(message, Color.ConvertStringToColor("#42FF00FF"), logToFile);
            InformationManager.AddQuickInformation(new TextObject(message, null), 0, null, "");
        }


        [Conditional("DEBUG")]
        public static void DebugMessage(string message)
        {
            MessageDebug(message);
        }
        //~ BT



        public static void MessageDebug(string message)
        {
            if (Debug)
            {
                ShowMessage(message, Color.ConvertStringToColor("#E6FF00FF"), true);
            }
        }

        // From Modlib---
        public static void ShowError(string message, string title = "", Exception? exception = null, bool ShowVersionsInfo = true)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                title = "";
            }
            message += "\n\n" + exception?.ToStringFull();
            if (ShowVersionsInfo)
            {
                message += "\n\nGameVersion: " + Statics.GameVersion + "\nModVersion: " + Statics.ModVersion;
            }
            logMessage(title + "\n" + message);
            MessageBox.Show(message, title);
        }

        public static string ToStringFull(this Exception ex) => ex != null ? GetString(ex) : "";

        private static string GetString(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            GetStringRecursive(ex, sb);
            sb.AppendLine();
            sb.AppendLine("Stack trace:");
            sb.AppendLine(ex.StackTrace);
            return sb.ToString();
        }

        private static void GetStringRecursive(Exception ex, StringBuilder sb)
        {
            while (true)
            {
                sb.AppendLine(ex.GetType().Name + ":");
                sb.AppendLine(ex.Message);
                if (ex.InnerException == null)
                {
                    return;
                }

                sb.AppendLine();
                ex = ex.InnerException;
            }
        }



    }
}
