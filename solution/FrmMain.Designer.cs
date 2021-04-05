
namespace FSimTrack
{
  partial class FrmMain
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) ) {
        components.Dispose( );
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
      this.lblVersion = new System.Windows.Forms.Label();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.lblAcft = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lblPing = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.btConnect = new bm98_Switches.UC_PushButtonRect();
      this.lblWCliStatusTxt = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.txRemIP = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.txRemPort = new System.Windows.Forms.TextBox();
      this.RTB = new System.Windows.Forms.RichTextBox();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.groupBox4.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblVersion
      // 
      this.lblVersion.AutoSize = true;
      this.lblVersion.Location = new System.Drawing.Point(18, 10);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new System.Drawing.Size(17, 17);
      this.lblVersion.TabIndex = 9;
      this.lblVersion.Text = "...";
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.lblAcft);
      this.groupBox4.Controls.Add(this.label2);
      this.groupBox4.Controls.Add(this.lblPing);
      this.groupBox4.Controls.Add(this.button1);
      this.groupBox4.Controls.Add(this.btConnect);
      this.groupBox4.Controls.Add(this.lblWCliStatusTxt);
      this.groupBox4.Controls.Add(this.label9);
      this.groupBox4.Controls.Add(this.txRemIP);
      this.groupBox4.Controls.Add(this.label6);
      this.groupBox4.Controls.Add(this.label7);
      this.groupBox4.Controls.Add(this.txRemPort);
      this.groupBox4.Location = new System.Drawing.Point(12, 31);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(250, 216);
      this.groupBox4.TabIndex = 10;
      this.groupBox4.TabStop = false;
      // 
      // lblAcft
      // 
      this.lblAcft.AutoSize = true;
      this.lblAcft.Location = new System.Drawing.Point(75, 114);
      this.lblAcft.Name = "lblAcft";
      this.lblAcft.Size = new System.Drawing.Size(17, 17);
      this.lblAcft.TabIndex = 29;
      this.lblAcft.Text = "...";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 114);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(38, 17);
      this.label2.TabIndex = 28;
      this.label2.Text = "FSim:";
      // 
      // lblPing
      // 
      this.lblPing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblPing.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPing.Location = new System.Drawing.Point(154, 185);
      this.lblPing.Name = "lblPing";
      this.lblPing.Size = new System.Drawing.Size(79, 18);
      this.lblPing.TabIndex = 27;
      this.lblPing.Text = "0";
      this.lblPing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // button1
      // 
      this.button1.ForeColor = System.Drawing.Color.Black;
      this.button1.Location = new System.Drawing.Point(154, 143);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(79, 31);
      this.button1.TabIndex = 25;
      this.button1.Text = "Request";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Visible = false;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // btConnect
      // 
      this.btConnect.BackColor = System.Drawing.Color.Transparent;
      this.btConnect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btConnect.BackgroundImage")));
      this.btConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.btConnect.ButtonText = "Connect";
      this.btConnect.CaptureWheel = false;
      this.btConnect.HotTracking = true;
      this.btConnect.Location = new System.Drawing.Point(0, 143);
      this.btConnect.Name = "btConnect";
      this.btConnect.OnState = false;
      this.btConnect.PushOffColor = bm98_Switches.SwitchColor.Amber;
      this.btConnect.PushOnColor = bm98_Switches.SwitchColor.Green;
      this.btConnect.RepeatWhilePressed = false;
      this.btConnect.Size = new System.Drawing.Size(122, 60);
      this.btConnect.TabIndex = 24;
      this.btConnect.PushbuttonPressed += new System.EventHandler<System.EventArgs>(this.btConnect_PushbuttonPressed);
      // 
      // lblWCliStatusTxt
      // 
      this.lblWCliStatusTxt.AutoSize = true;
      this.lblWCliStatusTxt.Location = new System.Drawing.Point(75, 82);
      this.lblWCliStatusTxt.Name = "lblWCliStatusTxt";
      this.lblWCliStatusTxt.Size = new System.Drawing.Size(29, 17);
      this.lblWCliStatusTxt.TabIndex = 22;
      this.lblWCliStatusTxt.Text = "idle";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(6, 82);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(46, 17);
      this.label9.TabIndex = 21;
      this.label9.Text = "Status:";
      // 
      // txRemIP
      // 
      this.txRemIP.Location = new System.Drawing.Point(78, 15);
      this.txRemIP.Name = "txRemIP";
      this.txRemIP.Size = new System.Drawing.Size(161, 25);
      this.txRemIP.TabIndex = 11;
      this.txRemIP.Text = "192.168.1.1";
      this.txRemIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 46);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(35, 17);
      this.label6.TabIndex = 14;
      this.label6.Text = "Port:";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(6, 18);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(62, 17);
      this.label7.TabIndex = 13;
      this.label7.Text = "Server IP:";
      // 
      // txRemPort
      // 
      this.txRemPort.Location = new System.Drawing.Point(78, 43);
      this.txRemPort.Name = "txRemPort";
      this.txRemPort.Size = new System.Drawing.Size(161, 25);
      this.txRemPort.TabIndex = 12;
      this.txRemPort.Text = "9042";
      this.txRemPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // RTB
      // 
      this.RTB.BackColor = System.Drawing.Color.MidnightBlue;
      this.RTB.DetectUrls = false;
      this.RTB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RTB.ForeColor = System.Drawing.Color.Orange;
      this.RTB.Location = new System.Drawing.Point(12, 253);
      this.RTB.Name = "RTB";
      this.RTB.ReadOnly = true;
      this.RTB.Size = new System.Drawing.Size(250, 149);
      this.RTB.TabIndex = 26;
      this.RTB.Text = "Issues are reported here...";
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // FrmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(15)))));
      this.ClientSize = new System.Drawing.Size(278, 416);
      this.Controls.Add(this.groupBox4);
      this.Controls.Add(this.RTB);
      this.Controls.Add(this.lblVersion);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "FrmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "FSimTrack";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
      this.Load += new System.EventHandler(this.FrmMain_Load);
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblVersion;
    private System.Windows.Forms.GroupBox groupBox4;
    private bm98_Switches.UC_PushButtonRect btConnect;
    private System.Windows.Forms.Label lblWCliStatusTxt;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txRemIP;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txRemPort;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label lblPing;
    private System.Windows.Forms.RichTextBox RTB;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Label lblAcft;
    private System.Windows.Forms.Label label2;
  }
}

