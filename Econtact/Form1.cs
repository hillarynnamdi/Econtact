using Econtact.econtactclasses;
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
    public partial class contactform : Form 
    {
        public contactform()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            fadetimer.Start();
        }

  

        private void Add_Click(object sender, EventArgs e)
        {


            contactClass cc = new contactClass();
            
            cc.FirstName = txtfirstname.Text;
            cc.LastName = txtlastname.Text;
            cc.Gender = cmbgender.Text;
            cc.Address = txtaddress.Text;
            cc.ContactNo = txtcontactno.Text;

            bool success=cc.insert(cc);

            if (success == true)
            {
                
                MessageBox.Show("Was successfully added");
                clear();

            }
            else
            {
                MessageBox.Show("Was not successfully added");
            }


            updatedata();



        }

        private void Update_Click(object sender, EventArgs e)
        {
            contactClass cc = new contactClass();
            cc.FirstName = txtfirstname.Text;
            cc.LastName = txtlastname.Text;
            cc.Gender = cmbgender.Text;
            cc.ContactNo = txtcontactno.Text;
            cc.ContactId = Convert.ToInt16(txtcontactid.Text);
            cc.Address = txtaddress.Text;

            bool success = cc.update(cc);
            if (success == true)
            {

                MessageBox.Show("Was successfully updated");
                clear();

            }
            else
            {
                MessageBox.Show("Was not successfully updated");
            }

            updatedata();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            contactClass cc = new contactClass();
            cc.ContactId = Convert.ToInt16(txtcontactid.Text);

            bool success = cc.delete(cc);

            if (success == true)
            {

                MessageBox.Show("Was successfully deleted");
                clear();

            }
            else
            {
                MessageBox.Show("Was not successfully deleted");
            }


            updatedata();
        }

        private void Clear_Click(object sender, EventArgs e)
        {

            clear();

        }

        public void clear()
        {
            txtcontactid.Clear();
            txtcontactno.Clear();
            txtaddress.Clear();
            txtfirstname.Clear();
            txtlastname.Clear();
            txtsearch.Clear();
        }

        private void contactform_Load(object sender, EventArgs e)
        {
            updatedata();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = e.RowIndex;

            txtcontactid.Text = dataGridView1.Rows[id].Cells[0].Value.ToString();
            txtfirstname.Text = dataGridView1.Rows[id].Cells[1].Value.ToString();
            txtlastname.Text = dataGridView1.Rows[id].Cells[2].Value.ToString();
            txtcontactno.Text = dataGridView1.Rows[id].Cells[3].Value.ToString();
            txtaddress.Text = dataGridView1.Rows[id].Cells[5].Value.ToString();
            cmbgender.Text = dataGridView1.Rows[id].Cells[4].Value.ToString();

        }
            public void updatedata()
        {
            if (dataGridView1.RowCount > 0)
            {
                dataGridView1.Columns[0].Visible = false;
            }

            contactClass cc = new contactClass();
            DataTable dt = cc.select();
            dataGridView1.DataSource = dt;
        }

        private void txtcontactid_TextChanged(object sender, EventArgs e)
        {
            if (txtcontactid.Text == "")
            {
                Update.Enabled = false;
                Delete.Enabled = false;
            }
            else
            {
                Update.Enabled = true;
                Delete.Enabled = true;
            }
            
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (txtsearch != null)
            {

                DataTable dt = new DataTable();
                string keyword = txtsearch.Text;
                string connstr = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
                SqlConnection con = new SqlConnection(connstr);


                string query = string.Format("Select * from tbl_contact where FirstName LIKE '%{0}%'", keyword);


                SqlCommand cmd = new SqlCommand(query,con);

                SqlDataAdapter sql = new SqlDataAdapter(query,con);
                sql.Fill(dt);

                dataGridView1.DataSource = dt;



            }
        }

        private void fadetimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.075;
            }
            else
            {
                fadetimer.Stop();
                Application.Exit();
            }
        }
    }
}
