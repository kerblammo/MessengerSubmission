using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>
/// Enables the user to change settings in the main application
/// </summary>
namespace TheMessenger
{
    public partial class frmSettings : Form
    {
        public frmSettings(frmMessenger messenger)
        {
            InitializeComponent();
            ParentMessenger = messenger;
            SettingsAudioNotifications = ParentMessenger.SettingsAudioNotifications;
            SettingsTimeDisplayTimestamp = ParentMessenger.SettingsTimeDisplayTimestamp;
            SettingsTime24h = ParentMessenger.SettingsTime24h;
        }

        #region Attributes
        /// Attributes
        public bool SettingsAudioNotifications { get; set; }
        public bool SettingsTimeDisplayTimestamp { get; set; }
        public bool SettingsTime24h { get; set; }
        public frmMessenger ParentMessenger { get; set; }
        #endregion

        /// <summary>
        /// Apply settings to parent form and close this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            ParentMessenger.SettingsAudioNotifications = chkAudio.Checked;
            ParentMessenger.SettingsTimeDisplayTimestamp = chkDisplayTime.Checked;
            ParentMessenger.SettingsTime24h = chkTime24h.Checked;
            this.Close();
        }

        /// <summary>
        /// Display initial properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSettings_Load(object sender, EventArgs e)
        {
            chkAudio.Checked = SettingsAudioNotifications;
            chkDisplayTime.Checked = SettingsTimeDisplayTimestamp;
            chkTime24h.Checked = SettingsTime24h;
        }

        /// <summary>
        /// Close this form without changing anything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkDisplayTime_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDisplayTime.Checked)
            {
                chkTime24h.Enabled = true;
            }
            else
            {
                chkTime24h.Enabled = false;
            }
        }
    }
}
