namespace PalWorld_Server_Edit
{
    partial class frmPalworld
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnLoad = new Button();
            DlgLoad = new OpenFileDialog();
            DlgSave = new SaveFileDialog();
            txtServLoc = new TextBox();
            btnSave = new Button();
            Pnl1 = new Panel();
            btnUpdate = new Button();
            lblUpdate = new Label();
            SuspendLayout();
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(504, 12);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(75, 23);
            btnLoad.TabIndex = 0;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // txtServLoc
            // 
            txtServLoc.Location = new Point(12, 12);
            txtServLoc.Name = "txtServLoc";
            txtServLoc.PlaceholderText = "\\steamapps\\common\\PalServer\\Pal\\Saved\\Config\\WindowsServer\\PalWorldSettings.ini";
            txtServLoc.ReadOnly = true;
            txtServLoc.Size = new Size(486, 23);
            txtServLoc.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(251, 530);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // Pnl1
            // 
            Pnl1.AutoScroll = true;
            Pnl1.Location = new Point(12, 41);
            Pnl1.Name = "Pnl1";
            Pnl1.Size = new Size(558, 483);
            Pnl1.TabIndex = 4;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(495, 530);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 5;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Visible = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // lblUpdate
            // 
            lblUpdate.AutoSize = true;
            lblUpdate.Font = new Font("Segoe UI", 12F);
            lblUpdate.ForeColor = Color.Red;
            lblUpdate.Location = new Point(344, 530);
            lblUpdate.Name = "lblUpdate";
            lblUpdate.Size = new Size(145, 21);
            lblUpdate.TabIndex = 6;
            lblUpdate.Text = "Update is available!";
            lblUpdate.Visible = false;
            // 
            // frmPalworld
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 565);
            Controls.Add(lblUpdate);
            Controls.Add(btnUpdate);
            Controls.Add(Pnl1);
            Controls.Add(btnSave);
            Controls.Add(txtServLoc);
            Controls.Add(btnLoad);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "frmPalworld";
            Text = "Palworld server setting editor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLoad;
        private OpenFileDialog DlgLoad;
        private SaveFileDialog DlgSave;
        private TextBox txtServLoc;
        private Button btnSave;
        private Panel Pnl1;
        private Button btnUpdate;
        private Label lblUpdate;
    }
}
