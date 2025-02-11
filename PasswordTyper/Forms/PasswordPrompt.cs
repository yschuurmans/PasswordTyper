﻿namespace PasswordTyper.Forms
{
    public partial class PasswordPrompt : Form
    {
        public string Password
        {
            get { return tbPassword.Text; }
        }

        public PasswordPrompt()
        {
            InitializeComponent();
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!MeetsPasswordRequirements(tbPassword.Text))
            {
                MessageBox.Show("Password must be at least 8 characters long, and contain at least one number.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool MeetsPasswordRequirements(string password)
        {
            // Password must be at least 8 characters long, and contain at least one number.
            return password.Length >= 8 && password.Any(char.IsDigit);
        }

        public static (bool correct, string password) PromptPassword()
        {
            var passwordPrompt = new PasswordPrompt();
            if (passwordPrompt.ShowDialog() == DialogResult.OK)
            {
                string masterPassword = passwordPrompt.Password;
                try
                {
                    if (!TrayApp.ConfigService.VerifyPassword(masterPassword))
                    {
                        // Show error: password incorrect, try again
                        MessageBox.Show("The password you entered is incorrect. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return PromptPassword();
                    }
                    return (true, masterPassword);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return (false, "");
        }

        private void PasswordPrompt_Load(object sender, EventArgs e)
        {
            // Focus this dialog window
            this.Activate();
        }
    }
}
