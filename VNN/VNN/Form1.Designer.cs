namespace VNN
{
    partial class Form1
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
            this.btnStartWebsite = new System.Windows.Forms.Button();
            this.btnStopWebsite = new System.Windows.Forms.Button();
            this.btnOpenHtml = new System.Windows.Forms.Button();
            this.console1 = new System.Windows.Forms.TextBox();
            this.btnSendMsgUsingWebsocket = new System.Windows.Forms.Button();
            this.tbMessageToWebsocket = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnStartWebsite
            // 
            this.btnStartWebsite.Location = new System.Drawing.Point(12, 12);
            this.btnStartWebsite.Name = "btnStartWebsite";
            this.btnStartWebsite.Size = new System.Drawing.Size(239, 23);
            this.btnStartWebsite.TabIndex = 0;
            this.btnStartWebsite.Text = "Start Website";
            this.btnStartWebsite.UseVisualStyleBackColor = true;
            this.btnStartWebsite.Click += new System.EventHandler(this.BtnStartWebsite_Click);
            // 
            // btnStopWebsite
            // 
            this.btnStopWebsite.Location = new System.Drawing.Point(12, 41);
            this.btnStopWebsite.Name = "btnStopWebsite";
            this.btnStopWebsite.Size = new System.Drawing.Size(239, 23);
            this.btnStopWebsite.TabIndex = 1;
            this.btnStopWebsite.Text = "Stop Website";
            this.btnStopWebsite.UseVisualStyleBackColor = true;
            this.btnStopWebsite.Click += new System.EventHandler(this.BtnStopWebsite_Click);
            // 
            // btnOpenHtml
            // 
            this.btnOpenHtml.Location = new System.Drawing.Point(12, 70);
            this.btnOpenHtml.Name = "btnOpenHtml";
            this.btnOpenHtml.Size = new System.Drawing.Size(239, 23);
            this.btnOpenHtml.TabIndex = 2;
            this.btnOpenHtml.Text = "Open web page";
            this.btnOpenHtml.UseVisualStyleBackColor = true;
            this.btnOpenHtml.Click += new System.EventHandler(this.BtnOpenHtml_Click);
            // 
            // console1
            // 
            this.console1.Location = new System.Drawing.Point(329, 38);
            this.console1.Multiline = true;
            this.console1.Name = "console1";
            this.console1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.console1.Size = new System.Drawing.Size(319, 539);
            this.console1.TabIndex = 3;
            // 
            // btnSendMsgUsingWebsocket
            // 
            this.btnSendMsgUsingWebsocket.Enabled = false;
            this.btnSendMsgUsingWebsocket.Location = new System.Drawing.Point(329, 10);
            this.btnSendMsgUsingWebsocket.Name = "btnSendMsgUsingWebsocket";
            this.btnSendMsgUsingWebsocket.Size = new System.Drawing.Size(28, 23);
            this.btnSendMsgUsingWebsocket.TabIndex = 4;
            this.btnSendMsgUsingWebsocket.Text = "<-";
            this.btnSendMsgUsingWebsocket.UseVisualStyleBackColor = true;
            this.btnSendMsgUsingWebsocket.Click += new System.EventHandler(this.BtnSendMsgUsingWebsocket_Click);
            // 
            // tbMessageToWebsocket
            // 
            this.tbMessageToWebsocket.Enabled = false;
            this.tbMessageToWebsocket.Location = new System.Drawing.Point(363, 12);
            this.tbMessageToWebsocket.Name = "tbMessageToWebsocket";
            this.tbMessageToWebsocket.Size = new System.Drawing.Size(285, 20);
            this.tbMessageToWebsocket.TabIndex = 5;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(40, 195);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(167, 23);
            this.btnSelectFile.TabIndex = 6;
            this.btnSelectFile.Text = "Select file with NN\'s data";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "JSON files|*.json|All files|*.*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 589);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.tbMessageToWebsocket);
            this.Controls.Add(this.btnSendMsgUsingWebsocket);
            this.Controls.Add(this.console1);
            this.Controls.Add(this.btnOpenHtml);
            this.Controls.Add(this.btnStopWebsite);
            this.Controls.Add(this.btnStartWebsite);
            this.Name = "Form1";
            this.Text = "Visual Neural Network";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartWebsite;
        private System.Windows.Forms.Button btnStopWebsite;
        private System.Windows.Forms.Button btnOpenHtml;
        private System.Windows.Forms.TextBox console1;
        private System.Windows.Forms.Button btnSendMsgUsingWebsocket;
        private System.Windows.Forms.TextBox tbMessageToWebsocket;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

