namespace WinFormCefSharpDemo
{
    partial class WebViewLoginForm
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
            this.MainWebView = new Microsoft.Toolkit.Forms.UI.Controls.WebView();
            ((System.ComponentModel.ISupportInitialize)(this.MainWebView)).BeginInit();
            this.SuspendLayout();
            // 
            // MainWebView
            // 
            this.MainWebView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainWebView.Location = new System.Drawing.Point(0, 0);
            this.MainWebView.MinimumSize = new System.Drawing.Size(20, 20);
            this.MainWebView.Name = "MainWebView";
            this.MainWebView.Size = new System.Drawing.Size(800, 450);
            this.MainWebView.TabIndex = 0;
            // 
            // WebViewLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainWebView);
            this.Name = "WebViewLoginForm";
            this.Text = "WebViewLoginForm";
            this.Shown += new System.EventHandler(this.WebViewLoginForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.MainWebView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Toolkit.Forms.UI.Controls.WebView MainWebView;
    }
}