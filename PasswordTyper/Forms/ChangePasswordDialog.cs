namespace PasswordTyper.Forms
{
    public partial class ChangePasswordDialog : Form
    {
        public ChangePasswordDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tbNewPassword.Text != tbNewPasswordConfirmation.Text)
            {
                MessageBox.Show("The passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!MeetsPasswordRequirements(tbNewPassword.Text))
            {
                MessageBox.Show("Password must be at least 8 characters long, and contain at least one number.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!TrayApp.ConfigService.VerifyPassword(tbOldPassword.Text))
            {
                MessageBox.Show("The old password you entered is incorrect. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TrayApp.ConfigService.ChangePassword(tbOldPassword.Text, tbNewPassword.Text);

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
    }
}
