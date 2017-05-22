using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using IniParser;
using IniParser.Model;

namespace EmailUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void multiThread_DoWork(object sender, DoWorkEventArgs e)
        {
            TextBox resumeID = (TextBox)e.Argument;
            List<int> emaiIds = new List<int>();
            eMail.emailid maildata = new eMail.emailid();
            eMail emaildata = new eMail();
            if (resumeID.Text.Length > 1)
            {
                emaiIds = emaildata.getMails(logger, int.Parse(resumeID.Text));
            }
            else
            {
                emaiIds = emaildata.getMails(logger, int.Parse(Properties.Settings.Default.fromID));
            }
            logger.Invoke(new Action(() => { logger.ResetText(); }));
            int i = 1;
            foreach (var emailno in emaiIds)
            {
                var eMail = emaildata.readMails(emailno, logger);
                if (eMail != null && eMail.IsHtml && eMail.Attachments.Count > 0)
                {
                    maildata.Sender = eMail.From.Name;
                    maildata.Subject = eMail.Subject;
                    maildata.Body = eMail.BodyHtml.Text;
                    maildata.Attachment = eMail.Attachments[0].BinaryContent;
                    var maildataList = emaildata.htmlDetailsParser(eMail.BodyHtml.TextStripped);
                    maildata.Heading = emaildata.BodyParse(maildataList.ElementAt(0), "Resume Headline");
                    maildata.Key_skill = emaildata.BodyParse(maildataList.ElementAt(1), "Key Skill");
                    maildata.Name = emaildata.BodyParse(maildataList.ElementAt(2), "Name");
                    maildata.Total_experiance = emaildata.BodyParse(maildataList.ElementAt(3), "Total Experience");
                    maildata.Ctc = emaildata.BodyParse(maildataList.ElementAt(4), "CTC");
                    maildata.Current_employer = emaildata.BodyParse(maildataList.ElementAt(5), "Current Employer");
                    maildata.Current_designation = emaildata.BodyParse(maildataList.ElementAt(6), "Current Designation");
                    maildata.Last_employer = emaildata.BodyParse(maildataList.ElementAt(7), "Last Employer");
                    maildata.Last_designation = emaildata.BodyParse(maildataList.ElementAt(8), "Last Designation");
                    maildata.Current_location = emaildata.BodyParse(maildataList.ElementAt(9), "Current Location");
                    maildata.Preferred_location = emaildata.BodyParse(maildataList.ElementAt(10), "Preferred Location");
                    maildata.Education = emaildata.BodyParse(maildataList.ElementAt(11), "Education");
                    maildata.Mobile = emaildata.BodyParse(maildataList.ElementAt(12), "Mobile");
                    maildata.Landline = emaildata.BodyParse(maildataList.ElementAt(13), "Landline");
                    maildata.Notice_period = emaildata.BodyParse(maildataList.ElementAt(14), "Notice Period");
                    maildata.emailID = emailno.ToString();
                    emaildata.emailToDatabase(maildata, logger);
                }
                Properties.Settings.Default.fromID = emailno.ToString();
                Properties.Settings.Default.Save();
                logger.Invoke(new Action(() => { logger.ResetText(); }));
                multiThread.ReportProgress(i * 100 / emaiIds.Count);
                i++;
                if (multiThread.CancellationPending == true)
                {
                    e.Cancel = true;
                    multiThread.ReportProgress(0);
                    return;
                }
            }
        }

        private void multiThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
            progressPercentage.Text = progress.Value.ToString() + "%";
        }

        private void multiThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                progressPercentage.Text = "Cancelled";
            }
            else if (e.Error != null)
            {
                progressPercentage.Text = "Error";
            }
            else
            {
                progressPercentage.Text = "Completed";
            }
            signIn.Enabled = true;
            cancle.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            signIn.Enabled = false;
            cancle.Enabled = true;
            multiThread.RunWorkerAsync(fromID);
        }

        private void cancle_Click(object sender, EventArgs e)
        {
            if (multiThread.IsBusy)
            {
                multiThread.CancelAsync();
            }
            else
            {
                progressPercentage.Text = "Nothing to cancle";
            }
        }
    }
}
