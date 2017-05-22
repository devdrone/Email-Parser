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

        private void button1_Click(object sender, EventArgs e)
        {
            eMail.emailid maildata = new eMail.emailid();
            eMail emaildata = new eMail();
            logger.ResetText();
            var emaiIds = emaildata.getMails(logger,fromID);
            int i = 1;
            progress.Minimum = 0;
            progress.Maximum = emaiIds.Count;
            foreach (var emailno in emaiIds)
            {
                try
                {
                    var eMail = emaildata.readMails(emailno, logger);
                    if (eMail != null && eMail.IsHtml && eMail.Attachments.Count > 0)
                    {
                        maildata.Sender = eMail.From.Name;
                        maildata.Subject = eMail.Subject;
                        maildata.Body = eMail.BodyHtml.Text;
                        maildata.Attachment = eMail.Attachments[0].BinaryContent;
                        var maildataList = emaildata.htmlDetailsParser(eMail.BodyHtml.TextStripped);
                        maildata.Heading = emaildata.BodyParse(maildataList.ElementAt(0), "Resume Headline", logger);
                        maildata.Key_skill = emaildata.BodyParse(maildataList.ElementAt(1), "Key Skill", logger);
                        maildata.Name = emaildata.BodyParse(maildataList.ElementAt(2), "Name", logger);
                        maildata.Total_experiance = emaildata.BodyParse(maildataList.ElementAt(3), "Total Experience", logger);
                        maildata.Ctc = emaildata.BodyParse(maildataList.ElementAt(4), "CTC", logger);
                        maildata.Current_employer = emaildata.BodyParse(maildataList.ElementAt(5), "Current Employer", logger);
                        maildata.Current_designation = emaildata.BodyParse(maildataList.ElementAt(6), "Current Designation", logger);
                        maildata.Last_employer = emaildata.BodyParse(maildataList.ElementAt(7), "Last Employer", logger);
                        maildata.Last_designation = emaildata.BodyParse(maildataList.ElementAt(8), "Last Designation", logger);
                        maildata.Current_location = emaildata.BodyParse(maildataList.ElementAt(9), "Current Location", logger);
                        maildata.Preferred_location = emaildata.BodyParse(maildataList.ElementAt(10), "Preferred Location", logger);
                        maildata.Education = emaildata.BodyParse(maildataList.ElementAt(11), "Education", logger);
                        maildata.Mobile = emaildata.BodyParse(maildataList.ElementAt(12), "Mobile", logger);
                        maildata.Landline = emaildata.BodyParse(maildataList.ElementAt(13), "Landline", logger);
                        maildata.Notice_period = emaildata.BodyParse(maildataList.ElementAt(14), "Notice Period", logger);
                        maildata.emailID = emailno.ToString();
                        emaildata.emailToDatabase(maildata, logger);
                        logger.Text = string.Format("{0} mails left.\n", emaiIds.Count - i);
                    }
                    progress.Value = i;
                    i++;
                }
                catch (Exception ex)
                {
                    logger.Text = "Error : " + ex.Message;
                }
            }
        }
    }
}
