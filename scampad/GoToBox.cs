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
    public partial class GoToBox : Form
    {
        public GoToBox()
        {
            InitializeComponent();
        }

        //When the Go To button is clicked
        private void gotoButton_Click(object sender, EventArgs e)
        {
            bool wasWordWrapon = false;
            //If wordwrap is on
            if (((Form1)Owner).notepad.WordWrap == true)
            {
                //Remember that and turn it off
                wasWordWrapon = true;
                ((Form1)Owner).notepad.WordWrap = false;
            }
            //Check if text is a number
            bool isNum = int.TryParse(lineTextBox.Text, out int line);
            if (!isNum)
            {
                MessageBox.Show("Only numbers are allowed.", "Unacceptable Input", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.Close();
            }
            //Go to position
            int position = 0;
            for (int i = 0; i < line; i++)
            {
                try
                {
                    position += ((Form1)Owner).notepad.Lines[i].Length;
                } catch { }
            }
            ((Form1)Owner).notepad.SelectionStart = position;
            ((Form1)Owner).notepad.ScrollToCaret();
            //Turn wordwrap back on if it was on
            if (wasWordWrapon)
                ((Form1)Owner).notepad.WordWrap = true;
            //Close window
            this.Close();
        }

        //If the cancel button is clicked
        private void cancelButton_Click(object sender, EventArgs e)
        {
            //Close window
            this.Close();
        }

        //If a key is pressed in the line textbox
        private void lineTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Check if it's not a number
            bool isNum = int.TryParse(e.KeyChar.ToString(), out int num);
            if (!isNum)
            {
                //Not handle the press if it's not a number
                e.Handled = false;
                MessageBox.Show("You can only type a number here.", "Unacceptable Character", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
