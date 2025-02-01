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

            RefreshItems();

            AdjustColumnWidths();
        }

        private void RefreshItems()
        {
            lvApplications.Items.Clear();
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
        }

        private void AdjustColumnWidths()
        {
            foreach (ColumnHeader column in lvApplications.Columns)
            {
                column.Width = (lvApplications.Width - 5) / lvApplications.Columns.Count;
            }
        }

        private void ManagePasswords_Resize(object sender, EventArgs e)
        {
            AdjustColumnWidths();
        }

        private void lvApplications_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // open the ManagePasswordPrompt form with the selected application data
            if (lvApplications.SelectedItems.Count > 0)
            {
                var processName = lvApplications.SelectedItems[0].SubItems[0].Text;
                var windowTitle = lvApplications.SelectedItems[0].SubItems[1].Text;
                OpenApplicationDataForm(processName, windowTitle);

            }
        }

        private void OpenApplicationDataForm(string processName, string windowTitle)
        {
            var applicationData = TrayApp.ConfigService.GetApplicationData(processName, windowTitle);
            if (applicationData != null)
            {
                var applicationDataForm = new ManagePasswordPrompt(applicationData);
                applicationDataForm.ShowDialog();
            }
            else
            {
                var applicationDataForm = new ManagePasswordPrompt(processName, windowTitle);
                applicationDataForm.ShowDialog();
            }
            RefreshItems();
        }
    }
}
