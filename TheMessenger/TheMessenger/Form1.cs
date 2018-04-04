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

namespace TheMessenger
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Check if record of username and password exists
        /// If username exists, but password doesn't match, display error
        /// If username exists and password matches, log in as that user
        /// Else, register a new user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerify_Click(object sender, EventArgs e)
        {

            //All fields must be filled
            if (txtEmail.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show("Whoa there, partner! \nAll fields must be filled", "Empty Fields");
                if (txtEmail.Text == "")
                {
                    txtEmail.Focus();
                }
                else
                {
                    txtPass.Focus();
                }
            }
            else
            {


                //Check if username exists
                //Call a stored procedure from database. First need a list of paramaters
                List<SqlParameter> paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("email", txtEmail.Text));    //TODO: these parameters are used elsewhere. Instantiate them once to use multiple times
                paramList.Add(new SqlParameter("password", txtPass.Text));

                //Call the stored procedure using DAL class
                //Store the data in a usable manner
                DataTable dt = DAL.ExecStoredProcedure("Login", paramList);

                //if username exists and password matches
                if (dt.Rows.Count == 1)
                {
                    //get userid
                    int id = (int)dt.Rows[0][0];
                    //login the user
                    Login(id);
                }
                else
                {
                    //does username exist?
                    //run new stored procedure
                    paramList.Clear();
                    paramList.Add(new SqlParameter("email", txtEmail.Text));
                    dt = DAL.ExecStoredProcedure("VerifyEmail", paramList);

                    //see if there is a result, display invalid password
                    if (dt.Rows.Count != 0)
                    {
                        MessageBox.Show("Whoops, looks like that password doesn't match our records!"
                            + "\nIf you've forgotten your password, then you're out of luck as we don't have a recovery service in place",
                            "Invalid Password");
                        txtPass.Clear();
                        txtPass.Focus();
                    }
                    //username does not exist, register a new user
                    else
                    {
                        //give an option for user to register
                        var result = MessageBox.Show("It looks like you don't exist yet! \nWould you like to register a profile?",
                                        "Unregistered User",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            //create a new entry for the user
                            //execute a stored procedure
                            paramList.Clear();
                            paramList.Add(new SqlParameter("email", txtEmail.Text));
                            paramList.Add(new SqlParameter("password", txtPass.Text));
                            paramList.Add(new SqlParameter("firstName", "John"));
                            paramList.Add(new SqlParameter("lastName", "Doe"));
                            dt = DAL.ExecStoredProcedure("CreateUser", paramList);

                            //login the user
                            paramList.Clear();
                            paramList.Add(new SqlParameter("email", txtEmail.Text));
                            paramList.Add(new SqlParameter("password", txtPass.Text));
                            dt = DAL.ExecStoredProcedure("Login", paramList);
                            int id = (int)dt.Rows[0][0];
                            Login(id);   
                        }
                        else
                        {
                            txtPass.Clear();
                        }
                    }
                }
            }
                
        }

        /// <summary>
        /// Hide this form and open the chat form
        /// </summary>
        public void Login(int id)
        {
            
            frmMessenger messenger = new frmMessenger(txtEmail.Text);
            messenger.UserID = id;
            messenger.Show();
            this.Hide();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
