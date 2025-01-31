using PasswordTyper.Forms;
using PasswordTyper.Helpers;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace PasswordTyper
{
    internal class TrayAppActions
    {

        internal static void TypePassword()
        {
            // Get exe and window title
            var (processName, windowTitle) = GetActiveProcessAndWindow();
            if (string.IsNullOrEmpty(processName) && string.IsNullOrEmpty(windowTitle))
            {
                return;
            }

            if (!TrayApp.ConfigService.IsDecrypted())
            {
                if (!PromptPasswordAndDecryptData())
                {
                    return;
                }
            }

            var appData = TrayApp.ConfigService.GetApplicationData(processName, windowTitle);
            if (appData == null)
            {
                // YesNo Dialog, asking if the user wants to add this application to the list
                // If yes, show the ManagePassword form, with the "public ManagePasswords(string processName, string windowTitle)" ctor
                var result = MessageBox.Show("This application is currently not known yet.\r\nWould you like to add this application to the list?", "Add Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var managePasswords = new ManagePasswordPrompt(processName, windowTitle);
                    managePasswords.Show();
                }
            }
            else
            {
                if (appData.TypeUsername)
                {
                    SendKeys.Send(appData.Username);
                    if (appData.TypePassword || appData.TypeTotp)
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                if (appData.TypePassword)
                {
                    SendKeys.Send(appData.Password);
                    if (appData.TypeTotp)
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                if (appData.TypeTotp)
                {
                    var totpKey = TotpHelper.GenerateTotp(appData.TotpSecret);
                    SendKeys.Send(totpKey);
                }
            }
        }

        /// <summary>
        /// Prompt for password and decrypt the Config
        /// </summary>
        internal static bool PromptPasswordAndDecryptData()
        {
            var masterPassword = PasswordPrompt.PromptPassword();
            if (!masterPassword.correct)
            {
                return false;
            }
            TrayApp.ConfigService.DecryptData(masterPassword.password);
            return true;
        }

        private static (string processName, string windowTitle) GetActiveProcessAndWindow()
        {
            IntPtr hwnd = GetForegroundWindow();
            if (hwnd == IntPtr.Zero)
            {
                return (string.Empty, string.Empty);
            }

            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process process = Process.GetProcessById((int)pid);

            StringBuilder windowText = new StringBuilder(256);
            GetWindowText(hwnd, windowText, 256);

            return (process.ProcessName, windowText.ToString());
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    }
}
