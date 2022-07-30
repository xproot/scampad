namespace scampad
{
    partial class ReplaceBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplaceBox));
            this.caseSensitiveCheckBox = new System.Windows.Forms.CheckBox();
            this.replaceAllButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.findLabel = new System.Windows.Forms.Label();
            this.replaceLabel = new System.Windows.Forms.Label();
            this.findTextBox = new System.Windows.Forms.TextBox();
            this.replaceTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // caseSensitiveCheckBox
            // 
            this.caseSensitiveCheckBox.AutoSize = true;
            this.caseSensitiveCheckBox.Location = new System.Drawing.Point(12, 122);
            this.caseSensitiveCheckBox.Name = "caseSensitiveCheckBox";
            this.caseSensitiveCheckBox.Size = new System.Drawing.Size(83, 17);
            this.caseSensitiveCheckBox.TabIndex = 0;
            this.caseSensitiveCheckBox.Text = "Match Case";
            this.caseSensitiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // replaceAllButton
            // 
            this.replaceAllButton.Enabled = false;
            this.replaceAllButton.Location = new System.Drawing.Point(247, 12);
            this.replaceAllButton.Name = "replaceAllButton";
            this.replaceAllButton.Size = new System.Drawing.Size(75, 23);
            this.replaceAllButton.TabIndex = 1;
            this.replaceAllButton.Text = "Replace All";
            this.replaceAllButton.UseVisualStyleBackColor = true;
            this.replaceAllButton.Click += new System.EventHandler(this.replaceAllButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(247, 41);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // findLabel
            // 
            this.findLabel.AutoSize = true;
            this.findLabel.Location = new System.Drawing.Point(9, 17);
            this.findLabel.Name = "findLabel";
            this.findLabel.Size = new System.Drawing.Size(56, 13);
            this.findLabel.TabIndex = 3;
            this.findLabel.Text = "Find what:";
            // 
            // replaceLabel
            // 
            this.replaceLabel.AutoSize = true;
            this.replaceLabel.Location = new System.Drawing.Point(9, 46);
            this.replaceLabel.Name = "replaceLabel";
            this.replaceLabel.Size = new System.Drawing.Size(72, 13);
            this.replaceLabel.TabIndex = 4;
            this.replaceLabel.Text = "Replace with:";
            // 
            // findTextBox
            // 
            this.findTextBox.Location = new System.Drawing.Point(87, 14);
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(154, 20);
            this.findTextBox.TabIndex = 5;
            this.findTextBox.TextChanged += new System.EventHandler(this.findTextBox_TextChanged);
            // 
            // replaceTextBox
            // 
            this.replaceTextBox.Location = new System.Drawing.Point(87, 43);
            this.replaceTextBox.Name = "replaceTextBox";
            this.replaceTextBox.Size = new System.Drawing.Size(154, 20);
            this.replaceTextBox.TabIndex = 6;
            // 
            // ReplaceBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(334, 151);
            this.Controls.Add(this.replaceTextBox);
            this.Controls.Add(this.findTextBox);
            this.Controls.Add(this.replaceLabel);
            this.Controls.Add(this.findLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.replaceAllButton);
            this.Controls.Add(this.caseSensitiveCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 190);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 190);
            this.Name = "ReplaceBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Replace";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox caseSensitiveCheckBox;
        private System.Windows.Forms.Button replaceAllButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label findLabel;
        private System.Windows.Forms.Label replaceLabel;
        private System.Windows.Forms.TextBox findTextBox;
        private System.Windows.Forms.TextBox replaceTextBox;
    }
}