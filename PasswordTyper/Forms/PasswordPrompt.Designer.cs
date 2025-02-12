namespace PasswordTyper.Forms
{
    partial class PasswordPrompt
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
            components = new System.ComponentModel.Container();
            tbPassword = new TextBox();
            label1 = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            ttPasswordhint = new ToolTip(components);
            tHintTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // tbPassword
            // 
            tbPassword.Font = new Font("Segoe UI", 9F);
            tbPassword.Location = new Point(12, 54);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(350, 23);
            tbPassword.TabIndex = 0;
            tbPassword.UseSystemPasswordChar = true;
            tbPassword.TextChanged += tbPassword_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(325, 30);
            label1.TabIndex = 1;
            label1.Text = "Enter your password.\r\nYour password will be hidden, and will not be stored on disk.";
            // 
            // btnOk
            // 
            btnOk.Font = new Font("Segoe UI", 9F);
            btnOk.Location = new Point(287, 83);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 27);
            btnOk.TabIndex = 2;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 9F);
            btnCancel.Location = new Point(206, 83);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 27);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // ttPasswordhint
            // 
            ttPasswordhint.BackColor = Color.NavajoWhite;
            ttPasswordhint.IsBalloon = true;
            ttPasswordhint.ToolTipTitle = "Password does not meet requirements";
            // 
            // tHintTimer
            // 
            tHintTimer.Interval = 2500;
            tHintTimer.Tick += tHintTimer_Tick;
            // 
            // PasswordPrompt
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(374, 117);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(label1);
            Controls.Add(tbPassword);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "PasswordPrompt";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Password";
            TopMost = true;
            Load += PasswordPrompt_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbPassword;
        private Label label1;
        private Button btnOk;
        private Button btnCancel;
        private ToolTip ttPasswordhint;
        private System.Windows.Forms.Timer tHintTimer;
    }
}