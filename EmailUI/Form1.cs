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
using HtmlAgilityPack;

namespace EmailUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            alternateProcess.DoWork += new DoWorkEventHandler(alternateProcess_DoWork);
            alternateProcess.ProgressChanged += new ProgressChangedEventHandler(alternateProcess_Progress);
            alternateProcess.RunWorkerCompleted += new RunWorkerCompletedEventHandler(alternateProcess_Completed);
        }

        private void alternateProcess_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void alternateProcess_Progress(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void alternateProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            eMail.emailid maildata = new eMail.emailid();
            eMail emaildata = new eMail();
            logger.ResetText();
            var emaiIds = emaildata.getMails(logger);
            progress.Minimum = 0;
            progress.Maximum = emaiIds.Length;
            foreach (var emailno in emaiIds)
            {
                var eMail = emaildata.readMails(emailno, logger);
                if (eMail != null)
                {
                    maildata.Sender = eMail.From.Name;
                    maildata.Subject = eMail.Subject;
                    maildata.Body = eMail.BodyHtml.Text;
                    var maildataList = emaildata.htmlDetailsParser(eMail.BodyHtml.Text);
                    maildata.Heading = emaildata.BodyParse(maildataList.ElementAt(0), "Resume Headline", logger);
                    maildata.Key_skill = emaildata.BodyParse(maildataList.ElementAt(1),"Key Skill",logger);
                    maildata.Name = emaildata.BodyParse(maildataList.ElementAt(2),"Name",logger);
                    maildata.Total_experiance = emaildata.BodyParse(maildataList.ElementAt(3),"Total Experience",logger);
                    maildata.Ctc = maildataList.ElementAt(4);
                    maildata.Current_employer = emaildata.BodyParse(maildataList.ElementAt(5),"Current Employer",logger);
                    maildata.Current_designation = emaildata.BodyParse(maildataList.ElementAt(6),"Current Designation",logger);
                    maildata.Last_employer = emaildata.BodyParse(maildataList.ElementAt(7),"Last Employer",logger);
                    maildata.Last_designation = emaildata.BodyParse(maildataList.ElementAt(8),"Last Designation",logger);
                    maildata.Current_location = emaildata.BodyParse(maildataList.ElementAt(9),"Current Location",logger);
                    maildata.Preferred_location = emaildata.BodyParse(maildataList.ElementAt(10),"Preferred Location",logger);
                    maildata.Education = emaildata.BodyParse(maildataList.ElementAt(11),"Education",logger);
                    maildata.Mobile = emaildata.BodyParse(maildataList.ElementAt(12),"Mobile",logger);
                    maildata.Landline = maildataList.ElementAt(13);
                    maildata.Notice_period = emaildata.BodyParse(maildataList.ElementAt(14), "Notice Period", logger);
                    emaildata.emailToDatabase(maildata, logger);
                }
                progress.Value += i;
            }
        }
    }
}
