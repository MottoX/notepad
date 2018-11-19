using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
namespace notepad
{
    public delegate void trunTextHandler(object sender, String text);
    public delegate void findTextHandler(object sender, String text, bool UpOrDown, bool sensitive);
    public delegate void replaceTextHandler(object sender, String text, String pattern, bool UpOrDown, bool sensitive);
    public delegate void replaceAllTextHandler(object sender, String text, String patter, bool UpOrDown, bool sensitive);
    public partial class pad : Form
    {
        public pad()
        {
            InitializeComponent();
            statusShow();
        }
        CompareInfo compare = CultureInfo.InvariantCulture.CompareInfo;
        private String filePath = String.Empty;
        private String fileName = "无标题";
        #region 辅助方法
        private void statusShow()
        {
            int index = noteText.GetFirstCharIndexOfCurrentLine();//得到当前行第一个字符的索引!!
            int row = noteText.GetLineFromCharIndex(index) + 1;//得到当前位置的行号
            int col = noteText.SelectionStart - index + 1;//得到当前位置的列
            infoLabel.Text = String.Format("第{0}行，第{1}列", row, col);
        }
        private RichTextBoxStreamType getType(String path) 
        {
            RichTextBoxStreamType streamType;
            switch (path)
            {
                case "txt": // 文本文件
                    streamType = RichTextBoxStreamType.PlainText; break;
                case "rtf": // 写字板
                    streamType = RichTextBoxStreamType.RichText; break;
                case "srt": // 影片字幕
                    streamType = RichTextBoxStreamType.UnicodePlainText; break;
                case "lrc": // 歌词文件
                    streamType = RichTextBoxStreamType.PlainText; break;
                case "xml": // XML文件
                    streamType = RichTextBoxStreamType.PlainText; break;
                default: // 默认类型
                    streamType = RichTextBoxStreamType.PlainText; break;
            }
            return streamType;
        }
        private void SaveFile()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = @"C:/";
            dialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            dialog.RestoreDirectory = true;
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String path = dialog.FileName;
                StreamWriter sw = new StreamWriter(path, false);
                sw.Write(noteText.Text);
                sw.Close();
                FileInfo fileInfo = new FileInfo(path);
                fileName = fileInfo.Name;
                filePath = path;
                Text = fileName + " - 记事本";
                noteText.Modified = false;
            }
        }
        #endregion
        #region 文件菜单
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileName == "无标题" && noteText.Text == String.Empty)
                    return;
            if (noteText.Modified == true)
            {
                var choice=MessageBox.Show("是否保存当前文件到" + fileName + "?", "记事本", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (choice == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (choice == DialogResult.Cancel)
                    return;
            }
            Text = "无标题 - 记事本";
            noteText.Text = String.Empty;
            fileName = "无标题";
            filePath = String.Empty;
        }
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory=@"C:/";
            dialog.Filter="文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            dialog.RestoreDirectory = true;
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String path = dialog.FileName;
                String pathExt = Path.GetExtension(path);
                RichTextBoxStreamType type = getType(pathExt);
                noteText.LoadFile(path,type);
                FileInfo fileInfo = new FileInfo(path);
                fileName = fileInfo.Name;
                filePath = path;
                Text = fileName+" - 记事本";
                noteText.Modified = false;
            }

        }
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileName == "无标题")
                SaveFile();
            else
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                sw.Write(noteText.Text);
                sw.Close();
                noteText.Modified = false;
            }
        }
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
            noteText.Modified = false;
        }
        private void 页面设置toolStripMenuItem_Click(object sender, EventArgs e)
        {
            PageSetupDialog  dialog = new PageSetupDialog();
            dialog.Document = printDocument;
            dialog.ShowDialog();   
        }
        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            printDocument.DocumentName = noteText.Text;
            dialog.Document = printDocument;             
            if (dialog.ShowDialog() == DialogResult.OK)   
            {
                try
                {
                    printDocument.Print();           
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);       
                }
            }     
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
        #region 编辑菜单
        private void 编辑ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if (noteText.Modified == false)
                撤销ToolStripMenuItem.Enabled = false;
            else
                撤销ToolStripMenuItem.Enabled = true;
            if (noteText.SelectedText == "")
            {
                剪切ToolStripMenuItem.Enabled = false;
                复制ToolStripMenuItem.Enabled = false;
                删除ToolStripMenuItem.Enabled = false;
            }
            else
            {
                剪切ToolStripMenuItem.Enabled = true;
                复制ToolStripMenuItem.Enabled = true;
                删除ToolStripMenuItem.Enabled = true;
            }
            if (noteText.Text == "")
            {
                查找ToolStripMenuItem.Enabled = false;
                替换ToolStripMenuItem.Enabled = false;
            }
            else
            {
                查找ToolStripMenuItem.Enabled = true;
                替换ToolStripMenuItem.Enabled = true;
            }
            if (Clipboard.GetText().ToString() == "")
            {
                粘贴ToolStripMenuItem.Enabled = false;
            }
            else
            {
                粘贴ToolStripMenuItem.Enabled = true;
            }
        }
        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (noteText.CanUndo == true)
            {
                noteText.Undo();
                noteText.ClearUndo();
            }
        }
        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noteText.Cut();
        }
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noteText.Copy();
        }
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noteText.Paste();
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noteText.SelectedText = String.Empty;
        }
        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //findForm.MdiParent = this;
            find findForm = new find();
            findForm.findText += new findTextHandler(findForm_findText);
            findForm.Show();
        }
        private void 查找下一个ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (noteText.SelectionLength == 0)
            {
                find findForm = new find();
                findForm.findText += new findTextHandler(findForm_findText);
                findForm.Show();
            }
            else
            {
                findForm_findText(this, noteText.Text.Substring(noteText.SelectionStart, noteText.SelectionLength));
            }
        }
        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            replace replaceForm = new replace();
            replaceForm.findText+= new findTextHandler(findForm_findText);
            replaceForm.replaceText+=new replaceTextHandler(replaceForm_replaceText);
            replaceForm.replaceAllText += new replaceAllTextHandler(replaceForm_replaceAllText);
            replaceForm.Show();
        }
        private void 转到ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn turnForm = new turn();
            turnForm.turnText += new trunTextHandler(turnForm_turnText);
            turnForm.ShowDialog();
        }
        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noteText.SelectAll();
        }
        private void 时间日期ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noteText.SelectedText = Convert.ToString(DateTime.Now);
        }
        #endregion
        #region 格式菜单
        private void 自动换行toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (自动换行toolStripMenuItem.Checked == false)
            {
                自动换行toolStripMenuItem.Checked = true;
                noteText.WordWrap = true;
            }
            else
            {
                自动换行toolStripMenuItem.Checked = false;
                noteText.WordWrap = false;
            }
        }
        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                noteText.Font = fontDialog.Font;
            }
        }
        #endregion
        #region 查看菜单
        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (状态栏ToolStripMenuItem.Checked == true)
            {
                状态栏ToolStripMenuItem.Checked = false;
                statusStrip.Visible = false;
            }
            else
            {
                状态栏ToolStripMenuItem.Checked = true;
                statusStrip.Visible = true;
            }
        }
        #endregion
        #region 帮助菜单
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about aboutForm = new about();
            aboutForm.ShowDialog();
        }
        #endregion
        #region 右键菜单
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (noteText.Modified == false)
                撤销UToolStripMenuItem.Enabled = false;
            else
                撤销UToolStripMenuItem.Enabled = true;
            if (noteText.SelectedText == "")
            {
                剪切TToolStripMenuItem.Enabled = false;
                复制CToolStripMenuItem.Enabled = false;
                删除DToolStripMenuItem.Enabled = false;
            }
            else
            {
                剪切TToolStripMenuItem.Enabled = true;
                复制CToolStripMenuItem.Enabled = true;
                删除DToolStripMenuItem.Enabled = true;
            }
            if (Clipboard.GetText().ToString() == "")
            {
                粘贴PToolStripMenuItem.Enabled = false;
            }
            else
            {
                粘贴PToolStripMenuItem.Enabled = true;
            }
        }
        private void 撤销UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            撤销ToolStripMenuItem_Click(sender, e);
        }
        private void 剪切TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            剪切ToolStripMenuItem_Click(sender, e);
        }
        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            复制ToolStripMenuItem_Click(sender, e);
        }
        private void 粘贴PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            粘贴ToolStripMenuItem_Click(sender, e);
        }
        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            删除ToolStripMenuItem_Click(sender, e);
        }
        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            全选ToolStripMenuItem_Click(sender, e);
        }
        #endregion
        #region 核心事件处理
        private void noteText_SelectionChanged(object sender, EventArgs e)
        {
            statusShow();
        }
        private void turnForm_turnText(object sender, String e)
        {
            int row = Convert.ToInt32(e)-1;
            noteText.SelectionStart = noteText.GetFirstCharIndexFromLine(row);
            noteText.SelectionLength = 0;
            noteText.Focus();
        }
        private void findForm_findText(object sender, String e,bool UpOrDown=true,bool sensitive=true)
        {
            
            if (UpOrDown == true)
            {
                int start = noteText.SelectionStart + noteText.SelectionLength;
                if (sensitive == true)
                {
                    int pos = noteText.Text.IndexOf(e, start);
                    if (pos != -1)
                    {
                        noteText.Focus();
                        noteText.Select(pos, e.Length);
                    }
                    else MessageBox.Show(String.Format("找不到{0}", e));
                }
                else
                {
                    int pos = compare.IndexOf(noteText.Text, e, start, CompareOptions.IgnoreCase);
                    if (pos != -1)
                    {
                        noteText.Focus();
                        noteText.Select(pos, e.Length);
                    }
                    else MessageBox.Show(String.Format("找不到{0}", e));
                }
            }
            else
            {
                //int start = Math.Min(noteText.Text.Length-1,noteText.SelectionStart - noteText.SelectionLength+1);
                int start = Math.Max(noteText.SelectionStart - 1, 0);
                if (sensitive == true)
                {
                    int pos = noteText.Text.LastIndexOf(e, start);
                    if (pos != -1)
                    {
                        noteText.Focus();
                        noteText.Select(pos, e.Length);
                    }
                    else MessageBox.Show(String.Format("找不到{0}", e));
                }
                else
                {
                    int pos = compare.LastIndexOf(noteText.Text, e, start, CompareOptions.IgnoreCase);
                    if (pos != -1)
                    {
                        noteText.Focus();
                        noteText.Select(pos, e.Length);
                    }
                    else MessageBox.Show(String.Format("找不到{0}", e));
                }
            }
        }
        private void replaceForm_replaceText(object sender, String find,String e, bool UpOrDown = true, bool sensitive = true)
        {
            int start = noteText.SelectionStart + noteText.SelectionLength;
            if (sensitive == true)
            {
                int pos = noteText.Text.IndexOf(find, start);
                if (pos != -1)
                {
                    noteText.Focus();
                    noteText.Select(pos, find.Length);
                    noteText.SelectedText = e;
                    noteText.Select(pos, e.Length);
                }
                else MessageBox.Show(String.Format("找不到{0}", find));
            }
            else
            {
                int pos = compare.IndexOf(noteText.Text, find, start, CompareOptions.IgnoreCase);
                if (pos != -1)
                {
                    noteText.Focus();
                    noteText.Select(pos, find.Length);
                    noteText.SelectedText = e;
                    noteText.Select(pos, e.Length);
                }
                else MessageBox.Show(String.Format("找不到{0}", find));
            }
        }
        private void replaceForm_replaceAllText(object sender, String find, String e, bool UpOrDown = true, bool sensitive = true)
        {
            int start = 0;
            if (sensitive == true)
            {
                int pos = noteText.Text.IndexOf(find, start);
                while (pos != -1)
                {
                    noteText.Select(pos, find.Length);
                    noteText.SelectedText = e;
                    pos = noteText.Text.IndexOf(find, pos + e.Length);
                }
            }
            else
            {
                int pos = compare.IndexOf(noteText.Text, find, start, CompareOptions.IgnoreCase);
                while (pos != -1)
                {
                    noteText.Select(pos, find.Length);
                    noteText.SelectedText = e;
                    pos = compare.IndexOf(noteText.Text, find, pos+e.Length, CompareOptions.IgnoreCase);
                }
            }
        }
        private void notePad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (noteText.Modified == true)
            {
                var choice = MessageBox.Show("是否保存当前文件到" + fileName + "?", "记事本", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (choice == DialogResult.Yes)
                {
                    if (fileName == "无标题")
                        SaveFile();
                    else
                    {
                        StreamWriter sw = new StreamWriter(filePath, false);
                        sw.Write(noteText.Text);
                        sw.Close();
                    }
                }
                else if (choice == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
        #endregion
    }
}
