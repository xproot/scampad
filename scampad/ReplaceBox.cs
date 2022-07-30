using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scampad
{
    public partial class ReplaceBox : Form
    {
        public ReplaceBox()
        {
            InitializeComponent();
        }

        //If the cancel button is clicked
        private void cancelButton_Click(object sender, EventArgs e)
        {
            //Close this dialog
            this.Close();
        }

        //If the text in the Find textbox changes
        private void findTextBox_TextChanged(object sender, EventArgs e)
        {
            //If the Find textbox is empty
            if (findTextBox.Text == String.Empty)
                //Disable the replace all button
                replaceAllButton.Enabled = false;
            //If not
            else
                //Enable it
                replaceAllButton.Enabled = true;
        }

        //If the replaceAllButton is clicked
        private void replaceAllButton_Click(object sender, EventArgs e)
        {
            //About if the Match Case check box is checked
            switch (caseSensitiveCheckBox.Checked)
            {
                //If it is
                case true:
                    //Set text to a Replaced version
                    ((Form1)Owner).notepad.Text = ((Form1)Owner).notepad.Text.Replace(findTextBox.Text, replaceTextBox.Text);
                    break;

                //If it isn't
                case false:
                    //Replace (but everything turned lowercase)
                    ((Form1)Owner).notepad.Text = ((Form1)Owner).notepad.Text.ToLower().Replace(findTextBox.Text.ToLower(), replaceTextBox.Text);
                    break;
            }
        }
    }
}
