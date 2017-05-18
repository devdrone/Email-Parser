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
            this.cancle = new System.Windows.Forms.Button();
            this.alternateProcess = new System.ComponentModel.BackgroundWorker();
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
            this.signIn.Location = new System.Drawing.Point(12, 12);
            this.signIn.Name = "signIn";
            this.signIn.Size = new System.Drawing.Size(158, 23);
            this.signIn.TabIndex = 5;
            this.signIn.Text = "Sign In && Copy !";
            this.signIn.UseVisualStyleBackColor = true;
            this.signIn.Click += new System.EventHandler(this.button1_Click);
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(12, 311);
            this.progress.MarqueeAnimationSpeed = 50;
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(322, 24);
            this.progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progress.TabIndex = 6;
            // 
            // cancle
            // 
            this.cancle.Location = new System.Drawing.Point(176, 12);
            this.cancle.Name = "cancle";
            this.cancle.Size = new System.Drawing.Size(158, 23);
            this.cancle.TabIndex = 7;
            this.cancle.Text = "Cancle";
            this.cancle.UseVisualStyleBackColor = true;
            // 
            // alternateProcess
            // 
            this.alternateProcess.WorkerReportsProgress = true;
            this.alternateProcess.WorkerSupportsCancellation = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 347);
            this.Controls.Add(this.cancle);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.signIn);
            this.Controls.Add(this.logger);
            this.Name = "Form1";
            this.Text = "EMAIL Parser";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox logger;
        private System.Windows.Forms.Button signIn;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Button cancle;
        private System.ComponentModel.BackgroundWorker alternateProcess;
    }
}

