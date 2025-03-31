using OtpNet;
using PasswordTyper.Models;
using System.Text.RegularExpressions;
using System.Web;

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

            btnDeletePassword.Visible = false;
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
            btnDeletePassword.Visible = true;

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

        private void btn_GetFromClipboard_Click(object sender, EventArgs e)
        {
            Get2FASecretFromClipboard();
        }

        private void Get2FASecretFromClipboard()
        {
            {
                // get the image currently on the clipboard
                // attempt to read the 2FA secret from the image, it might not be a perfect fit QR code
                // if it fails, show an error message
                // if it succeeds, fill in the 2FA secret textbox

                if (!Clipboard.ContainsImage())
                {
                    MessageBox.Show("There is no image on the clipboard.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var image = Clipboard.GetImage();
                if (image == null)
                {
                    MessageBox.Show("There was an error reading the image from the clipboard.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Read the QR code from the photo in the clipboard, similar to how a phone camera would capture a QR code from the camera
                var reader = new ZXing.Windows.Compatibility.BarcodeReader();
                reader.Options.TryHarder = true;
                reader.Options.PossibleFormats = new List<ZXing.BarcodeFormat> { ZXing.BarcodeFormat.QR_CODE };

                var bitmap = new Bitmap(image);
                var result = reader.Decode(bitmap);

                if (result == null)
                {
                    MessageBox.Show("No (valid) QR code found in the image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // The result would be an otpauth link, like: otpauth://totp/LabelOfTheWebsite?secret=SECRETSECRETSECRET&issuer=WebsiteName
                // First, validate if it is a valid otpauth link, and if it is, 
                var otpauthPattern = @"^otpauth://totp/[^?]+\?(.*&)?secret=([^&]+)(&.*)?$";
                var match = Regex.Match(result.Text, otpauthPattern);

                if (!match.Success)
                {
                    MessageBox.Show($"The QR code found in the image does not contain a valid 2FA link.\r\nThe QR code contained: \"{result.Text}\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //get the Label, the issuer and the secret
                var uri = new Uri(result.Text);
                var query = HttpUtility.ParseQueryString(uri.Query);
                var label = uri.AbsolutePath.Split('/').Last();
                var issuer = query.Get("issuer");
                var secret = query.Get("secret");

                try
                {
                    var secretBytes = Base32Encoding.ToBytes(secret);

                    // Show a message box, asking if the user if "Found a valid 2FA code! Would you like to add the code for "Label": "Issuer"? "
                    var message = $"Found a valid 2FA code! Would you like to add the code for \"{label}\": \"{issuer}\"?";
                    var result2 = MessageBox.Show(message, "2FA Code Found", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result2 == DialogResult.Yes)
                    {
                        tb2FASecret.Text = secret;
                    }
                }
                catch
                {
                    MessageBox.Show($"The QR code found in the image does not contain a valid secret.\r\nThe QR code contained: \"{result.Text}\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
