using PasswordTyper.Models;

namespace PasswordTyper.Forms
{
    public partial class ManagePasswordPrompt : Form
    {
        ApplicationData? originalApplicationData;

        public ManagePasswordPrompt()
        {
            InitializeComponent();
            ReevaluateEnabled();
        }

        public ManagePasswordPrompt(string processName, string windowTitle)
        {
            InitializeComponent();

            tbProcessName.Text = processName;
            tbWindowTitle.Text = windowTitle;
            ReevaluateEnabled();
        }

        public ManagePasswordPrompt(ApplicationData applicationData)
        {
            InitializeComponent();

            originalApplicationData = applicationData;

            tbProcessName.Text = applicationData.ProcessName;
            tbWindowTitle.Text = applicationData.WindowTitle;
            cbUsername.Checked = applicationData.TypeUsername;
            tbUsername.Text = applicationData.Username;
            cbPassword.Checked = applicationData.TypePassword;
            tbPassword.Text = applicationData.Password;
            cb2FASecret.Checked = applicationData.TypeTotp;
            tb2FASecret.Text = applicationData.TotpSecret;

            ReevaluateEnabled();
        }

        private void ReevaluateEnabled()
        {
            tbUsername.Enabled = cbUsername.Checked;
            lblUsername.Enabled = cbUsername.Checked;

            tbPassword.Enabled = cbPassword.Checked;
            lblPassword.Enabled = cbPassword.Checked;

            tb2FASecret.Enabled = cb2FASecret.Checked;
            lbl2FASecret.Enabled = cb2FASecret.Checked;
        }

        private void cbUsername_CheckedChanged(object sender, EventArgs e)
        {
            ReevaluateEnabled();
        }

        private void cbPassword_CheckedChanged(object sender, EventArgs e)
        {
            ReevaluateEnabled();
        }

        private void cb2FASecret_CheckedChanged(object sender, EventArgs e)
        {
            ReevaluateEnabled();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var application = new ApplicationData()
            {
                ProcessName = tbProcessName.Text,
                WindowTitle = tbWindowTitle.Text,
                TypeUsername = cbUsername.Checked,
                Username = cbUsername.Checked ? tbUsername.Text : "",
                TypePassword = cbPassword.Checked,
                Password = cbPassword.Checked ? tbPassword.Text : "",
                TypeTotp = cb2FASecret.Checked,
                TotpSecret = cb2FASecret.Checked ? tb2FASecret.Text : ""
            };

            if (originalApplicationData != null && originalApplicationData.Equals(application))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!TrayApp.ConfigService.AddOrEditApplication(application))
            {
                return;
            }

            if (originalApplicationData != null &&
                (originalApplicationData.ProcessName != application.ProcessName ||
                originalApplicationData.WindowTitle != application.WindowTitle))
            {
                // Show a warning dialog to the user to inform them they are about to **delete** a password
                var result = MessageBox.Show("You have changed an existing title/process combination, would you like to remove the previous binding?", "Remove Password Binding", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // If the user wants to delete a password, check the password, *again*.
                    var password = PasswordPrompt.PromptPassword();
                    if (!password.correct)
                    {
                        MessageBox.Show("The password you entered is incorrect. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    TrayApp.ConfigService.DeleteApplication(originalApplicationData, password.password);
                }
            }


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cbViewPasswords_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.UseSystemPasswordChar = true;
            tb2FASecret.UseSystemPasswordChar = true;

            if (cbViewPasswords.Checked)
            {
                // If the user wants to view the passwords, check the password, *again*.
                if (PasswordPrompt.PromptPassword().correct)
                {
                    tbPassword.UseSystemPasswordChar = false;
                    tb2FASecret.UseSystemPasswordChar = false;
                }
                else
                {
                    cbViewPasswords.Checked = false;
                    return;
                }
            }
        }

        private void btnDeletePassword_Click(object sender, EventArgs e)
        {
            // Show a warning dialog to the user to inform them they are about to **delete** a password
            var result = MessageBox.Show("Are you sure you want to delete this password? This operation is not reversible!", "Delete Password", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                return;
            }

            // If the user wants to delete a password, check the password, *again*.
            var password = PasswordPrompt.PromptPassword();
            if (!password.correct)
            {
                MessageBox.Show("The password you entered is incorrect. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var application = new ApplicationData()
            {
                ProcessName = tbProcessName.Text,
                WindowTitle = tbWindowTitle.Text,
                TypeUsername = cbUsername.Checked,
                Username = cbUsername.Checked ? tbUsername.Text : "",
                TypePassword = cbPassword.Checked,
                Password = cbPassword.Checked ? tbPassword.Text : "",
                TypeTotp = cb2FASecret.Checked,
                TotpSecret = cb2FASecret.Checked ? tb2FASecret.Text : ""
            };

            TrayApp.ConfigService.DeleteApplication(application, password.password);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
