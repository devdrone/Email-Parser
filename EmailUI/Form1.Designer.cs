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
            this.SuspendLayout();
            // 
            // logger
            // 
            this.logger.Location = new System.Drawing.Point(12, 41);
            this.logger.Name = "logger";
            this.logger.Size = new System.Drawing.Size(323, 264);
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
            this.progress.Location = new System.Drawing.Point(12, 311);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 347);
            this.Controls.Add(this.cancle);
            this.Controls.Add(this.progressPercentage);
            this.Controls.Add(this.fromID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.signIn);
            this.Controls.Add(this.logger);
            this.Name = "Form1";
            this.Text = "EMAIL Parser";
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
    }
}

