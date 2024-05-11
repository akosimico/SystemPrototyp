using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ThesisPrototype
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(UsernameTxt.Text) || string.IsNullOrEmpty(PasswordTxt.Text))
            {
                msgNull.Show("Please enter username and password", "Error");
                return;
            } else if(Authentication.IsValidUser(UsernameTxt.Text, PasswordTxt.Text) == false)
            {
                msgWrong.Show("Invalid username or password", "Error");
            }
            else
            {
                
                SwitchFroms SF = new SwitchFroms();
                SF.Switch(this, new MainForm());
            }
            
           
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if(msgExit.Show("Are you sure you want to exit?", "Chef Joy Application")== DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
