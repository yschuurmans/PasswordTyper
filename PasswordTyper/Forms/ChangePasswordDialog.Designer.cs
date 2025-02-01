namespace PasswordTyper.Forms
{
    partial class ChangePasswordDialog
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
            btnCancel = new Button();
            btnOk = new Button();
            label1 = new Label();
            tbOldPassword = new TextBox();
            label2 = new Label();
            tbNewPassword = new TextBox();
            label3 = new Label();
            tbNewPasswordConfirmation = new TextBox();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 9F);
            btnCancel.Location = new Point(206, 171);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 27);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOk
            // 
            btnOk.Font = new Font("Segoe UI", 9F);
            btnOk.Location = new Point(287, 171);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 27);
            btnOk.TabIndex = 6;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(158, 15);
            label1.TabIndex = 5;
            label1.Text = "Enter your current password.";
            // 
            // tbOldPassword
            // 
            tbOldPassword.Font = new Font("Segoe UI", 9F);
            tbOldPassword.Location = new Point(12, 27);
            tbOldPassword.Name = "tbOldPassword";
            tbOldPassword.Size = new Size(350, 23);
            tbOldPassword.TabIndex = 4;
            tbOldPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.Location = new Point(12, 80);
            label2.Name = "label2";
            label2.Size = new Size(142, 15);
            label2.TabIndex = 9;
            label2.Text = "Enter your new password.";
            // 
            // tbNewPassword
            // 
            tbNewPassword.Font = new Font("Segoe UI", 9F);
            tbNewPassword.Location = new Point(12, 98);
            tbNewPassword.Name = "tbNewPassword";
            tbNewPassword.Size = new Size(350, 23);
            tbNewPassword.TabIndex = 8;
            tbNewPassword.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.Location = new Point(12, 124);
            label3.Name = "label3";
            label3.Size = new Size(267, 15);
            label3.TabIndex = 11;
            label3.Text = "Enter your new password again, for confirmation.";
            // 
            // tbNewPasswordConfirmation
            // 
            tbNewPasswordConfirmation.Font = new Font("Segoe UI", 9F);
            tbNewPasswordConfirmation.Location = new Point(12, 142);
            tbNewPasswordConfirmation.Name = "tbNewPasswordConfirmation";
            tbNewPasswordConfirmation.Size = new Size(350, 23);
            tbNewPasswordConfirmation.TabIndex = 10;
            tbNewPasswordConfirmation.UseSystemPasswordChar = true;
            // 
            // ChangePasswordDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(369, 210);
            Controls.Add(label3);
            Controls.Add(tbNewPasswordConfirmation);
            Controls.Add(label2);
            Controls.Add(tbNewPassword);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(label1);
            Controls.Add(tbOldPassword);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ChangePasswordDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Change Password";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnOk;
        private Label label1;
        private TextBox tbOldPassword;
        private Label label2;
        private TextBox tbNewPassword;
        private Label label3;
        private TextBox tbNewPasswordConfirmation;
    }
}