using InterviewApp1.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterviewApp1.PAL
{
    public partial class EmployeeForm : Form
    {
        BLL_EmployeeDetails empDetails = new BLL_EmployeeDetails();
        public EmployeeForm()
        {
            InitializeComponent();
        }


        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (!IsOnlyLetters(textBoxName.Text))
            {

                //((TextEdit)sender).Focus();
                errorProvider1.SetError(textBoxName, "Name should be characters!");
            }
            else
            {
                errorProvider1.SetError(textBoxName, "");
            }
        }

        private void textBoxAge_TextChanged(object sender, EventArgs e)
        {
            if (!IsOnlyNumbers(textBoxAge.Text))
            {
                textBoxAge.Focus();
                errorProvider2.SetError(textBoxAge, "Age should be numbers!");
            }
            else
            {
                errorProvider2.SetError(textBoxAge, "");
            }
        }

        private bool IsOnlyLetters(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsOnlyNumbers(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            txtEmployeeID.Text = "";
            txtName.Text = "";
            txtAge.Text = "";
            txtEducation.Text = "";
          
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                
                textBoxEmpID.Text = selectedRow.Cells[4].Value.ToString();
                textBoxName.Text = selectedRow.Cells[0].Value.ToString();
                textBoxAge.Text = selectedRow.Cells[2].Value.ToString();
                comboBoxEducation.Text = selectedRow.Cells[3].Value.ToString();
            }
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = empDetails.LoadEmployeeDetails();

            comboBoxEducation.DataSource = empDetails.LoadEducationDetails();
            comboBoxEducation.DisplayMember = "EduType"; // The column to display in the ComboBox
            comboBoxEducation.ValueMember = "eID";
            comboBoxEducation.SelectedIndex = -1;
        }

        private void buttonAdd_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            Boolean result = false;

            string employeeID = "";
            string name = "";
            string age = "";
            string gender = "";
            string education = "";

            employeeID = textBoxEmpID.Text;
            name = textBoxName.Text;
            age = textBoxAge.Text;
            education = comboBoxEducation.SelectedValue.ToString();

            if (txtEmployeeID.Text != null)
            {
                if ((txtName.Text == "" && txtAge.Text == "" && txtEducation.Text == "" ))
                {
                    MessageBox.Show("Please Fill All Details!");
                   
                }
                else
                {
                    result = empDetails.AddEmployeeDetails(employeeID, name, age, education, gender);

                    if (result == true)
                    {
                        
                        MessageBox.Show("Saved Successfully!");
                        dataGridView1.DataSource = empDetails.LoadEmployeeDetails();
                    }
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxEmpID.Text))
            {
                string employeeID = textBoxEmpID.Text;
     
                var confirmResult = MessageBox.Show("Are you sure to delete this employee?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    bool result = empDetails.DeleteEmployeeDetails(employeeID);

                    if (result)
                    {
                        MessageBox.Show("Employee deleted successfully!");

                        
                        dataGridView1.DataSource = empDetails.LoadEmployeeDetails();

                        textBoxEmpID.Text = "";
                        textBoxName.Text = "";
                        textBoxAge.Text = "";
                        comboBoxEducation.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete employee.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.");

            }

        }

        

    }
}
