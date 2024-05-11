using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThesisPrototype
{
    public partial class MainForm : Form
    {
        InventoryManagementForm FormInv;
        SalesOperationForms FormOperation;
        SalesReportForms FormReports;
        public bool sidebarExpand;
        public static readonly string connstring = "Data Source=DESKTOP-33UQUOB\\SQLEXPRESS;Initial Catalog=ChefJoyDB;Integrated Security=True;";
        public static SqlConnection connection = new SqlConnection(connstring);
        public MainForm()
        {
            InitializeComponent();
        }
        

        
        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                    UpdateSize(sidebar.Width);
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                    UpdateSize(sidebar.Width);
                }
            }

        }
        private void UpdateSize(int width)
        {

            pnlInv.Width = width;
            pnlSalesOp.Width = width;
            pnlSales.Width = width;
            pnlLogout.Width = width;

        }
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {   //Menu Button
            sidebarTimer.Start();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        { //Inventory Management Button

            if (FormInv == null)
            {
                FormInv = new InventoryManagementForm();
                FormInv.FormClosed += FormInv_FormClosed;
                FormInv.MdiParent = this;
                FormInv.Show();
                FormInv.BringToFront();
            }
            else
            {
                FormInv.Activate();
            }
            
        }

        private void FormInv_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormInv = null;
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (msgLogout.Show("Are you sure you want to logout?", "Chef Joy Application") == DialogResult.Yes)
            {
                string insertQuery = @"UPDATE CJ_Session set sessionEnd = @sessionEnd";
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@sessionEnd", DateTime.Now);

                    connection.Open();
                    insertCommand.ExecuteNonQuery();
                    connection.Close();
                    SwitchFroms SF = new SwitchFroms();
                    SF.Switch(this, new LoginForm());
                }
                    
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (FormOperation == null)
            {
                FormOperation = new SalesOperationForms();
                FormOperation.FormClosed += FormOperation_FormClosed;
                FormOperation.MdiParent = this;
                FormOperation.Show();
                FormOperation.BringToFront();
            }
            else
            {
                FormOperation.Activate();
            }
        }
        private void FormOperation_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormOperation = null;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            //Sales report
            if (FormReports == null)
            {
                FormReports = new SalesReportForms();
                FormReports.FormClosed += FormReports_FormClosed;
                FormReports.MdiParent = this;
                FormReports.Show();
                FormReports.BringToFront();
            }
            else
            {
                FormOperation.Activate();
            }
        }
        private void FormReports_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormReports = null;
        }

    }
}

