namespace PasswordTyper.Forms
{
    partial class ManagePasswords
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
            lvApplications = new ListView();
            SuspendLayout();
            // 
            // lvApplications
            // 
            lvApplications.Dock = DockStyle.Fill;
            lvApplications.FullRowSelect = true;
            lvApplications.GridLines = true;
            lvApplications.Location = new Point(0, 0);
            lvApplications.MultiSelect = false;
            lvApplications.Name = "lvApplications";
            lvApplications.Size = new Size(800, 450);
            lvApplications.TabIndex = 0;
            lvApplications.UseCompatibleStateImageBehavior = false;
            lvApplications.View = View.Details;
            lvApplications.MouseDoubleClick += lvApplications_MouseDoubleClick;
            // 
            // ManagePasswords
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lvApplications);
            Name = "ManagePasswords";
            Text = "ManagePasswords";
            Load += ManagePasswords_Load;
            Resize += ManagePasswords_Resize;
            ResumeLayout(false);
        }

        #endregion

        private ListView lvApplications;
    }
}