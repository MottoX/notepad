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
    public partial class turn : Form
    {
        public turn()
        {
            InitializeComponent();
        }

        public event trunTextHandler turnText;
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void yesBtn_Click(object sender, EventArgs e)
        {
            turnText(this,lineText.Text);
            Close();
        }
    }
}
