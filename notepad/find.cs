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
    public partial class find : Form
    {
        public find()
        {
            InitializeComponent();
        }
        public event findTextHandler findText;

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            bool UpOrDown = radioButton2.Checked;//向下为true
            bool sensitive = checkBox.Checked;//选中为true;
            findText(this, findContent.Text,UpOrDown,sensitive); 
        }
    }
}
