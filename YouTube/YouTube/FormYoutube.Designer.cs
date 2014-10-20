namespace YouTube
{
    partial class FormYouTube
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormYouTube));
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.labelBorder = new System.Windows.Forms.Label();
            this.labelURL = new System.Windows.Forms.Label();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.labelQuality = new System.Windows.Forms.Label();
            this.comboBoxQuality = new System.Windows.Forms.ComboBox();
            this.labelLine = new System.Windows.Forms.Label();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelEdge = new System.Windows.Forms.Label();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.progressBarStatus = new System.Windows.Forms.ProgressBar();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelLength = new System.Windows.Forms.Label();
            this.labelLengthValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.BackgroundImage")));
            this.pictureBoxLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxLogo.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // labelBorder
            // 
            this.labelBorder.BackColor = System.Drawing.Color.Red;
            this.labelBorder.Location = new System.Drawing.Point(0, 0);
            this.labelBorder.Name = "labelBorder";
            this.labelBorder.Size = new System.Drawing.Size(605, 5);
            this.labelBorder.TabIndex = 0;
            // 
            // labelURL
            // 
            this.labelURL.AutoSize = true;
            this.labelURL.Font = new System.Drawing.Font("Eras Bold ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelURL.Location = new System.Drawing.Point(118, 12);
            this.labelURL.Name = "labelURL";
            this.labelURL.Size = new System.Drawing.Size(176, 19);
            this.labelURL.TabIndex = 1;
            this.labelURL.Text = "YouTube Video URL :";
            // 
            // textBoxURL
            // 
            this.textBoxURL.BackColor = System.Drawing.Color.White;
            this.textBoxURL.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxURL.ForeColor = System.Drawing.Color.Black;
            this.textBoxURL.Location = new System.Drawing.Point(122, 34);
            this.textBoxURL.MaxLength = 5000;
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(458, 23);
            this.textBoxURL.TabIndex = 2;
            this.textBoxURL.TextChanged += new System.EventHandler(this.textBoxURL_TextChanged);
            this.textBoxURL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxURL_KeyUp);
            // 
            // labelQuality
            // 
            this.labelQuality.AutoSize = true;
            this.labelQuality.Location = new System.Drawing.Point(119, 71);
            this.labelQuality.Name = "labelQuality";
            this.labelQuality.Size = new System.Drawing.Size(70, 15);
            this.labelQuality.TabIndex = 3;
            this.labelQuality.Text = "Quality :";
            // 
            // comboBoxQuality
            // 
            this.comboBoxQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxQuality.FormattingEnabled = true;
            this.comboBoxQuality.Location = new System.Drawing.Point(122, 89);
            this.comboBoxQuality.Name = "comboBoxQuality";
            this.comboBoxQuality.Size = new System.Drawing.Size(458, 23);
            this.comboBoxQuality.TabIndex = 4;
            // 
            // labelLine
            // 
            this.labelLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelLine.Location = new System.Drawing.Point(0, 220);
            this.labelLine.Name = "labelLine";
            this.labelLine.Size = new System.Drawing.Size(606, 3);
            this.labelLine.TabIndex = 11;
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxPreview.Location = new System.Drawing.Point(122, 118);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(120, 90);
            this.pictureBoxPreview.TabIndex = 7;
            this.pictureBoxPreview.TabStop = false;
            // 
            // labelTitle
            // 
            this.labelTitle.Location = new System.Drawing.Point(248, 118);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(332, 46);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "Title :";
            // 
            // labelEdge
            // 
            this.labelEdge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelEdge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelEdge.Location = new System.Drawing.Point(1, 223);
            this.labelEdge.Name = "labelEdge";
            this.labelEdge.Size = new System.Drawing.Size(587, 75);
            this.labelEdge.TabIndex = 12;
            // 
            // buttonDownload
            // 
            this.buttonDownload.Location = new System.Drawing.Point(370, 236);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(102, 47);
            this.buttonDownload.TabIndex = 13;
            this.buttonDownload.Text = "&Download";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(478, 236);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(102, 47);
            this.buttonExit.TabIndex = 14;
            this.buttonExit.Text = "&Cancel";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // progressBarStatus
            // 
            this.progressBarStatus.Location = new System.Drawing.Point(12, 252);
            this.progressBarStatus.Name = "progressBarStatus";
            this.progressBarStatus.Size = new System.Drawing.Size(230, 15);
            this.progressBarStatus.TabIndex = 16;
            // 
            // labelAuthor
            // 
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelAuthor.ForeColor = System.Drawing.Color.Gray;
            this.labelAuthor.Location = new System.Drawing.Point(9, 280);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(203, 15);
            this.labelAuthor.TabIndex = 17;
            this.labelAuthor.Text = "Developed by Gehan Fernando.";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelStatus.Location = new System.Drawing.Point(12, 236);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(126, 15);
            this.labelStatus.TabIndex = 15;
            this.labelStatus.Text = "Status... : Ready";
            // 
            // labelLength
            // 
            this.labelLength.AutoSize = true;
            this.labelLength.Location = new System.Drawing.Point(248, 170);
            this.labelLength.Name = "labelLength";
            this.labelLength.Size = new System.Drawing.Size(49, 15);
            this.labelLength.TabIndex = 6;
            this.labelLength.Text = "Length";
            // 
            // labelLengthValue
            // 
            this.labelLengthValue.AutoSize = true;
            this.labelLengthValue.Location = new System.Drawing.Point(303, 170);
            this.labelLengthValue.Name = "labelLengthValue";
            this.labelLengthValue.Size = new System.Drawing.Size(0, 15);
            this.labelLengthValue.TabIndex = 7;
            // 
            // FormYouTube
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(589, 298);
            this.Controls.Add(this.labelLengthValue);
            this.Controls.Add(this.labelLength);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.progressBarStatus);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.labelEdge);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.labelLine);
            this.Controls.Add(this.comboBoxQuality);
            this.Controls.Add(this.labelQuality);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.labelURL);
            this.Controls.Add(this.labelBorder);
            this.Controls.Add(this.pictureBoxLogo);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormYouTube";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YouTube Downloader";
            this.Load += new System.EventHandler(this.FormYouTube_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label labelBorder;
        private System.Windows.Forms.Label labelURL;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.Label labelQuality;
        private System.Windows.Forms.ComboBox comboBoxQuality;
        private System.Windows.Forms.Label labelLine;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelEdge;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.ProgressBar progressBarStatus;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.Label labelLengthValue;
    }
}

