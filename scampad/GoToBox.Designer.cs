namespace scampad
{
    partial class GoToBox
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoToBox));
            this.lineLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.gotoButton = new System.Windows.Forms.Button();
            this.lineTextBox = new System.Windows.Forms.TextBox();
            this.balloonTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lineLabel
            // 
            this.lineLabel.AutoSize = true;
            this.lineLabel.Location = new System.Drawing.Point(11, 10);
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(68, 13);
            this.lineLabel.TabIndex = 0;
            this.lineLabel.Text = "Line number:";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(162, 66);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // gotoButton
            // 
            this.gotoButton.Location = new System.Drawing.Point(81, 66);
            this.gotoButton.Name = "gotoButton";
            this.gotoButton.Size = new System.Drawing.Size(75, 23);
            this.gotoButton.TabIndex = 3;
            this.gotoButton.Text = "Go To";
            this.gotoButton.UseVisualStyleBackColor = true;
            this.gotoButton.Click += new System.EventHandler(this.gotoButton_Click);
            // 
            // lineTextBox
            // 
            this.lineTextBox.Location = new System.Drawing.Point(14, 30);
            this.lineTextBox.Name = "lineTextBox";
            this.lineTextBox.Size = new System.Drawing.Size(223, 20);
            this.lineTextBox.TabIndex = 1;
            this.lineTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lineTextBox_KeyPress);
            // 
            // balloonTip
            // 
            this.balloonTip.AutoPopDelay = 2000;
            this.balloonTip.InitialDelay = 500;
            this.balloonTip.IsBalloon = true;
            this.balloonTip.ReshowDelay = 100;
            this.balloonTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            this.balloonTip.ToolTipTitle = "Unacceptable Character";
            // 
            // GoToBox
            // 
            this.AcceptButton = this.gotoButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(249, 101);
            this.Controls.Add(this.gotoButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.lineTextBox);
            this.Controls.Add(this.lineLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoToBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Go To Line";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lineLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button gotoButton;
        private System.Windows.Forms.TextBox lineTextBox;
        private System.Windows.Forms.ToolTip balloonTip;
    }
}