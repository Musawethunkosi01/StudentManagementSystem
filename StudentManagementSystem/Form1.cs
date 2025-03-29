using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace StudentManagementSystem
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection("Data Source=DESKTOP-6MU06SD\\SQLEXPRESS;Initial Catalog=Students_DB;Integrated Security=True;");
            con.Open();
        }

        public void ClearBox()
        {
            txtStudentID.Clear();
            txtName.Clear();
            txtSurname.Clear();
            txtEmail.Clear();
            txtAge.Clear();
            txtNumber.Clear();
            rdbtnFemale.Checked = false;
            rdbtnMale.Checked = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            string name = txtName.Text;
            string surname = txtSurname.Text;
            string email = txtEmail.Text;
            string number = txtNumber.Text;

            //validate studentID
            if (! int.TryParse(txtStudentID.Text, out int studentID))
            {
                MessageBox.Show("Student ID must be a valid number!", "Invalid Input");
                return;
            }
            
            //validate age
            if (! double.TryParse(txtAge.Text, out double age))
            {
                MessageBox.Show("Student ID must be a valid number!", "Invalid Input");
                return;
            }

            // Validate Required Fields
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtNumber.Text))
            {
                MessageBox.Show("Please enter all required fields!", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //Validate gender
            string gender = "";

            if(rdbtnMale.Checked)
                gender = "Male";
            else if(rdbtnFemale.Checked)
                gender = "Female";
            else
            {
                MessageBox.Show("Please select a gender!", "Required");
                return;
            }

            //Check if connection is open
            if(con.State != ConnectionState.Open)
            {
                MessageBox.Show("Database connection is not open", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try 
                {
                    using (SqlCommand cmd = new SqlCommand("exec InsertStudent_SP @StudentID, @Name, @Surname, @Age, @Email, @Number, @Gender", con))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", studentID);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Surname", surname);
                        cmd.Parameters.AddWithValue("@Age", age);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Number", number);
                        cmd.Parameters.AddWithValue("@Gender", gender);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Added", "Success", MessageBoxButtons.OK);
                        GetList();
                        ClearBox();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
                           
        }

        //Fetch data from database an dplace to datagridview
        void GetList()
        {
            SqlCommand c = new SqlCommand("exec ListStudent_SP", con);
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        private void dataGridView2_CellContentClick(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetList();
        }

        //Update records in database
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            string name = txtName.Text;
            string surname = txtSurname.Text;
            string email = txtEmail.Text;
            string number = txtNumber.Text;

            //Validate studentID
            if (!int.TryParse(txtStudentID.Text, out int studentID))
            {
                MessageBox.Show("Student ID must be a valid number!", "Invalid Input");
                return;
            }

            //Validate age
            if (!double.TryParse(txtAge.Text, out double age))
            {
                MessageBox.Show("Student ID must be a valid number!", "Invalid Input");
                return;
            }

            // Validate required fields
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtNumber.Text))
            {
                MessageBox.Show("Please enter all required fields!", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string gender = "";
            if (rdbtnMale.Checked)
                gender = "Male";
            else if (rdbtnFemale.Checked)
                gender = "Female";
            else
            {
                MessageBox.Show("Please select a gender!", "Required");
                return;
            }

            //Check if connection is open
            if (con.State != ConnectionState.Open)
            {
                MessageBox.Show("Database connection is not open", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("exec UpdateStudent_SP @StudentID, @Name, @Surname, @Age, @Email, @Number, @Gender", con))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", studentID);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Surname", surname);
                        cmd.Parameters.AddWithValue("@Age", age);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Number", number);
                        cmd.Parameters.AddWithValue("@Gender", gender);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated", "Success", MessageBoxButtons.OK);
                        GetList();
                        ClearBox();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Delete records in datasbase
        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if(MessageBox.Show("Are you sure you want to delete student record?", "Delete Document", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!int.TryParse(txtStudentID.Text, out int studentID))
                {
                    MessageBox.Show("Student ID must be a valid number!", "Invalid Input");
                    return;
                }


                using (SqlCommand cmd = new SqlCommand("exec DeleteStudent_SP @StudentID ", con))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentID);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Deleted ");
                    GetList();
                    ClearBox();
                }
            }
     
        }

        //Fecth and display specific records
        private void btnStudents_Click(object sender, EventArgs e)
        {
            
            if (!int.TryParse(txtStudentID.Text, out int studentID))
            {
                MessageBox.Show("Please enter a valid student ID number!", "Invalid Input");
                return;
            }

            try
            {
                SqlCommand c = new SqlCommand(" exec LoadStudent_SP '" + studentID + "' ", con);
                SqlDataAdapter sd = new SqlDataAdapter(c);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                dataGridView2.DataSource = dt;

                
                bool found = false;

                foreach(DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["Student_ID"].Value != null && row.Cells["Student_ID"].Value.ToString() == txtStudentID.Text)
                    {
                        txtName.Text = row.Cells["FirstName"].Value?.ToString() ?? "";
                        txtSurname.Text = row.Cells["Surname"].Value?.ToString() ?? "";
                        txtAge.Text = row.Cells["Age"].Value?.ToString() ?? "";
                        txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                        txtNumber.Text = row.Cells["PhoneNumber"].Value?.ToString() ?? "";
                    
                        dataGridView2.ClearSelection();
                        row.Selected = true;
                        found = true;
                        break;
                    }
                   
                }
                if (!found)
                {
                    MessageBox.Show("Student ID number not found");
                    ClearBox();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error loading student data: " + ex.Message, "Error");
            }
        }
    }
}
