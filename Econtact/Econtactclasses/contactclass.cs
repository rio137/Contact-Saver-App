using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Econtact.Econtactclasses
{
    class contactclass
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconstrng = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //selecting data from database
        public DataTable Select()
        {
            ///step1: database connection
            ///
            SqlConnection conn = new SqlConnection(myconstrng);
            DataTable dt = new DataTable();
            try
            {
                //step:2 Writing sql query
                string sql = "SELECT * FROM tbl_contact";

                //creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql,conn);

                //creating sql data adapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //inserting data into database

        public bool Insert(contactclass c)
        {
            bool isSuccess = false;

            //step1: connect database
            SqlConnection conn = new SqlConnection(myconstrng);
            try
            {
                //step 2: create a sql query to insert data
                string sql = "INSERT INTO tbl_contact(FirstName,LastName,ContactNo,Address,Gender) VALUES (@FirstName,@LastName,@ContactNo,@Address,@Gender)";

                //creating sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //connection open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }

            catch
            {

            }

            finally
            {
                conn.Close();
            }
            return isSuccess;
        }




        public bool Update(contactclass c)
        {
            bool isSuccess = false;

            //step1: connect database
            SqlConnection conn = new SqlConnection(myconstrng);
            try
            {
                //step 2: create a sql query to update data
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName = @LastName , ContactNo=@ContactNo,Address=@Address, Gender=@Gender WHERE contactID=@contactID";


                //creating sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@contactID", c.ContactID);

                //connection open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }

            catch
            {

            }

            finally
            {
                conn.Close();
            }
            return isSuccess;
        }


        public bool Delete(contactclass c)
        {
            bool isSuccess = false;

            //step1: connect database
            SqlConnection conn = new SqlConnection(myconstrng);
            try
            {
                //step 2: create a sql query to update data
                string sql = "DELETE FROM tbl_contact WHERE contactID=@contactID";


                //creating sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create parameters to DELETE data
                
                cmd.Parameters.AddWithValue("@contactID", c.ContactID);

                //connection open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }

            catch
            {

            }

            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

    }
}
