namespace PasswordTyper.Forms
{
    public partial class ManagePasswords : Form
    {
        public ManagePasswords()
        {
            InitializeComponent();
        }

        private void ManagePasswords_Load(object sender, EventArgs e)
        {
            if (!TrayAppActions.PromptPasswordAndDecryptData())
            {
                this.Close();
                return;
            }

            lvApplications.Columns.Add("Process Name");
            lvApplications.Columns.Add("Window Title");
            lvApplications.Columns.Add("Username");
            lvApplications.Columns.Add("Password", 200);
            lvApplications.Columns.Add("2FA Secret");

            foreach (var application in TrayApp.ConfigService.GetApplicationList())
            {
                var item = new ListViewItem(new string[]
                {
                    application.ProcessName,
                    application.WindowTitle,
                    application.TypeUsername ? application.Username : "",
                    application.TypePassword ? application.MaskedPassword : "",
                    application.TypeTotp ? application.MaskedTotpSecret : ""
                });
                lvApplications.Items.Add(item);
            }

            AdjustColumnWidths();
        }

        private void AdjustColumnWidths()
        {
            foreach (ColumnHeader column in lvApplications.Columns)
            {
                column.Width = lvApplications.Width / lvApplications.Columns.Count;
            }
        }

        private void ManagePasswords_Resize(object sender, EventArgs e)
        {
            AdjustColumnWidths();
        }

        private void lvApplications_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void OpenApplicationDataForm(string processName, string windowTitle)
        {
            var applicationData = TrayApp.ConfigService.GetApplicationData(processName, windowTitle);
            var applicationDataForm = new ManagePasswordPrompt(processName, windowTitle, applicationData);
            applicationDataForm.ShowDialog();
        }
    }
}
