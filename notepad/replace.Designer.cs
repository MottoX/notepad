namespace notepad
{
    partial class replace
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
            this.contentLabel = new System.Windows.Forms.Label();
            this.findContent = new System.Windows.Forms.TextBox();
            this.findBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.replaceBtn = new System.Windows.Forms.Button();
            this.replaceAllBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.replaceContent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // contentLabel
            // 
            this.contentLabel.AutoSize = true;
            this.contentLabel.Location = new System.Drawing.Point(24, 21);
            this.contentLabel.Name = "contentLabel";
            this.contentLabel.Size = new System.Drawing.Size(71, 12);
            this.contentLabel.TabIndex = 0;
            this.contentLabel.Text = "查找内容(N)";
            // 
            // findContent
            // 
            this.findContent.Location = new System.Drawing.Point(124, 21);
            this.findContent.Name = "findContent";
            this.findContent.Size = new System.Drawing.Size(181, 21);
            this.findContent.TabIndex = 1;
            // 
            // findBtn
            // 
            this.findBtn.Location = new System.Drawing.Point(356, 19);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(75, 23);
            this.findBtn.TabIndex = 2;
            this.findBtn.Text = "查找";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(356, 106);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 24);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "取消";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Checked = true;
            this.checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox.Location = new System.Drawing.Point(26, 106);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(84, 16);
            this.checkBox.TabIndex = 5;
            this.checkBox.Text = "区分大小写";
            this.checkBox.UseVisualStyleBackColor = true;
            // 
            // replaceBtn
            // 
            this.replaceBtn.Location = new System.Drawing.Point(356, 48);
            this.replaceBtn.Name = "replaceBtn";
            this.replaceBtn.Size = new System.Drawing.Size(75, 23);
            this.replaceBtn.TabIndex = 6;
            this.replaceBtn.Text = "替换";
            this.replaceBtn.UseVisualStyleBackColor = true;
            this.replaceBtn.Click += new System.EventHandler(this.replaceBtn_Click);
            // 
            // replaceAllBtn
            // 
            this.replaceAllBtn.Location = new System.Drawing.Point(356, 77);
            this.replaceAllBtn.Name = "replaceAllBtn";
            this.replaceAllBtn.Size = new System.Drawing.Size(75, 23);
            this.replaceAllBtn.TabIndex = 7;
            this.replaceAllBtn.Text = "全部替换";
            this.replaceAllBtn.UseVisualStyleBackColor = true;
            this.replaceAllBtn.Click += new System.EventHandler(this.replaceAllBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "替换为(P)";
            // 
            // replaceContent
            // 
            this.replaceContent.Location = new System.Drawing.Point(124, 62);
            this.replaceContent.Name = "replaceContent";
            this.replaceContent.Size = new System.Drawing.Size(181, 21);
            this.replaceContent.TabIndex = 9;
            // 
            // replace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 145);
            this.Controls.Add(this.replaceContent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.replaceAllBtn);
            this.Controls.Add(this.replaceBtn);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.findBtn);
            this.Controls.Add(this.findContent);
            this.Controls.Add(this.contentLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "replace";
            this.ShowIcon = false;
            this.Text = "替换";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label contentLabel;
        private System.Windows.Forms.TextBox findContent;
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Button replaceBtn;
        private System.Windows.Forms.Button replaceAllBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox replaceContent;
    }
}