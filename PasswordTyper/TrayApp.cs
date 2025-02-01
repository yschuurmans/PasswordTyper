using PasswordTyper.Forms;
using PasswordTyper.Models;
using PasswordTyper.Services;
using System.Runtime.InteropServices;

namespace PasswordTyper
{
    public class TrayApp : ApplicationContext
    {
        private NotifyIcon trayIcon;
        public static readonly ConfigService ConfigService = new ConfigService();
        private bool IsProcessingHotkey = false;

        private ManagePasswords ManagePasswordsWindow;

        public TrayApp()
        {
            trayIcon = new NotifyIcon()
            {
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                ContextMenuStrip = new ContextMenuStrip()
            };

            trayIcon.DoubleClick += ManagePasswords;
            trayIcon.ContextMenuStrip.Items.Add("Manage Passwords", null, ManagePasswords);
            trayIcon.ContextMenuStrip.Items.Add("Change Master Password", null, ChangePassword);
            trayIcon.ContextMenuStrip.Items.Add("Exit", null, Exit);
            trayIcon.Visible = true;

            //double clicking also opens ManagePasswords

            if (!ConfigService.ConfigExists())
            {
                MessageBox.Show(
                    "It seems you have never started this application before. In order to use this application, please choose a master password.\r\n\r\nThis password will only be used to encrypt your data, but will not be stored on disk.",
                    "Welcome",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Show the PasswordPrompt form, and wait for it to return the password
                var passwordPrompt = new PasswordPrompt();
                if (passwordPrompt.ShowDialog() == DialogResult.OK)
                {
                    string masterPassword = passwordPrompt.Password;
                    ConfigService.InitializeNewConfig(masterPassword);
                }
            }

            ManagePasswordsWindow = new ManagePasswords();

            RegisterHotKey();
            Application.AddMessageFilter(new HotkeyMessageFilter(1, TypePassword));
        }


        private void TypePassword()
        {
            if (IsProcessingHotkey)
            {
                return;
            }
            IsProcessingHotkey = true;
            // Wait until all the keys have been released before typing
            while ((Control.ModifierKeys & Keys.Control) == Keys.Control ||
                   (Control.ModifierKeys & Keys.Shift) == Keys.Shift ||
                   (Control.ModifierKeys & Keys.Alt) == Keys.Alt ||
                   IsKeyPressed(Keys.V))
            {
                Thread.Sleep(50);
            }

            TrayAppActions.TypePassword();

            IsProcessingHotkey = false;
        }

        private void ManagePasswords(object? sender, EventArgs e)
        {
            // Open forms window
            if (ManagePasswordsWindow.IsDisposed)
            {
                ManagePasswordsWindow = new ManagePasswords();
                ManagePasswordsWindow.Show();
            }
            else
            {
                ManagePasswordsWindow.Show();
            }
        }

        private void ChangePassword(object sender, EventArgs e)
        {
            // Open forms window
            // If any forms are already open, give an error message telling to close all forms first.
            if (ManagePasswordsWindow != null && !ManagePasswordsWindow.IsDisposed)
            {
                MessageBox.Show("Please close the Manage Passwords window before changing your master password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var changePasswordDialog = new ChangePasswordDialog();
            changePasswordDialog.ShowDialog();
        }

        private void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void RegisterHotKey()
        {
            if (!RegisterHotKey(IntPtr.Zero, 1, MOD_CONTROL | MOD_SHIFT | MOD_ALT, Keys.V.GetHashCode()))
            {
                MessageBox.Show("Failed to register hotkey.\r\nThere seems to be another application that already uses the CTRL+ALT+SHIFT+V hotkey.\r\nThe application will now exit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Task.Run(Application.Exit);
            }
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        private const int MOD_CONTROL = 0x2;
        private const int MOD_SHIFT = 0x4;
        private const int MOD_ALT = 0x1;


        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        private bool IsKeyPressed(Keys key)
        {
            return (GetAsyncKeyState(key) & 0x8000) != 0;
        }
    }
}
