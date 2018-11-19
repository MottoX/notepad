using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace notepad
{
    public partial class replace : Form
    {
        public replace()
        {
            InitializeComponent();
        }
        public event findTextHandler findText;
        public event replaceTextHandler replaceText;
        public event replaceAllTextHandler replaceAllText;

        private void findBtn_Click(object sender, EventArgs e)
        {
            bool sensitive = checkBox.Checked;
            findText(this, findContent.Text, true, sensitive);
        }

        private void replaceBtn_Click(object sender, EventArgs e)
        {
            bool sensitive = checkBox.Checked;
            replaceText(this, findContent.Text, replaceContent.Text, true, sensitive);
        }

        private void replaceAllBtn_Click(object sender, EventArgs e)
        {
            bool sensitive = checkBox.Checked;
            replaceAllText(this, findContent.Text, replaceContent.Text, true, sensitive);
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
