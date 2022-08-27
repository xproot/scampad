using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;

namespace scampad
{
    public partial class Form1 : Form
    {
        //Variables
        readonly Random rand = new Random();
        Font OriginalFont;
        String DesignTitle;
        String DefaultZoom;
        String DesignLnStatus;
        String OriginalText;
        internal String CurrentFile;
        readonly String[] Vocabulary = new string[] {" this is not easy", "wbasbd", " what happened?", " please give me your creditcard info sir",
            " this costs $1400 per month", " not", " please no", " do not do it", " go to your bank", "this is not important" };
        readonly String[] FontVocabulary = new string[] { "Webdings", "Comic Sans MS", "Impact", "Segoe Script", "Symbol" };
        readonly PageSetupDialog psd = new PageSetupDialog();
        readonly PrintDialog prd = new PrintDialog();
        readonly PrintDocument pd = new PrintDocument(); 
        bool isChanged = false;
        internal bool doClose = true;

        //Import the ShellAboutA function from shell32.dll
        [DllImport("shell32.dll")]
        public static extern Int32 ShellAboutA(
            IntPtr hWnd,
            string szApp,
            string szOtherStuff,
            IntPtr hIcon);

        //Function ran when an Interaction happened with the notepad textbox
        public void OnInteraction()
        {
            //If the selection is longer than 0
            if (notepad.SelectionLength > 0)
            {
                //Enable some menu items
                DeleteMenuItem.Enabled = true;
                CutMenuItem.Enabled = true;
                CopyMenuItem.Enabled = true;
                SearchBingMenuItem.Enabled = true;
                //If random number spanning from 0 to 1k is bigger than 905
                if (rand.Next(0, 1000) > 905)
                    //Search selection with bing
                    SearchBing(null, null);
                //blah blah is lower than 100
                if (rand.Next(0, 1000) < 100)
                    //Delete the selection
                    notepad.SelectedText = "";
            } else //If the selection is not longer than 0 
            {
                //KEEP IN MIND THIS IS RAN EVERYTIME YOU INTERACT WITH THE TEXTBOX
                //Thus why the random chances might seem higher than they really are.

                //Disable some menu items
                DeleteMenuItem.Enabled = false;
                CutMenuItem.Enabled = false;
                CopyMenuItem.Enabled = false;
                SearchBingMenuItem.Enabled = false;
                //If random number 0-1000 is smaller than 4
                if (rand.Next(0,1000) < 4)
                    notepad.Text = notepad.Text.Replace("scan", "scam");
                //If random number 0-1000 is smaller than 8
                if (rand.Next(0, 1000) < 8)
                    OriginalFont = new Font(FontVocabulary[rand.Next(0, FontVocabulary.Length - 1)], OriginalFont.Size); notepad.Font = new Font(OriginalFont.FontFamily, notepad.Font.Size);
                //Rand is smaller than 10
                if (rand.Next(0, 1000) < 4)
                    //Add a random word from the vocabulary array
                    notepad.AppendText(Vocabulary[rand.Next(0, Vocabulary.Length - 1)]);
                //Rand is smaller than 25
                //if (rand.Next(0, 1000) < 12)
                    //Sleep for *random* 1 second to 15 seconds
                    //Thread.Sleep(rand.Next(1000, 15000));
            }
            //Update Line and Column text
            lineStatusLabel.Text = String.Format(DesignLnStatus, (notepad.GetLineFromCharIndex(notepad.SelectionStart) + 1), ((notepad.SelectionStart - notepad.GetFirstCharIndexFromLine(notepad.GetLineFromCharIndex(notepad.SelectionStart))) + 1 ));
        }

        #region Form1
        public Form1()
        {
            InitializeComponent();
        }

        //When the form loads (application load basically)
        private void Form1_Load(object sender, EventArgs e)
        {
            OriginalFont = notepad.Font;
            //If random number between 0-1000 is above 900
            if (rand.Next(0,1000) > 900)
            {
                //Show message boxes and close
                MessageBox.Show("Error 56, No further information", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                MessageBox.Show("Notepad experienced an unrecoverable error, please try again later.", "Crash!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            //Some variables
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
                //If the arguments contain a valid file path
                if (File.Exists(Environment.GetCommandLineArgs()[1]))
                {
                    //Open that file
                    CurrentFile = Environment.GetCommandLineArgs()[1];
                    this.Text = String.Format(DesignTitle, Path.GetFileName(CurrentFile));
                    OriginalText = File.ReadAllText(Environment.GetCommandLineArgs()[1]);
                    notepad.Text = OriginalText;
                }
            } catch { }
        }

        //If the form is closing
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //If the file has changed
            if (isChanged)
            {
                //Show a "Want to save?" Dialog
                SaveQuestionBox sqb = new SaveQuestionBox();
                sqb.ShowDialog(this);
                sqb.Dispose();
                //If we dont want to close
                if (!doClose)
                {
                    //Stop closing
                    e.Cancel = true;
                    doClose = true;
                }
            }
        }
        #endregion

        #region notepad Text Box
        private void NotepadTextChanged(object sender, EventArgs e)
        {
            if (OriginalText != notepad.Text)
            {
                if (!isChanged)
                    this.Text = "*" + this.Text;
                isChanged = true;
            }
            else
            {
                if (this.Text.Substring(0, 1) == "*")
                    this.Text = this.Text.Substring(1);
                isChanged = false;
            }
            OnInteraction();
        }

        private void NotepadOnKeyUp(object sender, KeyEventArgs e)
        {
            OnInteraction();
        }

        private void NotepadOnMouseUp(object sender, MouseEventArgs e)
        {
            OnInteraction();
        }

        private void NotepadDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void NotepadDragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            try
            {
                MessageBox.Show(e.Data.GetData("System.String", true).ToString());
            }
            catch { }
        }
        #endregion

        #region File Menu Strip
        private void NewDocument(object sender, EventArgs e)
        {
            if (isChanged)
            {
                //Show a "Want to save?" Dialog
                SaveQuestionBox sqb = new SaveQuestionBox();
                sqb.ShowDialog(this);
                sqb.Dispose();
                //If we don't want to close the document
                if (!doClose)
                {
                    //Some variable cleanup lol
                    doClose = true;
                }
                //If we do
                else {
                    isChanged = false;
                    this.Text = String.Format(DesignTitle, "Untitled");
                    notepad.Text = "";
                }
            }
        }

        private void NewWindow(object sender, EventArgs e)
        {
            Process.Start("notepad");
        }

        private void OpenDocument(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Open",
                Filter = "Text Documents|*.txt|All Files (*.*)|*.*",
                Multiselect = false
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(ofd.FileName))
                {
                    CurrentFile = ofd.FileName;
                    OriginalText = File.ReadAllText(ofd.FileName, Encoding.UTF8);
                    if (this.Text.Substring(0, 1) == "*")
                        this.Text = this.Text.Substring(1);
                    isChanged = false;
                    notepad.Text = OriginalText;
                    this.Text = String.Format(DesignTitle, Path.GetFileName(ofd.FileName));
                }
                else
                    MessageBox.Show("File does not exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            ofd.Dispose();
        }

        internal void SaveDocument(object sender, EventArgs e)
        {
            if (CurrentFile != null && CurrentFile != String.Empty)
            {
                File.WriteAllText(CurrentFile, notepad.Text);
                OriginalText = notepad.Text;
                if (this.Text.Substring(0, 1) == "*")
                    this.Text = this.Text.Substring(1);
                isChanged = false;
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Title = "Save",
                    Filter = "Text Documents|*.txt|All Files (*.*)|*.*"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    OriginalText = notepad.Text;
                    try
                    {
                        File.WriteAllText(sfd.FileName, notepad.Text, Encoding.UTF8);
                        CurrentFile = sfd.FileName;
                    }
                    catch { MessageBox.Show("Unable to save!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
                    isChanged = false;
                    this.Text = String.Format(DesignTitle, Path.GetFileName(sfd.FileName));
                } else { doClose = false; }
                sfd.Dispose();
            }
        }

        private void SaveDocumentAs(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Save As",
                Filter = "Text Documents|*.txt|All Files (*.*)|*.*"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                OriginalText = notepad.Text;
                try
                {
                    File.WriteAllText(sfd.FileName, notepad.Text, Encoding.UTF8);
                    CurrentFile = sfd.FileName;
                }
                catch { MessageBox.Show("Unable to save!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
                isChanged = false;
                this.Text = String.Format(DesignTitle, Path.GetFileName(sfd.FileName));
            }
            sfd.Dispose();
        }

        private void DocumentPageSetup(object sender, EventArgs e)
        {
            psd.ShowDialog();
        }

        private void PrintDocument(object sender, EventArgs e)
        {
            prd.ShowDialog();
        }

        private void NotepadExit(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Edit Menu Strip
        private void DocumentUndo(object sender, EventArgs e)
        {
            notepad.Undo();
        }

        private void DocumentCut(object sender, EventArgs e)
        {
            notepad.Cut();
        }

        private void DocumentCopy(object sender, EventArgs e)
        {
            notepad.Copy();
        }

        private void DocumentPaste(object sender, EventArgs e)
        {
            notepad.Paste();
        }

        private void DocumentDelete(object sender, EventArgs e)
        {
            notepad.SelectedText = "";
        }

        private void SearchBing(object sender, EventArgs e)
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
            ReplaceBox rebo = new ReplaceBox();
            rebo.ShowDialog(this);
            rebo.Dispose();
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToBox gobo = new GoToBox();
            gobo.ShowDialog(this);
            gobo.Dispose();
        }

        private void DocumentSelectAll(object sender, EventArgs e)
        {
            notepad.SelectAll();
        }

        private void DocumentInsertDate(object sender, EventArgs e)
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
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = notepad.Font;
            DialogResult _result = fontDialog.ShowDialog();
            if (_result == DialogResult.OK)
            {
                OriginalFont = fontDialog.Font;
                restoreDefaultZoomToolStripMenuItem_Click(null, null);
            }
        }
        #endregion

        #region View Menu Strip
        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Font = new Font(notepad.Font.FontFamily, notepad.Font.Size * 2F);
            zoomStatusLabel.Text = String.Format(DefaultZoom, Math.Round((notepad.Font.Size / OriginalFont.Size) * 100).ToString());
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Font = new Font(notepad.Font.FontFamily, notepad.Font.Size / 2F);
            zoomStatusLabel.Text = String.Format(DefaultZoom, Math.Round((notepad.Font.Size / OriginalFont.Size) * 100).ToString());
        }

        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Font = OriginalFont;
            zoomStatusLabel.Text = String.Format(DefaultZoom, Math.Round((notepad.Font.Size / OriginalFont.Size) * 100).ToString());
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
            Process.Start("https://go.microsoft.com/fwlink/?LinkId=834783");
        }

        private void feedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("feedback-hub:");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShellAboutA(this.Handle, "Notepad", "", this.Icon.Handle);
        }
        #endregion
    }
}
