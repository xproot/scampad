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
    public partial class SaveQuestionBox : Form
    {

        public SaveQuestionBox()
        {
            InitializeComponent();
        }

        //When the form loads
        private void SaveQuestionBox_Load(object sender, EventArgs e)
        {
            //Get the filename
            String filename = ((Form1)Owner).CurrentFile;
            //If the filename is not null
            if (((Form1)Owner).CurrentFile != null)
            {
                //If it's longer than 41 characters
                if (((Form1)Owner).CurrentFile.Length > 41)
                    //Truncate it
                    filename = ((Form1)Owner).CurrentFile.Substring(0, 38) + "...";
                //If not
                else
                    //Just use the variable
                    changesLabel.Text = String.Format(changesLabel.Text, filename);

                if (((Form1)Owner).CurrentFile.Length > 13)
                    this.Size = this.MaximumSize;
            }
            //If it is
            else
                //Use untitled for the text
                changesLabel.Text = String.Format(changesLabel.Text, "Untitled");
        }

        //If the Save button is clicked
        private void yesButton_Click(object sender, EventArgs e)
        {
            //Run the save document function
            ((Form1)Owner).SaveDocument(this, null);
        }

        //If the Don't Save button is clicked
        private void noButton_Click(object sender, EventArgs e)
        {
            //Close this form
            this.Close();
        }

        //If the Cancel button is clicked
        private void cancelButton_Click(object sender, EventArgs e)
        {
            //Set variable do Close to false, so Form doesn't close
            ((Form1)Owner).doClose = false;
            this.Close();
        }
    }
}
