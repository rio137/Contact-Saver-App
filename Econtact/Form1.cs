using Econtact.Econtactclasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        contactclass c = new contactclass();

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load Data on Datagried view
            DataTable dt = c.Select();
            dgvcontactlist.DataSource = dt;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            //get the value from the input field
            c.FirstName = txtboxfirstname.Text;
            c.LastName = txtlastname.Text;
            c.ContactNo = txtcontactno.Text;
            c.Address = txtaddress.Text;
            c.Gender = cmbgender.Text;

            bool success = c.Insert(c);
            if(success == true)
            {
                MessageBox.Show("Data saved successfully");
                clear();
            }
            else
            {
                MessageBox.Show("Something went wrong. Try again!");
            }

            //Load Data on Datagried view
            DataTable dt = c.Select();
            dgvcontactlist.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void clear()
        {
            txtboxfirstname.Text = "";
            txtlastname.Text = "";
            txtcontactno.Text = "";
            txtaddress.Text = "";
            cmbgender.Text = "";
            txtboxcontactid.Text = "";
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            //get the data from textboxes
            c.ContactID = int.Parse(txtboxcontactid.Text);
            c.FirstName = txtboxfirstname.Text;
            c.LastName = txtlastname.Text;
            c.ContactNo = txtcontactno.Text;
            c.Address = txtaddress.Text;
            c.Gender = cmbgender.Text;

            //update data in database
            bool success = c.Update(c);
            if(success==true)
            {
                MessageBox.Show("Updated Data Successfully");

                //load data on datagried view
                DataTable dt = c.Select();
                dgvcontactlist.DataSource = dt;
                clear();
            }
            else
            {
                MessageBox.Show("Failed to update contact. Try Again");
            }
        }

        private void dgvcontactlist_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the data from data grid view and load it to the text boxes respectively
            //identify the row on which the mouse is clicked
            int rowIndex = e.RowIndex;
            txtboxcontactid.Text = dgvcontactlist.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxfirstname.Text = dgvcontactlist.Rows[rowIndex].Cells[1].Value.ToString();
            txtlastname.Text = dgvcontactlist.Rows[rowIndex].Cells[2].Value.ToString();
            txtcontactno.Text = dgvcontactlist.Rows[rowIndex].Cells[3].Value.ToString();
            txtaddress.Text = dgvcontactlist.Rows[rowIndex].Cells[4].Value.ToString();
            cmbgender.Text = dgvcontactlist.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            //get the contact id from the application
            c.ContactID = Convert.ToInt32(txtboxcontactid.Text);
            bool success = c.Delete(c);
            if(success == true)
            {
                MessageBox.Show("Contact Deleted Successfully");
                //load data on data gried view
                DataTable dt = c.Select();
                dgvcontactlist.DataSource = dt;
                clear();
            }
            else
            {
                MessageBox.Show("Failed to Delete Contact. Try Again.");
            }
        }
        static string myconnstr = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        private void txtboxsearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtboxsearch.Text;

            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'",conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvcontactlist.DataSource = dt;
        }

        private bool isDragging = false;
        private Point dragStartPoint = Point.Empty;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragStartPoint = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(isDragging)
            {
                Point CurrentPoint = PointToScreen(new Point(e.X, e.Y));
                Location = new Point(CurrentPoint.X - dragStartPoint.X, CurrentPoint.Y - dragStartPoint.Y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
