namespace WinFormCefSharpDemo
{
    partial class LaunchForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.webBrowserButton = new System.Windows.Forms.Button();
            this.cefSharpButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.button3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cefSharpButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.webBrowserButton, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(567, 149);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // webBrowserButton
            // 
            this.webBrowserButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserButton.Location = new System.Drawing.Point(3, 3);
            this.webBrowserButton.Name = "webBrowserButton";
            this.webBrowserButton.Size = new System.Drawing.Size(182, 143);
            this.webBrowserButton.TabIndex = 0;
            this.webBrowserButton.Text = "WebBrowser";
            this.webBrowserButton.UseVisualStyleBackColor = true;
            this.webBrowserButton.Click += new System.EventHandler(this.webBrowserButton_Click);
            // 
            // cefSharpButton
            // 
            this.cefSharpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cefSharpButton.Location = new System.Drawing.Point(191, 3);
            this.cefSharpButton.Name = "cefSharpButton";
            this.cefSharpButton.Size = new System.Drawing.Size(182, 143);
            this.cefSharpButton.TabIndex = 1;
            this.cefSharpButton.Text = "CefSharp";
            this.cefSharpButton.UseVisualStyleBackColor = true;
            this.cefSharpButton.Click += new System.EventHandler(this.cefSharpButton_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Location = new System.Drawing.Point(379, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(185, 143);
            this.button3.TabIndex = 2;
            this.button3.Text = "-";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // LaunchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 149);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LaunchForm";
            this.Text = "LaunchForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button webBrowserButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button cefSharpButton;
    }
}

