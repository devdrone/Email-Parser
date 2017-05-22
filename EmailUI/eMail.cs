using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using MySql.Data.MySqlClient;
using IniParser;
using IniParser.Model;
using ActiveUp.Net.Mail;

namespace EmailUI
{
    class eMail
    {
        Imap4Client client = new Imap4Client();
        Mailbox mails = new Mailbox();
        public class emailid
        {
            public string Sender { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public byte[] Attachment { get; set; }
            public string Heading { get; set; }
            public string Key_skill { get; set; }
            public string Name { get; set; }
            public string Total_experiance { get; set; }
            public string Ctc { get; set; }
            public string Current_employer { get; set; }
            public string Current_designation { get; set; }
            public string Last_employer { get; set; }
            public string Last_designation { get; set; }
            public string Current_location { get; set; }
            public string Preferred_location { get; set; }
            public string Education { get; set; }
            public string Mobile { get; set; }
            public string Landline { get; set; }
            public string Notice_period { get; set; }
            public string emailID { get; set; }
        }

        public List<int> getMails(RichTextBox logger,TextBox id)
        {
            int mailID = Convert.ToInt32(id.Text);
            try
            {
                client.ConnectSsl("imap.gmail.com", 993);
                LogUpdate(logger, "Connected to Gmail Server.", false);
                client.Login("hrd@selectiveindia.in", "Ndbhdispc");//("developer1@mobotics.in", "mobotics@01");//
                LogUpdate(logger, "Logged in Successfully", false);
                mails = client.SelectMailbox("INBOX");
                List<int> messageids = mails.Search("FROM \"@naukri.com\"").ToList();//("ALL");//
                List<int> selectMessage = messageids.Where(n => n > mailID).ToList();
                string log = string.Format("{0} mails fetched", selectMessage.Count);
                LogUpdate(logger, log, false);
                Thread.Sleep(3000);
                return selectMessage;
            }
            catch (Exception ex)
            {
                LogUpdate(logger, ex.Message, true);
                return null;
            }
        }

        public ActiveUp.Net.Mail.Message readMails(int messageid, RichTextBox logger)
        {
            string log = string.Empty;
            //logger.ResetText();
            try
            {
                LogUpdate(logger, string.Format("> ID:{0} Validating mail", messageid), false);
                var message = mails.Fetch.MessageObject(messageid);
                if (message.Subject.Contains("star applicant - Naukri.com"))
                {
                    LogUpdate(logger, string.Format("> ID:{0} email Validated.", messageid), false);
                    return message;
                }
                else
                {
                    LogUpdate(logger, string.Format("> ID:{0} Invalid eMail. Skipping..", messageid), false);
                    //Thread.Sleep(2000);
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogUpdate(logger, ex.Message, true);
                return null;
            }
        }

        public void emailToDatabase(emailid maildata, RichTextBox log)
        {
            try
            {
                LogUpdate(log, "\n ", false);
                LogUpdate(log, "Connecting to Database server..", false);
                var serverdetail = iniparser("mysql-config.ini");
                string sqlconnection = string.Format("Server={0};Database={1};uid={2};pwd={3};Allow User Variables=True", serverdetail.name, serverdetail.database, serverdetail.user, serverdetail.pass);
                MySqlConnection connection = new MySqlConnection(sqlconnection);
                connection.Open();
                LogUpdate(log, "Connected to Database server. \nCopying..", false);
                string query = string.Format("INSERT INTO test.email(sender,subject,body,attachment,resume_headline,key_skill,name,total_experience,ctc,current_employer,current_designation,last_employer,last_designation,current_location,preferred_location,education,mobile,landline,notice_period,emailid) VALUES(@SENDER,@SUBJECT,@BODY,@ATTACH,@HEAD,@SKILL,@NAME,@TEXP,@CTC,@CEMP,@CDSG,@LEMP,@LDSG,@CLOC,@PLOC,@EDU,@MOB,@LAND,@NP,@EID)");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@SENDER", MySqlDbType.VarChar, 100);
                command.Parameters.Add("@SUBJECT", MySqlDbType.VarChar, 100);
                command.Parameters.Add("@BODY", MySqlDbType.Text);
                command.Parameters.Add("@ATTACH", MySqlDbType.MediumBlob);
                command.Parameters.Add("@HEAD", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@SKILL", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@NAME", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@TEXP", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@CTC", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@CEMP", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@CDSG", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@LEMP", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@LDSG", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@CLOC", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@PLOC", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@EDU", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@MOB", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@LAND", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@NP", MySqlDbType.VarChar, 255);
                command.Parameters.Add("@EID", MySqlDbType.VarChar, 255);

                command.Parameters["@SENDER"].Value = maildata.Sender;
                command.Parameters["@SUBJECT"].Value = maildata.Subject;
                command.Parameters["@BODY"].Value = maildata.Body;
                command.Parameters["@ATTACH"].Value = maildata.Attachment;
                command.Parameters["@HEAD"].Value = maildata.Heading;
                command.Parameters["@SKILL"].Value = maildata.Key_skill;
                command.Parameters["@NAME"].Value = maildata.Name;
                command.Parameters["@TEXP"].Value = maildata.Total_experiance;
                command.Parameters["@CTC"].Value = maildata.Ctc;
                command.Parameters["@CEMP"].Value = maildata.Current_employer;
                command.Parameters["@CDSG"].Value = maildata.Current_designation;
                command.Parameters["@LEMP"].Value = maildata.Last_employer;
                command.Parameters["@LDSG"].Value = maildata.Last_designation;
                command.Parameters["@CLOC"].Value = maildata.Current_location;
                command.Parameters["@PLOC"].Value = maildata.Preferred_location;
                command.Parameters["@EDU"].Value = maildata.Education;
                command.Parameters["@MOB"].Value = maildata.Mobile;
                command.Parameters["@LAND"].Value = maildata.Landline;
                command.Parameters["@NP"].Value = maildata.Notice_period;
                command.Parameters["@EID"].Value = maildata.emailID;

                int reader = command.ExecuteNonQuery();
                LogUpdate(log, "Mail from " + maildata.Sender + " copied to Database", false);
            }
            catch (Exception ex)
            {
                LogUpdate(log, ex.Message, true);
            }
        }

        public dynamic iniparser(string path)
        {
            try
            {
                dynamic server = new ExpandoObject();
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(path);
                server.name = data["serverdetail"]["server"];
                server.database = data["serverdetail"]["database"];
                server.user = data["serverdetail"]["username"];
                server.pass = data["serverdetail"]["password"];
                return server;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public void lableUpdate(Label mail, string msg, string value)
        {
            mail.Text = string.Format("{0}:{1}", msg, value);
        }

        public void LogUpdate(RichTextBox logger, string message, bool error)
        {
            if (error)
            {
                logger.ForeColor = Color.Red;
                logger.Text = message + "\n";
            }
            else
            {
                logger.ForeColor = Color.Black;
                logger.Text += message + "\n";
            }
            logger.SelectionStart = logger.Text.Length;
            logger.ScrollToCaret();
            logger.Update();
        }

        public string BodyParse(string detail, string searchText, RichTextBox logger)
        {
            try
            {
                return detail.Substring(detail.IndexOf(':') + 1).Trim();
            }
            catch (Exception ex)
            {
                LogUpdate(logger, ex.Message, true);
                return "N/A";
            }
        }

        public int progressValue(double value)
        {
            double incrementValue = 100 / value;
            return (int)(incrementValue);
        }

        public List<string> htmlDetailsParser(string body)
        {
            int firstIndex = 0;
            int lastIndex = 0;
            List<string> candidateDetails = new List<string>();
            try
            {
                if (body.Contains("Notice Period"))
                {
                    firstIndex = body.IndexOf("Resume Headline");
                    lastIndex = body.IndexOf("Notice Period") + 20;
                    candidateDetails = body.Substring(firstIndex, lastIndex - firstIndex).Replace("          ", "\n").Split('\n').ToList();
                }
                else if (body.Contains("Landline"))
                {
                    firstIndex = body.IndexOf("Resume Headline");
                    lastIndex = body.IndexOf("Landline") + 20;
                    candidateDetails = body.Substring(firstIndex, lastIndex - firstIndex).Replace("          ", "\n").Split('\n').ToList();
                    candidateDetails.Add("Notice Period : N/A");
                }
                else
                {
                    firstIndex = body.IndexOf("Resume Headline");
                    lastIndex = body.IndexOf("Mobile") + 20;
                    candidateDetails = body.Substring(firstIndex, lastIndex - firstIndex).Replace("          ", "\n").Split('\n').ToList();
                    candidateDetails.Add("Landline : N/A");
                    candidateDetails.Add("Notice Period : N/A");

                }

                if (candidateDetails.Count == 14)
                {
                    candidateDetails.Add("Notice Period : N/A");
                }
                return candidateDetails;
            }
            catch (Exception ex)
            {
                return candidateDetails;
            }
        }
    }
}

