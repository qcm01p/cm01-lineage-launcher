
namespace LineageConnector
{
    partial class PaymentForm
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
            this.textBox_Account = new System.Windows.Forms.TextBox();
            this.label_Account = new System.Windows.Forms.Label();
            this.label_CharName = new System.Windows.Forms.Label();
            this.textBox_CharName = new System.Windows.Forms.TextBox();
            this.label_유저지갑주소 = new System.Windows.Forms.Label();
            this.textBox_billsender = new System.Windows.Forms.TextBox();
            this.groupBox_PaymentRequest = new System.Windows.Forms.GroupBox();
            this.label_결제금액 = new System.Windows.Forms.Label();
            this.textBox_결제금액 = new System.Windows.Forms.TextBox();
            this.label_일회용결제비밀번호 = new System.Windows.Forms.Label();
            this.textBox_PaymentPassword = new System.Windows.Forms.TextBox();
            this.button_RequestPayment = new System.Windows.Forms.Button();
            this.label_Status = new System.Windows.Forms.Label();
            this.textBox_Status = new System.Windows.Forms.TextBox();
            this.textBox_TxID = new System.Windows.Forms.TextBox();
            this.label_TxID = new System.Windows.Forms.Label();
            this.button_StartPayment = new System.Windows.Forms.Button();
            this.groupBox_StartPayment = new System.Windows.Forms.GroupBox();
            this.checkBox_Save = new System.Windows.Forms.CheckBox();
            this.groupBox_PaymentRequest.SuspendLayout();
            this.groupBox_StartPayment.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_Account
            // 
            this.textBox_Account.Location = new System.Drawing.Point(175, 16);
            this.textBox_Account.MaxLength = 30;
            this.textBox_Account.Name = "textBox_Account";
            this.textBox_Account.Size = new System.Drawing.Size(80, 21);
            this.textBox_Account.TabIndex = 0;
            // 
            // label_Account
            // 
            this.label_Account.AutoSize = true;
            this.label_Account.Location = new System.Drawing.Point(128, 19);
            this.label_Account.Name = "label_Account";
            this.label_Account.Size = new System.Drawing.Size(41, 12);
            this.label_Account.TabIndex = 1;
            this.label_Account.Text = "계정명";
            // 
            // label_CharName
            // 
            this.label_CharName.AutoSize = true;
            this.label_CharName.Location = new System.Drawing.Point(128, 46);
            this.label_CharName.Name = "label_CharName";
            this.label_CharName.Size = new System.Drawing.Size(41, 12);
            this.label_CharName.TabIndex = 3;
            this.label_CharName.Text = "캐릭명";
            // 
            // textBox_CharName
            // 
            this.textBox_CharName.Location = new System.Drawing.Point(175, 43);
            this.textBox_CharName.MaxLength = 30;
            this.textBox_CharName.Name = "textBox_CharName";
            this.textBox_CharName.Size = new System.Drawing.Size(80, 21);
            this.textBox_CharName.TabIndex = 2;
            // 
            // label_유저지갑주소
            // 
            this.label_유저지갑주소.AutoSize = true;
            this.label_유저지갑주소.Location = new System.Drawing.Point(6, 73);
            this.label_유저지갑주소.Name = "label_유저지갑주소";
            this.label_유저지갑주소.Size = new System.Drawing.Size(163, 12);
            this.label_유저지갑주소.TabIndex = 5;
            this.label_유저지갑주소.Text = "유저 지갑 주소 (보내는 사람)";
            // 
            // textBox_billsender
            // 
            this.textBox_billsender.Location = new System.Drawing.Point(175, 70);
            this.textBox_billsender.MaxLength = 512;
            this.textBox_billsender.Name = "textBox_billsender";
            this.textBox_billsender.Size = new System.Drawing.Size(174, 21);
            this.textBox_billsender.TabIndex = 4;
            // 
            // groupBox_PaymentRequest
            // 
            this.groupBox_PaymentRequest.Controls.Add(this.checkBox_Save);
            this.groupBox_PaymentRequest.Controls.Add(this.button_RequestPayment);
            this.groupBox_PaymentRequest.Controls.Add(this.label_일회용결제비밀번호);
            this.groupBox_PaymentRequest.Controls.Add(this.textBox_PaymentPassword);
            this.groupBox_PaymentRequest.Controls.Add(this.label_결제금액);
            this.groupBox_PaymentRequest.Controls.Add(this.textBox_결제금액);
            this.groupBox_PaymentRequest.Controls.Add(this.label_유저지갑주소);
            this.groupBox_PaymentRequest.Controls.Add(this.textBox_Account);
            this.groupBox_PaymentRequest.Controls.Add(this.textBox_billsender);
            this.groupBox_PaymentRequest.Controls.Add(this.label_Account);
            this.groupBox_PaymentRequest.Controls.Add(this.label_CharName);
            this.groupBox_PaymentRequest.Controls.Add(this.textBox_CharName);
            this.groupBox_PaymentRequest.Location = new System.Drawing.Point(12, 12);
            this.groupBox_PaymentRequest.Name = "groupBox_PaymentRequest";
            this.groupBox_PaymentRequest.Size = new System.Drawing.Size(358, 201);
            this.groupBox_PaymentRequest.TabIndex = 6;
            this.groupBox_PaymentRequest.TabStop = false;
            this.groupBox_PaymentRequest.Text = "결제 요청";
            // 
            // label_결제금액
            // 
            this.label_결제금액.AutoSize = true;
            this.label_결제금액.Location = new System.Drawing.Point(78, 100);
            this.label_결제금액.Name = "label_결제금액";
            this.label_결제금액.Size = new System.Drawing.Size(91, 12);
            this.label_결제금액.TabIndex = 7;
            this.label_결제금액.Text = "결제금액 (TRX)";
            // 
            // textBox_결제금액
            // 
            this.textBox_결제금액.Location = new System.Drawing.Point(175, 97);
            this.textBox_결제금액.MaxLength = 7;
            this.textBox_결제금액.Name = "textBox_결제금액";
            this.textBox_결제금액.Size = new System.Drawing.Size(80, 21);
            this.textBox_결제금액.TabIndex = 6;
            // 
            // label_일회용결제비밀번호
            // 
            this.label_일회용결제비밀번호.AutoSize = true;
            this.label_일회용결제비밀번호.Location = new System.Drawing.Point(48, 127);
            this.label_일회용결제비밀번호.Name = "label_일회용결제비밀번호";
            this.label_일회용결제비밀번호.Size = new System.Drawing.Size(121, 12);
            this.label_일회용결제비밀번호.TabIndex = 9;
            this.label_일회용결제비밀번호.Text = "일회용 결제 비밀번호";
            // 
            // textBox_PaymentPassword
            // 
            this.textBox_PaymentPassword.Location = new System.Drawing.Point(175, 124);
            this.textBox_PaymentPassword.MaxLength = 16;
            this.textBox_PaymentPassword.Name = "textBox_PaymentPassword";
            this.textBox_PaymentPassword.PasswordChar = '*';
            this.textBox_PaymentPassword.Size = new System.Drawing.Size(174, 21);
            this.textBox_PaymentPassword.TabIndex = 8;
            // 
            // button_RequestPayment
            // 
            this.button_RequestPayment.Location = new System.Drawing.Point(175, 151);
            this.button_RequestPayment.Name = "button_RequestPayment";
            this.button_RequestPayment.Size = new System.Drawing.Size(177, 40);
            this.button_RequestPayment.TabIndex = 10;
            this.button_RequestPayment.Text = "결제 요청";
            this.button_RequestPayment.UseVisualStyleBackColor = true;
            this.button_RequestPayment.Click += new System.EventHandler(this.button_RequestPayment_Click);
            // 
            // label_Status
            // 
            this.label_Status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Status.AutoSize = true;
            this.label_Status.Location = new System.Drawing.Point(10, 460);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(45, 12);
            this.label_Status.TabIndex = 7;
            this.label_Status.Text = "          ";
            // 
            // textBox_Status
            // 
            this.textBox_Status.Location = new System.Drawing.Point(12, 321);
            this.textBox_Status.MaxLength = 5000;
            this.textBox_Status.Multiline = true;
            this.textBox_Status.Name = "textBox_Status";
            this.textBox_Status.ReadOnly = true;
            this.textBox_Status.Size = new System.Drawing.Size(358, 136);
            this.textBox_Status.TabIndex = 11;
            this.textBox_Status.Text = "결제 요청을 해주세요.";
            // 
            // textBox_TxID
            // 
            this.textBox_TxID.Location = new System.Drawing.Point(43, 14);
            this.textBox_TxID.MaxLength = 512;
            this.textBox_TxID.Name = "textBox_TxID";
            this.textBox_TxID.Size = new System.Drawing.Size(306, 21);
            this.textBox_TxID.TabIndex = 11;
            this.textBox_TxID.Visible = false;
            // 
            // label_TxID
            // 
            this.label_TxID.AutoSize = true;
            this.label_TxID.Location = new System.Drawing.Point(6, 17);
            this.label_TxID.Name = "label_TxID";
            this.label_TxID.Size = new System.Drawing.Size(31, 12);
            this.label_TxID.TabIndex = 11;
            this.label_TxID.Text = "TxID";
            this.label_TxID.Visible = false;
            // 
            // button_StartPayment
            // 
            this.button_StartPayment.Location = new System.Drawing.Point(11, 48);
            this.button_StartPayment.Name = "button_StartPayment";
            this.button_StartPayment.Size = new System.Drawing.Size(338, 40);
            this.button_StartPayment.TabIndex = 11;
            this.button_StartPayment.Text = "결제 완료";
            this.button_StartPayment.UseVisualStyleBackColor = true;
            this.button_StartPayment.Visible = false;
            this.button_StartPayment.Click += new System.EventHandler(this.button_StartPayment_Click);
            // 
            // groupBox_StartPayment
            // 
            this.groupBox_StartPayment.Controls.Add(this.label_TxID);
            this.groupBox_StartPayment.Controls.Add(this.button_StartPayment);
            this.groupBox_StartPayment.Controls.Add(this.textBox_TxID);
            this.groupBox_StartPayment.Location = new System.Drawing.Point(12, 219);
            this.groupBox_StartPayment.Name = "groupBox_StartPayment";
            this.groupBox_StartPayment.Size = new System.Drawing.Size(358, 96);
            this.groupBox_StartPayment.TabIndex = 12;
            this.groupBox_StartPayment.TabStop = false;
            this.groupBox_StartPayment.Text = "결제 시작";
            this.groupBox_StartPayment.Visible = false;
            // 
            // checkBox_Save
            // 
            this.checkBox_Save.AutoSize = true;
            this.checkBox_Save.Checked = true;
            this.checkBox_Save.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Save.Location = new System.Drawing.Point(6, 164);
            this.checkBox_Save.Name = "checkBox_Save";
            this.checkBox_Save.Size = new System.Drawing.Size(168, 16);
            this.checkBox_Save.TabIndex = 13;
            this.checkBox_Save.Text = "다음에도 이 결제정보 사용";
            this.checkBox_Save.UseVisualStyleBackColor = true;
            this.checkBox_Save.CheckedChanged += new System.EventHandler(this.checkBox_Save_CheckedChanged);
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 481);
            this.Controls.Add(this.groupBox_StartPayment);
            this.Controls.Add(this.textBox_Status);
            this.Controls.Add(this.label_Status);
            this.Controls.Add(this.groupBox_PaymentRequest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentForm";
            this.Text = "Payment Form";
            this.groupBox_PaymentRequest.ResumeLayout(false);
            this.groupBox_PaymentRequest.PerformLayout();
            this.groupBox_StartPayment.ResumeLayout(false);
            this.groupBox_StartPayment.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Account;
        private System.Windows.Forms.Label label_Account;
        private System.Windows.Forms.Label label_CharName;
        private System.Windows.Forms.TextBox textBox_CharName;
        private System.Windows.Forms.Label label_유저지갑주소;
        private System.Windows.Forms.TextBox textBox_billsender;
        private System.Windows.Forms.GroupBox groupBox_PaymentRequest;
        private System.Windows.Forms.Label label_결제금액;
        private System.Windows.Forms.TextBox textBox_결제금액;
        private System.Windows.Forms.Label label_일회용결제비밀번호;
        private System.Windows.Forms.TextBox textBox_PaymentPassword;
        private System.Windows.Forms.Button button_RequestPayment;
        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.TextBox textBox_Status;
        private System.Windows.Forms.TextBox textBox_TxID;
        private System.Windows.Forms.Label label_TxID;
        private System.Windows.Forms.Button button_StartPayment;
        private System.Windows.Forms.GroupBox groupBox_StartPayment;
        private System.Windows.Forms.CheckBox checkBox_Save;
    }
}