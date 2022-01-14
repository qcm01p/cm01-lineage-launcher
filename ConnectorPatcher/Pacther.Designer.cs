
namespace ConnectorPatcher
{
    partial class Form_Pacther
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
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.textBox_ServerPort = new System.Windows.Forms.TextBox();
            this.label_IP = new System.Windows.Forms.Label();
            this.label_ServerPort = new System.Windows.Forms.Label();
            this.label_PaymentServerPort = new System.Windows.Forms.Label();
            this.textBox_PaymentServerPort = new System.Windows.Forms.TextBox();
            this.label_LeftUnderMessage = new System.Windows.Forms.Label();
            this.textBox_LeftUnderMessage = new System.Windows.Forms.TextBox();
            this.button_Edit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(75, 12);
            this.textBox_IP.MaxLength = 46;
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(239, 21);
            this.textBox_IP.TabIndex = 0;
            this.textBox_IP.Text = "127.0.0.1";
            // 
            // textBox_ServerPort
            // 
            this.textBox_ServerPort.Location = new System.Drawing.Point(75, 39);
            this.textBox_ServerPort.MaxLength = 5;
            this.textBox_ServerPort.Name = "textBox_ServerPort";
            this.textBox_ServerPort.Size = new System.Drawing.Size(239, 21);
            this.textBox_ServerPort.TabIndex = 1;
            this.textBox_ServerPort.Text = "2000";
            // 
            // label_IP
            // 
            this.label_IP.AutoSize = true;
            this.label_IP.Location = new System.Drawing.Point(12, 15);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(57, 12);
            this.label_IP.TabIndex = 2;
            this.label_IP.Text = "서버주소:";
            // 
            // label_ServerPort
            // 
            this.label_ServerPort.AutoSize = true;
            this.label_ServerPort.Location = new System.Drawing.Point(12, 42);
            this.label_ServerPort.Name = "label_ServerPort";
            this.label_ServerPort.Size = new System.Drawing.Size(57, 12);
            this.label_ServerPort.TabIndex = 3;
            this.label_ServerPort.Text = "서버포트:";
            // 
            // label_PaymentServerPort
            // 
            this.label_PaymentServerPort.AutoSize = true;
            this.label_PaymentServerPort.Location = new System.Drawing.Point(12, 69);
            this.label_PaymentServerPort.Name = "label_PaymentServerPort";
            this.label_PaymentServerPort.Size = new System.Drawing.Size(57, 12);
            this.label_PaymentServerPort.TabIndex = 5;
            this.label_PaymentServerPort.Text = "결제포트:";
            // 
            // textBox_PaymentServerPort
            // 
            this.textBox_PaymentServerPort.Location = new System.Drawing.Point(75, 66);
            this.textBox_PaymentServerPort.MaxLength = 5;
            this.textBox_PaymentServerPort.Name = "textBox_PaymentServerPort";
            this.textBox_PaymentServerPort.Size = new System.Drawing.Size(239, 21);
            this.textBox_PaymentServerPort.TabIndex = 4;
            this.textBox_PaymentServerPort.Text = "2002";
            // 
            // label_LeftUnderMessage
            // 
            this.label_LeftUnderMessage.AutoSize = true;
            this.label_LeftUnderMessage.Location = new System.Drawing.Point(12, 101);
            this.label_LeftUnderMessage.Name = "label_LeftUnderMessage";
            this.label_LeftUnderMessage.Size = new System.Drawing.Size(97, 12);
            this.label_LeftUnderMessage.TabIndex = 7;
            this.label_LeftUnderMessage.Text = "좌측하단 메시지:";
            // 
            // textBox_LeftUnderMessage
            // 
            this.textBox_LeftUnderMessage.Location = new System.Drawing.Point(115, 98);
            this.textBox_LeftUnderMessage.MaxLength = 18;
            this.textBox_LeftUnderMessage.Name = "textBox_LeftUnderMessage";
            this.textBox_LeftUnderMessage.Size = new System.Drawing.Size(199, 21);
            this.textBox_LeftUnderMessage.TabIndex = 6;
            this.textBox_LeftUnderMessage.Text = "XX서버에 오신것을 환영합니다.";
            // 
            // button_Edit
            // 
            this.button_Edit.Location = new System.Drawing.Point(14, 133);
            this.button_Edit.Name = "button_Edit";
            this.button_Edit.Size = new System.Drawing.Size(300, 36);
            this.button_Edit.TabIndex = 8;
            this.button_Edit.Text = "접속기 만들기";
            this.button_Edit.UseVisualStyleBackColor = true;
            this.button_Edit.Click += new System.EventHandler(this.button_Edit_Click);
            // 
            // Form_Pacther
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 181);
            this.Controls.Add(this.button_Edit);
            this.Controls.Add(this.label_LeftUnderMessage);
            this.Controls.Add(this.textBox_LeftUnderMessage);
            this.Controls.Add(this.label_PaymentServerPort);
            this.Controls.Add(this.textBox_PaymentServerPort);
            this.Controls.Add(this.label_ServerPort);
            this.Controls.Add(this.label_IP);
            this.Controls.Add(this.textBox_ServerPort);
            this.Controls.Add(this.textBox_IP);
            this.Name = "Form_Pacther";
            this.Text = "Pacther";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.TextBox textBox_ServerPort;
        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.Label label_ServerPort;
        private System.Windows.Forms.Label label_PaymentServerPort;
        private System.Windows.Forms.TextBox textBox_PaymentServerPort;
        private System.Windows.Forms.Label label_LeftUnderMessage;
        private System.Windows.Forms.TextBox textBox_LeftUnderMessage;
        private System.Windows.Forms.Button button_Edit;
    }
}

