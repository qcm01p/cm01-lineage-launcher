
namespace LineageConnector
{
    partial class LineageConnector
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_GameStart = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Payment = new System.Windows.Forms.Button();
            this.button_MoveToHomePage = new System.Windows.Forms.Button();
            this.label_해상도값 = new System.Windows.Forms.Label();
            this.label_Downloaded = new System.Windows.Forms.Label();
            this.checkBox_WindowMode = new System.Windows.Forms.CheckBox();
            this.comboBox_해상도 = new System.Windows.Forms.ComboBox();
            this.comboBox_DownloadLink = new System.Windows.Forms.ComboBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.progressBar_download = new System.Windows.Forms.ProgressBar();
            this.label_Status = new System.Windows.Forms.Label();
            this.label_cm01 = new System.Windows.Forms.Label();
            this.label_ServerName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_GameStart)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_GameStart
            // 
            this.pictureBox_GameStart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_GameStart.Image = global::LineageConnector.Properties.Resources.GameStart;
            this.pictureBox_GameStart.Location = new System.Drawing.Point(538, 390);
            this.pictureBox_GameStart.Name = "pictureBox_GameStart";
            this.pictureBox_GameStart.Size = new System.Drawing.Size(250, 61);
            this.pictureBox_GameStart.TabIndex = 0;
            this.pictureBox_GameStart.TabStop = false;
            this.pictureBox_GameStart.Click += new System.EventHandler(this.pictureBox_GameStart_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Payment);
            this.panel1.Controls.Add(this.button_MoveToHomePage);
            this.panel1.Controls.Add(this.label_해상도값);
            this.panel1.Controls.Add(this.label_Downloaded);
            this.panel1.Controls.Add(this.checkBox_WindowMode);
            this.panel1.Controls.Add(this.comboBox_해상도);
            this.panel1.Controls.Add(this.comboBox_DownloadLink);
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Controls.Add(this.progressBar_download);
            this.panel1.Controls.Add(this.label_Status);
            this.panel1.Controls.Add(this.pictureBox_GameStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 461);
            this.panel1.TabIndex = 1;
            // 
            // button_Payment
            // 
            this.button_Payment.Location = new System.Drawing.Point(538, 51);
            this.button_Payment.Name = "button_Payment";
            this.button_Payment.Size = new System.Drawing.Size(250, 33);
            this.button_Payment.TabIndex = 12;
            this.button_Payment.Text = "결제";
            this.button_Payment.UseVisualStyleBackColor = true;
            this.button_Payment.Click += new System.EventHandler(this.button_Payment_Click);
            // 
            // button_MoveToHomePage
            // 
            this.button_MoveToHomePage.Location = new System.Drawing.Point(538, 12);
            this.button_MoveToHomePage.Name = "button_MoveToHomePage";
            this.button_MoveToHomePage.Size = new System.Drawing.Size(250, 33);
            this.button_MoveToHomePage.TabIndex = 11;
            this.button_MoveToHomePage.Text = "홈페이지 이동";
            this.button_MoveToHomePage.UseVisualStyleBackColor = true;
            this.button_MoveToHomePage.Click += new System.EventHandler(this.button_MoveToHomePage_Click);
            // 
            // label_해상도값
            // 
            this.label_해상도값.AutoSize = true;
            this.label_해상도값.Location = new System.Drawing.Point(644, 363);
            this.label_해상도값.Name = "label_해상도값";
            this.label_해상도값.Size = new System.Drawing.Size(45, 12);
            this.label_해상도값.TabIndex = 10;
            this.label_해상도값.Text = "해상도:";
            // 
            // label_Downloaded
            // 
            this.label_Downloaded.AutoSize = true;
            this.label_Downloaded.Location = new System.Drawing.Point(185, 368);
            this.label_Downloaded.Name = "label_Downloaded";
            this.label_Downloaded.Size = new System.Drawing.Size(11, 12);
            this.label_Downloaded.TabIndex = 9;
            this.label_Downloaded.Text = "/";
            // 
            // checkBox_WindowMode
            // 
            this.checkBox_WindowMode.AutoSize = true;
            this.checkBox_WindowMode.Checked = true;
            this.checkBox_WindowMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_WindowMode.Location = new System.Drawing.Point(538, 362);
            this.checkBox_WindowMode.Name = "checkBox_WindowMode";
            this.checkBox_WindowMode.Size = new System.Drawing.Size(88, 16);
            this.checkBox_WindowMode.TabIndex = 8;
            this.checkBox_WindowMode.Text = "창모드 실행";
            this.checkBox_WindowMode.UseVisualStyleBackColor = true;
            this.checkBox_WindowMode.CheckedChanged += new System.EventHandler(this.checkBox_WindowMode_CheckedChanged);
            // 
            // comboBox_해상도
            // 
            this.comboBox_해상도.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_해상도.FormattingEnabled = true;
            this.comboBox_해상도.Items.AddRange(new object[] {
            "800*600",
            "1200*900",
            "1600*1200"});
            this.comboBox_해상도.Location = new System.Drawing.Point(695, 360);
            this.comboBox_해상도.Name = "comboBox_해상도";
            this.comboBox_해상도.Size = new System.Drawing.Size(93, 20);
            this.comboBox_해상도.TabIndex = 7;
            // 
            // comboBox_DownloadLink
            // 
            this.comboBox_DownloadLink.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DownloadLink.Enabled = false;
            this.comboBox_DownloadLink.FormattingEnabled = true;
            this.comboBox_DownloadLink.Items.AddRange(new object[] {
            "다운로드 필요없음"});
            this.comboBox_DownloadLink.Location = new System.Drawing.Point(12, 364);
            this.comboBox_DownloadLink.Name = "comboBox_DownloadLink";
            this.comboBox_DownloadLink.Size = new System.Drawing.Size(167, 20);
            this.comboBox_DownloadLink.TabIndex = 6;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, -15);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(509, 372);
            this.webBrowser1.TabIndex = 5;
            // 
            // progressBar_download
            // 
            this.progressBar_download.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_download.Location = new System.Drawing.Point(12, 390);
            this.progressBar_download.Name = "progressBar_download";
            this.progressBar_download.Size = new System.Drawing.Size(509, 27);
            this.progressBar_download.TabIndex = 4;
            // 
            // label_Status
            // 
            this.label_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Status.AutoSize = true;
            this.label_Status.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Status.Location = new System.Drawing.Point(8, 438);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(195, 13);
            this.label_Status.TabIndex = 3;
            this.label_Status.Text = "접속기를 시작하는 중입니다..";
            // 
            // label_cm01
            // 
            this.label_cm01.AutoSize = true;
            this.label_cm01.Font = new System.Drawing.Font("굴림", 8F);
            this.label_cm01.Location = new System.Drawing.Point(746, 465);
            this.label_cm01.Name = "label_cm01";
            this.label_cm01.Size = new System.Drawing.Size(54, 11);
            this.label_cm01.TabIndex = 2;
            this.label_cm01.Text = "by. cm01";
            // 
            // label_ServerName
            // 
            this.label_ServerName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ServerName.AutoSize = true;
            this.label_ServerName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_ServerName.Location = new System.Drawing.Point(8, 465);
            this.label_ServerName.Name = "label_ServerName";
            this.label_ServerName.Size = new System.Drawing.Size(165, 12);
            this.label_ServerName.TabIndex = 13;
            this.label_ServerName.Text = "　       　　　　　　　　　　";
            // 
            // LineageConnector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.label_ServerName);
            this.Controls.Add(this.label_cm01);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LineageConnector";
            this.Text = "Lineage Connector";
            this.TransparencyKey = System.Drawing.SystemColors.AppWorkspace;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LineageConnector_FormClosing);
            this.Load += new System.EventHandler(this.LineageConnector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_GameStart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_GameStart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_cm01;
        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.ProgressBar progressBar_download;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ComboBox comboBox_DownloadLink;
        private System.Windows.Forms.ComboBox comboBox_해상도;
        private System.Windows.Forms.CheckBox checkBox_WindowMode;
        private System.Windows.Forms.Label label_Downloaded;
        private System.Windows.Forms.Label label_해상도값;
        private System.Windows.Forms.Button button_MoveToHomePage;
        private System.Windows.Forms.Button button_Payment;
        private System.Windows.Forms.Label label_ServerName;
    }
}

