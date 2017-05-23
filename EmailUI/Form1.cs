using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using IniParser;
using IniParser.Model;

namespace EmailUI
{
    public partial class Form1 : Form
    {
        Settings resumeFrom = new Settings();
        Stopwatch watch = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
            serviceCheck();
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
                emaiIds = emaildata.getMails(logger, int.Parse(resumeFrom.fromID));
            }
            totalMail.Invoke(new Action(() => { totalMail.Text = "Total Mails : " + emaiIds.Count.ToString(); }));
            logger.Invoke(new Action(() => { logger.ResetText(); }));
            int i = 1;
            int syn = 1;
            int s = 1;
            int err = 1;
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
                        synced.Invoke(new Action(() => { synced.Text = "Synced      : " + syn.ToString(); }));
                        syn++;
                    }
                    else
                    {
                        skipped.Invoke(new Action(() => { skipped.Text = "Skipped : " + s; }));
                        s++;
                    }
                    resumeFrom.fromID = emailno.ToString();
                    resumeFrom.Save();
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
                catch (Exception ex)
                {
                    error.Invoke(new Action(() => { error.Text = "Error      : " + err; }));
                    err++;
                }
                emaildata.remainingTime(emaiIds.Count - i, remTime);
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
            watch.Stop();
            timer1.Stop();
            signIn.Enabled = true;
            cancle.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            watch.Start();
            signIn.Enabled = false;
            cancle.Enabled = true;
            startService.Enabled = false;
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

        private void installService_Click(object sender, EventArgs e)
        {
            try
            {
                logger.Invoke(new Action(() => { logger.Text = "Installing Service.."; }));
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Verb = "runas";
                startInfo.Arguments = "/K cd " + Path.GetFullPath(Application.StartupPath) + "&" + GetdotnetVersion() + "InstallUtil.exe EmailService.exe";
                process.StartInfo = startInfo;
                process.Start();
                logger.Invoke(new Action(() => { logger.Text = "Service Installed."; }));
                serviceCheck();
            }
            catch (Exception ex)
            {
                logger.Invoke(new Action(() => { logger.Text = "Error : " + ex.Message; }));
            }
        }

        private string GetdotnetVersion()
        {
            try
            {
                using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
                {
                    string releaseKey = Convert.ToString(ndpKey.GetValue("InstallPath"));
                    if (true)
                    {
                        return releaseKey;
                    }
                }
            }
            catch (Exception)
            {
                logger.Invoke(new Action(() => { logger.Text = ".NET Framework v4.5+ is required, Please install and try again."; }));
                return string.Empty;
            }
        }

        private void serviceCheck()
        {
            try
            {
                ServiceController service = new ServiceController("Email Parser Service");
                switch (service.Status)
                {
                    case ServiceControllerStatus.ContinuePending:
                        serviceStatus.Text = "Service : Continue Pending";
                        installService.Enabled = false;
                        serviceCheck();
                        break;
                    case ServiceControllerStatus.PausePending:
                        serviceStatus.Text = "Service : Pause Pending";
                        installService.Enabled = false;
                        serviceCheck();
                        break;
                    case ServiceControllerStatus.Paused:
                        serviceStatus.Text = "Service : Paused";
                        installService.Enabled = false;
                        break;
                    case ServiceControllerStatus.Running:
                        serviceStatus.Text = "Service : Running";
                        startService.Enabled = false;
                        signIn.Enabled = false;
                        cancle.Enabled = false;
                        installService.Enabled = false;
                        stopService.Enabled = true;
                        logger.Invoke(new Action(() => { logger.Text = "Service Running. Stop service to run manually."; }));
                        break;
                    case ServiceControllerStatus.StartPending:
                        serviceStatus.Text = "Service : Start Pending";
                        serviceCheck();
                        installService.Enabled = false;
                        break;
                    case ServiceControllerStatus.StopPending:
                        serviceStatus.Text = "Service : Stop Pending";
                        installService.Enabled = false;
                        serviceCheck();
                        break;
                    case ServiceControllerStatus.Stopped:
                        serviceStatus.Text = "Service : Stopped";
                        stopService.Enabled = false;
                        startService.Enabled = true;
                        installService.Enabled = false;
                        signIn.Enabled = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                serviceStatus.Text = "Service : Not Installed";
                installService.Enabled = true;
            }
        }

        private void startService_Click(object sender, EventArgs e)
        {
            try
            {
                logger.Invoke(new Action(() => { logger.Text = "Starting Service.."; }));
                ServiceController service = new ServiceController("Email Parser Service");
                service.Start();
                logger.Invoke(new Action(() => { logger.Text = "Service Started."; }));
            }
            catch (Exception ex)
            {
                logger.Invoke(new Action(() => { logger.Text = "Error : " + ex.Message; }));
            }
            serviceCheck();
        }

        private void stopService_Click(object sender, EventArgs e)
        {
            try
            {
                logger.Invoke(new Action(() => { logger.Text = "Stopping Service.."; }));
                ServiceController service = new ServiceController("Email Parser Service");
                service.Stop();
                logger.Invoke(new Action(() => { logger.Text = "Service Stopped."; }));
            }
            catch (Exception ex)
            {
                logger.Invoke(new Action(() => { logger.Text = "Error : " + ex.Message; }));
            }
            serviceCheck();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string time = string.Format("Elapsed : {0}:{1}:{2}", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds);
            totalTime.Text = time;
        }
    }
}
