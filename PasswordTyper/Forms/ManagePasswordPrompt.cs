using PasswordTyper.Models;

namespace PasswordTyper.Forms
{
    public partial class ManagePasswordPrompt : Form
    {
        ApplicationData previousApplicationData;
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

            previousApplicationData = applicationData;

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
                Username = tbUsername.Text,
                TypePassword = cbPassword.Checked,
                Password = tbPassword.Text,
                TypeTotp = cb2FASecret.Checked,
                TotpSecret = tb2FASecret.Text
            };

            if (TrayApp.ConfigService.AddOrEditApplication(application))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
