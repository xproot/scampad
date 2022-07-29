using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Threading;

namespace scampad
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        String DesignTitle;
        String DefaultZoom;
        String DesignLnStatus;
        String OriginalText;
        String CurrentFile;
        String[] vocabulary = new string[] {" this is not easy", "wbasbd", " what happened?", " please give me your creditcard info sir",
            " this costs $1400 per month", " not", " please no", " do not do it", " go to your bank", "this is not important" };
        PageSetupDialog psd = new PageSetupDialog();
        PrintDialog prd = new PrintDialog();
        PrintDocument pd = new PrintDocument(); 
        bool ischanged = false;

        public void OnSelectionUpdate()
        {
            if (notepad.SelectionLength > 0)
            {
                deleteToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                searchWithBingToolStripMenuItem.Enabled = true;
                if (rand.Next(0, 100) > 95)
                    searchWithBingToolStripMenuItem_Click(null, null);
                if (rand.Next(0, 100) < 10)
                    notepad.SelectedText = "";
            } else
            {
                deleteToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                searchWithBingToolStripMenuItem.Enabled = false;
                if (rand.Next(0,100) < 5)
                {
                    notepad.Text = notepad.Text.Replace("scan", "scam");
                }
                if (rand.Next(0, 100) < 1)
                {
                    notepad.AppendText(vocabulary[rand.Next(0, vocabulary.Length - 1)]);
                }
                if (rand.Next(0, 100) < 1)
                {
                    Thread.Sleep(rand.Next(1000, 15000));
                }
            }
            lineStatusLabel.Text = String.Format(DesignLnStatus, (notepad.GetLineFromCharIndex(notepad.SelectionStart) + 1), ((notepad.SelectionStart - notepad.GetFirstCharIndexFromLine(notepad.GetLineFromCharIndex(notepad.SelectionStart))) + 1 ));
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (rand.Next(0,100) > 90)
            {
                MessageBox.Show("Error 56, No further information", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                MessageBox.Show("Notepad experienced an unrecoverable error, please try again later.", "Crash!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            pd.DocumentName = "Document";
            psd.Document = pd;
            DesignTitle = this.Text;
            DesignLnStatus = lineStatusLabel.Text;
            DefaultZoom = zoomStatusLabel.Text;
            zoomStatusLabel.Text = String.Format(DefaultZoom, "100");
            lineStatusLabel.Text = String.Format(DesignLnStatus, "1", "1");
            this.Text = String.Format(DesignTitle, "Untitled");
            OriginalText = notepad.Text;
            try
            {
                if (File.Exists(Environment.GetCommandLineArgs()[1]))
                {
                    CurrentFile = Environment.GetCommandLineArgs()[1];
                    this.Text = String.Format(DesignTitle, Path.GetFileName(CurrentFile));
                    OriginalText = File.ReadAllText(Environment.GetCommandLineArgs()[1]);
                    notepad.Text = OriginalText;
                }
            } catch { }
        }

        #region notepad Text Box
        private void notepad_TextChanged(object sender, EventArgs e)
        {
            if (OriginalText != notepad.Text)
            {
                if (!ischanged)
                    this.Text = "*" + this.Text;
                ischanged = true;
            }
            else
            {
                if (this.Text.Substring(0, 1) == "*")
                    this.Text = this.Text.Substring(1);
                ischanged = false;
            }
            OnSelectionUpdate();
        }

        private void notepad_KeyUp(object sender, EventArgs e)
        {
            OnSelectionUpdate();
        }

        private void notepad_MouseUp(object sender, MouseEventArgs e)
        {
            OnSelectionUpdate();
        }
        #endregion

        #region File Menu Strip
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = String.Format(DesignTitle, "Untitled");
            notepad.Text = "";
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("notepad");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open";
            ofd.Filter = "Text Documents|*.txt|All Files (*.*)|*.*";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(ofd.FileName))
                {
                    CurrentFile = ofd.FileName;
                    OriginalText = File.ReadAllText(ofd.FileName, Encoding.UTF8);
                    if (this.Text.Substring(0, 1) == "*")
                        this.Text = this.Text.Substring(1);
                    ischanged = false;
                    notepad.Text = OriginalText;
                    this.Text = String.Format(DesignTitle, Path.GetFileName(ofd.FileName));
                }
                else
                    MessageBox.Show("File does not exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            ofd.Dispose();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentFile != null && CurrentFile != String.Empty)
            {
                File.WriteAllText(CurrentFile, notepad.Text);
                OriginalText = notepad.Text;
                if (this.Text.Substring(0, 1) == "*")
                    this.Text = this.Text.Substring(1);
                ischanged = false;
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save";
                sfd.Filter = "Text Documents|*.txt|All Files (*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    OriginalText = notepad.Text;
                    try
                    {
                        File.WriteAllText(sfd.FileName, notepad.Text, Encoding.UTF8);
                    }
                    catch { MessageBox.Show("Unable to save!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
                    ischanged = false;
                    this.Text = String.Format(DesignTitle, Path.GetFileName(sfd.FileName));
                }
                sfd.Dispose();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save As";
            sfd.Filter = "Text Documents|*.txt|All Files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                OriginalText = notepad.Text;
                try
                {
                    File.WriteAllText(sfd.FileName, notepad.Text, Encoding.UTF8);
                }
                catch { MessageBox.Show("Unable to save!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
                ischanged = false;
                this.Text = String.Format(DesignTitle, Path.GetFileName(sfd.FileName));
            }
            sfd.Dispose();
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            psd.ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prd.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Edit Menu Strip
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.SelectedText = "";
        }

        private void searchWithBingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://bing.com/search?q=" + HttpUtility.UrlEncode(notepad.SelectedText));
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Something happened.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Something happened.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void findPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Something happened.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Something happened.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Something happened.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.SelectAll();
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.SelectedText = DateTime.Now.ToString();
        }

        #endregion

        #region Format Menu Strip
        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (wordWrapToolStripMenuItem.Checked)
            {
                case true:
                    notepad.WordWrap = true;
                    break;

                case false:
                    notepad.WordWrap = false;
                    break;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Something happened.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        #endregion

        #region View Menu Strip
        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Font = new System.Drawing.Font(notepad.Font.FontFamily, notepad.Font.Size + 0.5F);
            zoomStatusLabel.Text = String.Format(DefaultZoom, Math.Round((notepad.Font.Size / 11F) * 100).ToString());
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Font = new System.Drawing.Font(notepad.Font.FontFamily, notepad.Font.Size - 0.5F);
            zoomStatusLabel.Text = String.Format(DefaultZoom, Math.Round((notepad.Font.Size / 11F) * 100).ToString());
        }

        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Font = new System.Drawing.Font(notepad.Font.FontFamily, 11F);
            zoomStatusLabel.Text = String.Format(DefaultZoom, Math.Round((notepad.Font.Size / 11F) * 100).ToString());
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (statusBarToolStripMenuItem.Checked)
            {
                case true:
                    bottomStrip.Visible = true;
                    break;

                case false:
                    bottomStrip.Visible = false;
                    break;
            }
        }
        #endregion

        #region Help Menu Strip
        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Something happened.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void feedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("feedback-hub:");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("winver");
        }
        #endregion
    }
}
