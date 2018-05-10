using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Econtact.econtactclasses
{
    class contactClass
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNo { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }


        static string connectionstr = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;

        //select data from database
        public DataTable select()
        {
            using (SqlConnection con = new SqlConnection(connectionstr))
            {
                DataTable dt = new DataTable();

                string query = "Select* from tbl_contact";

                SqlCommand cmd = new SqlCommand(query, con);

                //creating sql adapter

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                con.Open();

                adapter.Fill(dt);

                return dt;
            }   
        }

        public DataTable search(string c)
        {
            using (SqlConnection con = new SqlConnection(connectionstr))
            {
                DataTable dt = new DataTable();

                string query = "Select * from tbl_contact where FirstName like '%'+c+'%' or LastName like '%'+c+'%' or ContactNo like '%'+c+'%'"; 


                SqlCommand cmd = new SqlCommand(query, con);

                //creating sql adapter

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                con.Open();

                adapter.Fill(dt);

                return dt;
            }
        }

        public bool insert(contactClass c)
        {
            bool issuccess = false;
            using (SqlConnection con = new SqlConnection(connectionstr))
            {
                string query = "Insert into tbl_contact(FirstName,LastName,ContactNo,Address,Gender) values(@FirstName,@LastName,@ContactNo,@Address,@Gender)";

                SqlCommand cmd = new SqlCommand(query, con);


                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                con.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    issuccess = true;
                }

                return issuccess;
            }

        }

        public bool update(contactClass c)
        {
            using (SqlConnection con = new SqlConnection(connectionstr))
            {

                bool issucess = false;

                string query = "update tbl_contact set FirstName=@FirstName,LastName=@LastName,ContactNo=@ContactNo,Address=@Address,Gender=@Gender where ContactId=@ContactId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ContactId", c.ContactId);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                con.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    issucess = true;
                }

                return issucess;

            }


        }


        public bool delete(contactClass c)
        {
            using (SqlConnection con = new SqlConnection(connectionstr))
            {

                bool issucess = false;

                string query = "delete from tbl_contact where ContactID=@ContactID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactId);

                con.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    issucess = true;
                }

                return issucess;

            }


        }

      




    }
}
