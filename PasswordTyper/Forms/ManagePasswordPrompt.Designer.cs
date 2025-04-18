﻿namespace PasswordTyper.Forms
{
    partial class ManagePasswordPrompt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbProcessName = new TextBox();
            lblGuide = new Label();
            lblApplicationProcess = new Label();
            lblWindowTitle = new Label();
            tbWindowTitle = new TextBox();
            tbUsername = new TextBox();
            cbUsername = new CheckBox();
            lblUsername = new Label();
            lblPassword = new Label();
            cbPassword = new CheckBox();
            tbPassword = new TextBox();
            lbl2FASecret = new Label();
            cb2FASecret = new CheckBox();
            tb2FASecret = new TextBox();
            btnCancel = new Button();
            btnOk = new Button();
            cbViewPasswords = new CheckBox();
            btnDeletePassword = new Button();
            btn_GetFromClipboard = new Button();
            SuspendLayout();
            // 
            // tbProcessName
            // 
            tbProcessName.Location = new Point(12, 79);
            tbProcessName.Name = "tbProcessName";
            tbProcessName.Size = new Size(620, 23);
            tbProcessName.TabIndex = 0;
            // 
            // lblGuide
            // 
            lblGuide.AutoSize = true;
            lblGuide.Location = new Point(12, 9);
            lblGuide.Name = "lblGuide";
            lblGuide.Size = new Size(620, 30);
            lblGuide.TabIndex = 1;
            lblGuide.Text = "In this window you can enter your application details and application password. \r\nAfter confirming the details, you will be prompted for your master password, in order to store the information safely.";
            // 
            // lblApplicationProcess
            // 
            lblApplicationProcess.AutoSize = true;
            lblApplicationProcess.Location = new Point(12, 61);
            lblApplicationProcess.Name = "lblApplicationProcess";
            lblApplicationProcess.Size = new Size(111, 15);
            lblApplicationProcess.TabIndex = 2;
            lblApplicationProcess.Text = "Application Process";
            // 
            // lblWindowTitle
            // 
            lblWindowTitle.AutoSize = true;
            lblWindowTitle.Location = new Point(12, 105);
            lblWindowTitle.Name = "lblWindowTitle";
            lblWindowTitle.Size = new Size(76, 15);
            lblWindowTitle.TabIndex = 4;
            lblWindowTitle.Text = "Window Title";
            // 
            // tbWindowTitle
            // 
            tbWindowTitle.Location = new Point(12, 123);
            tbWindowTitle.Name = "tbWindowTitle";
            tbWindowTitle.Size = new Size(620, 23);
            tbWindowTitle.TabIndex = 3;
            // 
            // tbUsername
            // 
            tbUsername.Location = new Point(12, 202);
            tbUsername.Name = "tbUsername";
            tbUsername.Size = new Size(620, 23);
            tbUsername.TabIndex = 5;
            // 
            // cbUsername
            // 
            cbUsername.AutoSize = true;
            cbUsername.Location = new Point(12, 162);
            cbUsername.Name = "cbUsername";
            cbUsername.Size = new Size(193, 19);
            cbUsername.TabIndex = 7;
            cbUsername.Text = "Should the Username be typed?";
            cbUsername.UseVisualStyleBackColor = true;
            cbUsername.CheckedChanged += cbUsername_CheckedChanged;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(12, 184);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(60, 15);
            lblUsername.TabIndex = 8;
            lblUsername.Text = "Username";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(12, 253);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.TabIndex = 11;
            lblPassword.Text = "Password";
            // 
            // cbPassword
            // 
            cbPassword.AutoSize = true;
            cbPassword.Location = new Point(12, 231);
            cbPassword.Name = "cbPassword";
            cbPassword.Size = new Size(190, 19);
            cbPassword.TabIndex = 10;
            cbPassword.Text = "Should the Password be typed?";
            cbPassword.UseVisualStyleBackColor = true;
            cbPassword.CheckedChanged += cbPassword_CheckedChanged;
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(12, 271);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(620, 23);
            tbPassword.TabIndex = 9;
            tbPassword.UseSystemPasswordChar = true;
            // 
            // lbl2FASecret
            // 
            lbl2FASecret.AutoSize = true;
            lbl2FASecret.Location = new Point(12, 322);
            lbl2FASecret.Name = "lbl2FASecret";
            lbl2FASecret.Size = new Size(160, 15);
            lbl2FASecret.TabIndex = 14;
            lbl2FASecret.Text = "2FA *Secret* (not a 2FA code)";
            // 
            // cb2FASecret
            // 
            cb2FASecret.AutoSize = true;
            cb2FASecret.Location = new Point(12, 300);
            cb2FASecret.Name = "cb2FASecret";
            cb2FASecret.Size = new Size(177, 19);
            cb2FASecret.TabIndex = 13;
            cb2FASecret.Text = "Should a 2FA code be typed?";
            cb2FASecret.UseVisualStyleBackColor = true;
            cb2FASecret.CheckedChanged += cb2FASecret_CheckedChanged;
            // 
            // tb2FASecret
            // 
            tb2FASecret.Location = new Point(12, 340);
            tb2FASecret.Name = "tb2FASecret";
            tb2FASecret.Size = new Size(484, 23);
            tb2FASecret.TabIndex = 12;
            tb2FASecret.UseSystemPasswordChar = true;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 9F);
            btnCancel.Location = new Point(476, 369);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 27);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOk
            // 
            btnOk.Font = new Font("Segoe UI", 9F);
            btnOk.Location = new Point(557, 369);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 27);
            btnOk.TabIndex = 15;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // cbViewPasswords
            // 
            cbViewPasswords.AutoSize = true;
            cbViewPasswords.Font = new Font("Segoe UI", 7F);
            cbViewPasswords.Location = new Point(12, 380);
            cbViewPasswords.Name = "cbViewPasswords";
            cbViewPasswords.Size = new Size(92, 16);
            cbViewPasswords.TabIndex = 17;
            cbViewPasswords.Text = "View Passwords";
            cbViewPasswords.UseVisualStyleBackColor = true;
            cbViewPasswords.CheckedChanged += cbViewPasswords_CheckedChanged;
            // 
            // btnDeletePassword
            // 
            btnDeletePassword.Font = new Font("Segoe UI", 9F);
            btnDeletePassword.Location = new Point(362, 369);
            btnDeletePassword.Name = "btnDeletePassword";
            btnDeletePassword.Size = new Size(108, 27);
            btnDeletePassword.TabIndex = 18;
            btnDeletePassword.Text = "Delete Password";
            btnDeletePassword.UseVisualStyleBackColor = true;
            btnDeletePassword.Click += btnDeletePassword_Click;
            // 
            // btn_GetFromClipboard
            // 
            btn_GetFromClipboard.Font = new Font("Segoe UI", 9F);
            btn_GetFromClipboard.Location = new Point(502, 336);
            btn_GetFromClipboard.Name = "btn_GetFromClipboard";
            btn_GetFromClipboard.Size = new Size(130, 27);
            btn_GetFromClipboard.TabIndex = 19;
            btn_GetFromClipboard.Text = "Get From Clipboard";
            btn_GetFromClipboard.UseVisualStyleBackColor = true;
            btn_GetFromClipboard.Click += btn_GetFromClipboard_Click;
            // 
            // ManagePasswordPrompt
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(654, 407);
            Controls.Add(btn_GetFromClipboard);
            Controls.Add(btnDeletePassword);
            Controls.Add(cbViewPasswords);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(lbl2FASecret);
            Controls.Add(cb2FASecret);
            Controls.Add(tb2FASecret);
            Controls.Add(lblPassword);
            Controls.Add(cbPassword);
            Controls.Add(tbPassword);
            Controls.Add(lblUsername);
            Controls.Add(cbUsername);
            Controls.Add(tbUsername);
            Controls.Add(lblWindowTitle);
            Controls.Add(tbWindowTitle);
            Controls.Add(lblApplicationProcess);
            Controls.Add(lblGuide);
            Controls.Add(tbProcessName);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ManagePasswordPrompt";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ManagePasswordPrompt";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbProcessName;
        private Label lblGuide;
        private Label lblApplicationProcess;
        private Label lblWindowTitle;
        private TextBox tbWindowTitle;
        private TextBox tbUsername;
        private CheckBox cbUsername;
        private Label lblUsername;
        private Label lblPassword;
        private CheckBox cbPassword;
        private TextBox tbPassword;
        private Label lbl2FASecret;
        private CheckBox cb2FASecret;
        private TextBox tb2FASecret;
        private Button btnCancel;
        private Button btnOk;
        private CheckBox cbViewPasswords;
        private Button btnDeletePassword;
        private Button btn_GetFromClipboard;
    }
}