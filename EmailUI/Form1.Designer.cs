namespace EmailUI
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
            this.logger = new System.Windows.Forms.RichTextBox();
            this.signIn = new System.Windows.Forms.Button();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.fromID = new System.Windows.Forms.TextBox();
            this.multiThread = new System.ComponentModel.BackgroundWorker();
            this.progressPercentage = new System.Windows.Forms.Label();
            this.cancle = new System.Windows.Forms.Button();
            this.startService = new System.Windows.Forms.Button();
            this.stopService = new System.Windows.Forms.Button();
            this.installService = new System.Windows.Forms.Button();
            this.serviceStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.totalMail = new System.Windows.Forms.Label();
            this.synced = new System.Windows.Forms.Label();
            this.skipped = new System.Windows.Forms.Label();
            this.error = new System.Windows.Forms.Label();
            this.remTime = new System.Windows.Forms.Label();
            this.projectInstaller1 = new EmailUI.ProjectInstaller();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // logger
            // 
            this.logger.BackColor = System.Drawing.SystemColors.ControlDark;
            this.logger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logger.Cursor = System.Windows.Forms.Cursors.Default;
            this.logger.Location = new System.Drawing.Point(12, 70);
            this.logger.Name = "logger";
            this.logger.ReadOnly = true;
            this.logger.Size = new System.Drawing.Size(323, 194);
            this.logger.TabIndex = 4;
            this.logger.Text = "";
            // 
            // signIn
            // 
            this.signIn.Location = new System.Drawing.Point(158, 12);
            this.signIn.Name = "signIn";
            this.signIn.Size = new System.Drawing.Size(85, 23);
            this.signIn.TabIndex = 5;
            this.signIn.Text = "Sign In && Copy";
            this.signIn.UseVisualStyleBackColor = true;
            this.signIn.Click += new System.EventHandler(this.button1_Click);
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(12, 358);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(322, 24);
            this.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progress.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Start form ID";
            // 
            // fromID
            // 
            this.fromID.Location = new System.Drawing.Point(84, 13);
            this.fromID.Name = "fromID";
            this.fromID.Size = new System.Drawing.Size(68, 20);
            this.fromID.TabIndex = 8;
            this.fromID.Text = "0";
            // 
            // multiThread
            // 
            this.multiThread.WorkerReportsProgress = true;
            this.multiThread.WorkerSupportsCancellation = true;
            this.multiThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.multiThread_DoWork);
            this.multiThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.multiThread_ProgressChanged);
            this.multiThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.multiThread_RunWorkerCompleted);
            // 
            // progressPercentage
            // 
            this.progressPercentage.AutoSize = true;
            this.progressPercentage.Location = new System.Drawing.Point(152, 317);
            this.progressPercentage.Name = "progressPercentage";
            this.progressPercentage.Size = new System.Drawing.Size(0, 13);
            this.progressPercentage.TabIndex = 9;
            // 
            // cancle
            // 
            this.cancle.Location = new System.Drawing.Point(249, 12);
            this.cancle.Name = "cancle";
            this.cancle.Size = new System.Drawing.Size(85, 23);
            this.cancle.TabIndex = 10;
            this.cancle.Text = "Cancle";
            this.cancle.UseVisualStyleBackColor = true;
            this.cancle.Click += new System.EventHandler(this.cancle_Click);
            // 
            // startService
            // 
            this.startService.Location = new System.Drawing.Point(103, 42);
            this.startService.Name = "startService";
            this.startService.Size = new System.Drawing.Size(49, 23);
            this.startService.TabIndex = 11;
            this.startService.Text = "Start";
            this.startService.UseVisualStyleBackColor = true;
            this.startService.Click += new System.EventHandler(this.startService_Click);
            // 
            // stopService
            // 
            this.stopService.Location = new System.Drawing.Point(155, 42);
            this.stopService.Name = "stopService";
            this.stopService.Size = new System.Drawing.Size(49, 23);
            this.stopService.TabIndex = 12;
            this.stopService.Text = "Stop";
            this.stopService.UseVisualStyleBackColor = true;
            this.stopService.Click += new System.EventHandler(this.stopService_Click);
            // 
            // installService
            // 
            this.installService.Location = new System.Drawing.Point(12, 42);
            this.installService.Name = "installService";
            this.installService.Size = new System.Drawing.Size(85, 22);
            this.installService.TabIndex = 13;
            this.installService.Text = "Install Service";
            this.installService.UseVisualStyleBackColor = true;
            this.installService.Click += new System.EventHandler(this.installService_Click);
            // 
            // serviceStatus
            // 
            this.serviceStatus.AutoSize = true;
            this.serviceStatus.Location = new System.Drawing.Point(225, 47);
            this.serviceStatus.Name = "serviceStatus";
            this.serviceStatus.Size = new System.Drawing.Size(92, 13);
            this.serviceStatus.TabIndex = 14;
            this.serviceStatus.Text = "Service : Stopped";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.remTime);
            this.panel1.Controls.Add(this.error);
            this.panel1.Controls.Add(this.skipped);
            this.panel1.Controls.Add(this.synced);
            this.panel1.Controls.Add(this.totalMail);
            this.panel1.Location = new System.Drawing.Point(12, 270);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 82);
            this.panel1.TabIndex = 15;
            // 
            // totalMail
            // 
            this.totalMail.AutoSize = true;
            this.totalMail.Location = new System.Drawing.Point(15, 10);
            this.totalMail.Name = "totalMail";
            this.totalMail.Size = new System.Drawing.Size(64, 13);
            this.totalMail.TabIndex = 0;
            this.totalMail.Text = "Total Mails :";
            // 
            // synced
            // 
            this.synced.AutoSize = true;
            this.synced.Location = new System.Drawing.Point(15, 37);
            this.synced.Name = "synced";
            this.synced.Size = new System.Drawing.Size(64, 13);
            this.synced.TabIndex = 1;
            this.synced.Text = "Synced      :";
            // 
            // skipped
            // 
            this.skipped.AutoSize = true;
            this.skipped.Location = new System.Drawing.Point(177, 10);
            this.skipped.Name = "skipped";
            this.skipped.Size = new System.Drawing.Size(52, 13);
            this.skipped.TabIndex = 2;
            this.skipped.Text = "Skipped :";
            // 
            // error
            // 
            this.error.AutoSize = true;
            this.error.Location = new System.Drawing.Point(179, 37);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(50, 13);
            this.error.TabIndex = 3;
            this.error.Text = "Error      :";
            // 
            // remTime
            // 
            this.remTime.AutoSize = true;
            this.remTime.Location = new System.Drawing.Point(51, 60);
            this.remTime.Name = "remTime";
            this.remTime.Size = new System.Drawing.Size(92, 13);
            this.remTime.TabIndex = 4;
            this.remTime.Text = "Remaining Time : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 395);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.serviceStatus);
            this.Controls.Add(this.installService);
            this.Controls.Add(this.stopService);
            this.Controls.Add(this.startService);
            this.Controls.Add(this.cancle);
            this.Controls.Add(this.progressPercentage);
            this.Controls.Add(this.fromID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.signIn);
            this.Controls.Add(this.logger);
            this.Name = "Form1";
            this.Text = "EMAIL Parser";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox logger;
        private System.Windows.Forms.Button signIn;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fromID;
        private System.ComponentModel.BackgroundWorker multiThread;
        private System.Windows.Forms.Label progressPercentage;
        private System.Windows.Forms.Button cancle;
        private System.Windows.Forms.Button startService;
        private System.Windows.Forms.Button stopService;
        private System.Windows.Forms.Button installService;
        private System.Windows.Forms.Label serviceStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label totalMail;
        private System.Windows.Forms.Label error;
        private System.Windows.Forms.Label skipped;
        private System.Windows.Forms.Label synced;
        private System.Windows.Forms.Label remTime;
        private ProjectInstaller projectInstaller1;
    }
}

