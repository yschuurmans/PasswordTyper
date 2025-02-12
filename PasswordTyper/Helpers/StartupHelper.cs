using Microsoft.Win32;

namespace PasswordTyper.Helpers
{
    internal class StartupHelper
    {

        public static void AddApplicationToStartup()
        {
            string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(runKey, true);

            if (key == null)
                return;

            key.SetValue("PasswordTyper", "\"" + Application.ExecutablePath + "\"");
        }

        public static bool IsApplicationInStartup()
        {
            string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(runKey, true);

            if (key == null)
                return false;

            return key.GetValue("PasswordTyper") != null;
        }

        public static bool RemoveApplicationFromStartup()
        {
            string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(runKey, true);

            if (key == null)
                return false;

            key.DeleteValue("PasswordTyper", false);
            return true;
        }
    }
}
