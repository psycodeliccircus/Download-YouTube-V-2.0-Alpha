using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace YouTube
{
    public partial class FormYouTube : Form
    {
        private string webText = string.Empty;
        private string videoURL = string.Empty;
        private string videoID = string.Empty;
        private const string saveto = @"{0}\{1}{2}";
        string filePath = string.Empty;

        private IEnumerable<VideoInfo> listObj = null;

        private VideoInfo video = null;

        private WebClient webClient = null;
        private Stopwatch watch = new Stopwatch();

        public FormYouTube()
        {
            InitializeComponent();
        }

        private void FormYouTube_Load(object sender, EventArgs e)
        {
            try
            {
                Taskbar.SetState(this.Handle, Taskbar.TaskbarStates.Indeterminate);

                SetControls(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error ...",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void textBoxURL_TextChanged(object sender, EventArgs e)
        {
            if (labelTitle.Text.Trim() != "Title :")
            {
                comboBoxQuality.DataSource = null;
                comboBoxQuality.Refresh();

                pictureBoxPreview.Image = null;
                pictureBoxPreview.Refresh();

                labelTitle.Text = "Title :";
                labelTitle.Refresh();

                labelLengthValue.ResetText();
                labelLengthValue.Refresh();

                progressBarStatus.Value = 0;

                buttonDownload.Enabled = false;
                buttonExit.Enabled = false;

                webText = string.Empty;
                videoURL = string.Empty;
                videoID = string.Empty;
                filePath = string.Empty;

                listObj = null;
                video = null;
                webClient = null;

                watch.Reset();

                SetControls(false, false);
            }
        }

        private void textBoxURL_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxURL.Text.Trim().Length == 0) return;

                this.Cursor = Cursors.WaitCursor;

                ShowProgress(0, Taskbar.TaskbarStates.Indeterminate);

                try
                {
                    Application.DoEvents();

                    webText = textBoxURL.Text.Trim();

                    if (YouTube.Tools.VideoLink(webText) && YouTube.Tools.VideoID(webText, out videoID))
                    {
                        Application.DoEvents();

                        DowloadImage(videoID);

                        DownloadUrlResolver info = new DownloadUrlResolver();
                        info.PopulateVideoInfo(webText, out listObj);

                        labelTitle.Text = string.Format("Título : {0}", info.Title);
                        labelLengthValue.Text = info.VideoLength;

                        List<VideoInfo> lst = (from v in listObj
                                               .Distinct()
                                               select v).Where(v => v.Resolution > 0).ToList();

                        comboBoxQuality.DataSource = null;

                        comboBoxQuality.ValueMember = "FormatCode";
                        comboBoxQuality.DisplayMember = lst.ToString();
                        comboBoxQuality.DataSource = lst;

                        if (lst.Count > 0)
                            comboBoxQuality.SelectedIndex = 0;

                        SetControls(true, false);
                    }
                    else
                    {
                        MessageBox.Show("Formato URL YouTube inválido.", this.Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error ...",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

                ShowProgress(0, Taskbar.TaskbarStates.NoProgress);

                this.Cursor = Cursors.Default;
            }
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                video = (from lst in listObj
                         select lst).Where<VideoInfo>(lst => lst.FormatCode == (int)comboBoxQuality.SelectedValue).FirstOrDefault();

                Download(video);
            }
            catch (Exception ex)
            {
                SetControls(true, false);
                ShowProgress(progressBarStatus.Value, Taskbar.TaskbarStates.Error);
                MessageBox.Show(ex.Message, "Error ...",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.Default;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                webClient.CancelAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error ...",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            
            this.Cursor = Cursors.Default;
        }

        private void DowloadImage(string value)
        {
            pictureBoxPreview.WaitOnLoad = false;
            pictureBoxPreview.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxPreview.LoadAsync(string.Format(ConfigurationManager.AppSettings["ImagePreview"], value));
        }

        private void ShowProgress(int value,Taskbar.TaskbarStates status)
        {
            if (status == Taskbar.TaskbarStates.NoProgress)
            {
                Taskbar.SetState(this.Handle, Taskbar.TaskbarStates.NoProgress);
                return;
            }

            Taskbar.SetValue(this.Handle, value, 100);
            Taskbar.SetState(this.Handle, status);
        }

        private void Download(VideoInfo info)
        {
            filePath = string.Format(saveto, Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
                                     Regex.Replace(info.Title, "[^0-9a-zA-Z]+", "_").Trim(),
                                     info.VideoExtension);

            DownloadVideoFile(info.DownloadUrl, filePath);
        }

        private void DownloadVideoFile(string urlAddress, string location)
        {
            using (webClient = new WebClient())
            {
                webClient.Proxy = null;
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Download_Progress);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Download_Completed);
                
                Uri url = new Uri(urlAddress);
                watch.Start();
                webClient.DownloadFileAsync(url, location);
            }
        }

        private void SetControls(bool value, bool downloading)
        {
            textBoxURL.ReadOnly = downloading;
            buttonExit.Enabled = downloading;

            comboBoxQuality.Enabled = value;

            buttonDownload.Enabled = value;
        }

        private void Download_Completed(object sender, AsyncCompletedEventArgs e)
        {
            watch.Reset();

            SetControls(true, false);

            labelStatus.Text = "Status... : Pronto";
            labelStatus.Refresh();

            ShowProgress(0, Taskbar.TaskbarStates.NoProgress);

            progressBarStatus.Value = 0;
            progressBarStatus.Refresh();

            if (e.Cancelled)
            {
                MessageBox.Show("Download foi cancelado.",
                    this.Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Arquivo baixados com sucesso.\n\n" + Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), 
                    this.Text, MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }

            if (!File.Exists(filePath))
                return;

            string argument = @"/select, " + filePath;
            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        private void Download_Progress(object sender, DownloadProgressChangedEventArgs e)
        {
            SetControls(false, true);

            labelStatus.Text = string.Format("Status... : {0}% - [{1}]",
                (int)e.ProgressPercentage, string.Format("{0} KB/s",
                (e.BytesReceived / 1024d / watch.Elapsed.TotalSeconds).ToString("00.00")));
            labelStatus.Refresh();

            ShowProgress((int)e.ProgressPercentage, Taskbar.TaskbarStates.Normal);

            progressBarStatus.Value = (int)e.ProgressPercentage;
            progressBarStatus.Refresh();
        }
    }
}