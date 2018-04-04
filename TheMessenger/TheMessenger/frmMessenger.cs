using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Media;


/// <summary>
/// Author: Peter Adam
/// 
/// v1.0 Date: Mar 13, 2018
/// TheMessenger complies with the MVP outlined by my instructors' spec sheet.
/// When the user logs in (this form loads), the message log is populated by all messages.
/// Each entry in the message log displays the timestamp, sender, and content of the message.
/// When the user sends a message to the db message table, it records the machine's MAC address
/// Messages are displayed in order by time sent
/// 
/// v1.1 Date: Mar 16, 2018
/// The form now plays a system sound when a new message is received. Also added a settings window
/// so that the user can disable this feature.
/// 
/// v1.2 Date: Mar 26, 2018
/// The application is now connected to a cloud database, so some methods had to be reworked to
/// use the new db schema.
/// The form now also allows the user to send a message by hitting the ENTER key. It also sends
/// a message when the user enters or exits the form.
/// </summary>
namespace TheMessenger
{
    public partial class frmMessenger : Form
    {
        public frmMessenger(string username)
        {
            InitializeComponent();
            Username = username;
            SettingsAudioNotifications = true;
            SettingsTimeDisplayTimestamp = true;
            SettingsTime24h = true;
            lblUser.Text = "Logged in as: " + username;
        }


        #region Attributes
        /// <summary>
        /// Attributes
        /// </summary>
        public string Username { get; set; }
        public int UserID { get; set; }
        public bool SettingsAudioNotifications { get; set; }
        public bool SettingsTimeDisplayTimestamp { get; set; }
        public bool SettingsTime24h { get; set; }
        #endregion
        /// <summary>
        /// Close the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMessenger_FormClosed(object sender, FormClosedEventArgs e)
        {
            string content = "SYSTEM: " + Username + " has left the chat room";
            SendMessage(content);
            Application.Exit();
        }

        /// <summary>
        /// On load, fill the chat log with all messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMessenger_Load(object sender, EventArgs e)
        {
            string content = "SYSTEM: " + Username + " has entered the chat room";
            SendMessage(content);
        }


        /// <summary>
        /// Send typed message to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            //define message to be sent
            string message = txtMessage.Text;
            
            //no blank messages
            if (message.Trim(' ') != "")
            {
                string content = txtMessage.Text;
                SendMessage(content);
            }
        }

        private void SendMessage(string content)
        {
            //create list of parameters for stored procedure
            List<SqlParameter> paramList = new List<SqlParameter>();

            //send message text
            paramList.Add(new SqlParameter("content", content));

            //get userID
            //paramList.Add(new SqlParameter("email", Username));                   //old method, since been replaced
            //DataTable dt = DAL.ExecStoredProcedure("GetIDByEmail", paramList);
            //int userID = (int)dt.Rows[0][0];
            //paramList.Clear();
            paramList.Add(new SqlParameter("userID", UserID));



            //send current time                                                     //old method, now determined by db
            //DateTimeOffset timestamp = DateTimeOffset.Now;
            //paramList.Add(new SqlParameter("created", timestamp));



            //execute the procedure
            DataTable dt = DAL.ExecStoredProcedure("InsertMessage", paramList);

            ////send system information
            ////SO user Mohammed A. Fadil
            ////URL: https://stackoverflow.com/questions/850650/reliable-method-to-get-machines-mac-address-in-c-sharp
            var macAddr =
                    (
                        from nic in NetworkInterface.GetAllNetworkInterfaces()
                        where nic.OperationalStatus == OperationalStatus.Up
                        select nic.GetPhysicalAddress().ToString()
                    ).FirstOrDefault().ToString();
            paramList.Clear();
            paramList.Add(new SqlParameter("mac", macAddr));
            paramList.Add(new SqlParameter("Id", UserID));
            dt = DAL.ExecStoredProcedure("insertMac", paramList);

            //update messages
            MessagesRefresh(false);

            //finally, clear the text box
            txtMessage.Clear();
        }

        /// <summary>
        /// Periodically refresh the message log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            MessagesRefresh(true);
        }
        
        /// <summary>
        /// Call this method when you would like to refresh the message log
        /// It compares the existing message log with the database's. If the database has
        /// more recent messages, it will populate the log with those messages
        /// </summary>
        private void MessagesRefresh(bool playSound)
        {
            //stop refresh timer 
            tmrRefresh.Stop();

            //execute stored procedure
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataTable dt = DAL.ExecStoredProcedure("GetMessagesPA", paramList);

            //compare displayed log with db log
            int logCount = lstLog.Items.Count;
            int dtCount = dt.Rows.Count;
            if (dtCount > logCount)
            {
                
                for (int i = logCount; i < dtCount; i++)
                {
                    //Concatenate message
                    string message;
                    string messageTime = "";
                    if (SettingsTimeDisplayTimestamp)
                    {
                        messageTime = dt.Rows[i][2].ToString();
                        ////I suspect this will be much easier to modify with SQL
                        //if (SettingsTime24h)
                        //{
                        //    //check if it's the afternoon (PM)
                        //    if (messageTime[19] == 'P')
                        //    {
                        //        string hour = messageTime.Substring(11, 2);
                        //        if (hour[1] == ':')
                        //        {
                        //            hour = hour.Substring(0, 1);
                        //            messageTime.Remove(11, 1);
                        //        }
                        //        else
                        //        {
                        //            messageTime.Remove(11, 2);
                        //        }
                        //        int hourInt = Convert.ToInt32(hour);
                        //        hourInt += 12;
                        //        hour = hourInt.ToString();
                        //        messageTime.Insert(11, hour);
                                
                        //    }
                        //}
                        
                        
                    }
                    
                    string messageText = dt.Rows[i][1].ToString();
                    string messageSender = dt.Rows[i][5].ToString();
                    if (SettingsTimeDisplayTimestamp)
                    {
                        message = "[" + messageTime + "] " + messageSender + ": " + messageText;
                    }
                    else
                    {
                        message = messageSender + ": " + messageText;
                    }
                    

                    //add message to log
                    lstLog.Items.Add(message);
                    
                    
                }

                //play a notification sound
                if (playSound && SettingsAudioNotifications)
                {
                    SystemSounds.Beep.Play();
                }
                
            }

            //scroll to the bottom of the listbox
            lstLog.TopIndex = lstLog.Items.Count - 1;

            

            //start refresh timer
            tmrRefresh.Start();

            
        }

        /// <summary>
        /// Open the settings window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings(this);
            frm.Show();
        }

        
    }
}
